using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using CipherPark.TriggerOrange.Core.Data;
using CipherPark.TriggerOrange.Core.ApplicationServices;

namespace CipherPark.TriggerOrange.Core
{
    public class OrangeTaskManager
    {
        private const int TimerInterval = 60000; //1 minute.
        private readonly object _schedulerSyncRoot = new object();
        private readonly object _queueSyncRoot = new object();
        private OrangeApi _api = new OrangeApi();
        private Timer _timer = null;
        //private List<TaskSchedule> _schedules = new List<TaskSchedule>();
        private Queue<QueuedTask> _longRunningTaskQueue = new Queue<QueuedTask>();
        private Task _taskController = null;
        private AutoResetEvent _taskEvent = new AutoResetEvent(false);
        private AutoResetEvent _stopTaskControllerEvent = new AutoResetEvent(false);
        private long? _executingTaskId = null;

        public OrangeTaskManager()
        {
            _timer = new Timer(ScheduleTimer_Callback);
        }

        #region Initialization/Uninitialization

        public void Start()
        {         
            //Clean up DB state
            using (OrangeEntities db = new OrangeEntities())
            {
                db.LongRunningTasks.Where(t => t.Status == LongRunningTaskStatus.Processing)
                                   .ToList()
                                   .ForEach(t =>
                                   {
                                       t.Status = LongRunningTaskStatus.Error;
                                       t.ErrorMessage = "Processing task was terminated unexpectedly";
                                   });

                db.LongRunningTasks.Where(t => t.Status == LongRunningTaskStatus.Pending)
                                   .ToList()
                                   .ForEach(t =>
                                   {
                                       t.Status = LongRunningTaskStatus.Error;
                                       t.ErrorMessage = "Pending task was cancelled unexpectedly";
                                   });
                db.SaveChanges();
            }

            //Start task management.
            StartTaskController();

            //Start scheduler.
            StartScheduler();
        }

        public void Stop()
        {
            StopScheduler();

            StopTaskController();

            CancelRunningTasks();
        }

        #endregion

        #region Schedule Management

        public void RestartScheduler()
        {
            if (IsSchedulerRunning)
                StopScheduler();
            StartScheduler();
        }

        public bool IsSchedulerRunning
        {
            get; private set;
        }

        private void StartScheduler()
        {
            using (OrangeEntities db = new OrangeEntities())
            {
                lock (_schedulerSyncRoot)
                {                     
                    _timer.Change(0, TimerInterval);
                    IsSchedulerRunning = true;
                }
            }
        }

        private void StopScheduler()
        {
            lock (_schedulerSyncRoot)
            {
                _timer.Change(Timeout.Infinite, Timeout.Infinite);
                IsSchedulerRunning = false;
                //_schedules.Clear();
            }
        }

        private void ScheduleTimer_Callback(object state)
        {
            lock (_schedulerSyncRoot)
            {
                List<TaskSchedule> _schedules = new List<TaskSchedule>();

                using (OrangeEntities db = new OrangeEntities())
                {                    
                    foreach (var dbSchedule in db.Schedules)
                    {
                        TaskSchedule schedule = new TaskSchedule();
                        _schedules.Add(schedule);
                        schedule.Name = dbSchedule.Name;
                        schedule.Id = dbSchedule.Id;
                        schedule.StartTime = dbSchedule.StartTime;
                        //_schedule.ValidFrom = dbSchedule.ValidFrom;
                        //_schedule.Frequency = dbSchedule.Frequency;
                        schedule.ActiveSunday = dbSchedule.ActiveSunday;
                        schedule.ActiveMonday = dbSchedule.ActiveMonday;
                        schedule.ActiveTuesday = dbSchedule.ActiveTuesday;
                        schedule.ActiveWednesday = dbSchedule.ActiveWednesday;
                        schedule.ActiveThursday = dbSchedule.ActiveThursday;
                        schedule.ActiveFriday = dbSchedule.ActiveFriday;
                        schedule.ActiveSaturday = dbSchedule.ActiveSaturday;
                        schedule.Enabled = dbSchedule.Enabled;
                        schedule.LastTriggered = dbSchedule.LastTriggered;
                    }                                  
                }

                foreach (var _schedule in _schedules)
                {
                    if (IsSchedulerRunning && _schedule.IsTriggerTime())
                    {
                        foreach (string site in new[] { MarketPlaceSiteNames.eBayHotStarters })
                        {
                            EnqueueLongRunningTask(LongRunningTaskNames.Generate(LongRunningTaskNames.UpdateMarketPlaceHotLists, site),
                                                    "Scheduler",
                                                    () =>
                                                    {
                                                        _api.UpdateMarketPlaceHotLists(site);                                                        
                                                    });
                        }

                        _schedule.LastTriggered = DateTime.Now;

                        using (OrangeEntities db = new OrangeEntities())
                        {
                            var dbSchedule = db.Schedules.FirstOrDefault(s => s.Id == _schedule.Id);
                            if (dbSchedule != null)
                            {
                                dbSchedule.LastTriggered = _schedule.LastTriggered;
                                db.SaveChanges();
                            }
                        }
                    }
                }
            }
        }

        #endregion        

        #region Long Running Task Management

        private void CancelRunningTasks()
        {
            //TODO: Implement task cancellation (via cancellation token).
        }

        private void StartTaskController()
        {
            if (_taskController == null)
            {
                _taskController = Task.Run(() =>
                {
                    WaitHandle[] events = new WaitHandle[] { _taskEvent, _stopTaskControllerEvent };
                    const int TaskEventIndex = 0;
                    const int ExitEventIndex = 1;
                    bool exitLoop = false;

                    while (true)
                    {
                        int result = WaitHandle.WaitAny(events);

                        switch (result)
                        {
                            case TaskEventIndex:
                                Queue<QueuedTask> auxQueue = new Queue<QueuedTask>();
                                lock (_queueSyncRoot)
                                {
                                    while (_longRunningTaskQueue.Count > 0)
                                        auxQueue.Enqueue(_longRunningTaskQueue.Dequeue());
                                }
                                while (auxQueue.Count > 0)
                                {
                                    QueuedTask qt = auxQueue.Dequeue();
                                    UpdateDBRunningTask(qt.TaskId, LongRunningTaskStatus.Processing);
                                    string errorMessage = null;
                                    string status = null;
                                    try
                                    {                                  
                                        this._executingTaskId = qt.TaskId;
                                        OrangeCoreDiagnostics.LogTaskExecuting(qt);
                                        qt.ExecuteLongRunningTask();
                                        OrangeCoreDiagnostics.LogTaskComplete(qt);
                                        this._executingTaskId = null;
                                        status = LongRunningTaskStatus.Complete;                                        
                                    }
                                    catch (Exception ex)
                                    {
                                        OrangeCoreDiagnostics.LogTaskException(qt, ex);
                                        errorMessage = ex.GetCompleteDetails();
                                        status = LongRunningTaskStatus.Error;
                                    }
                                    finally
                                    {
                                        UpdateDBFinishedTask(qt.TaskId, status, errorMessage);
                                    }
                                }
                                break;

                            case ExitEventIndex:
                                exitLoop = true;
                                break;
                        }
                        if (exitLoop)
                            break;
                    }
                });
            }
        }

        private void StopTaskController()
        {
            _stopTaskControllerEvent.Set();
        }

        private void EnqueueLongRunningTask(string longRunningTaskName, string executedBy, Action longRunningTask)
        {
            long runningTaskId = InsertDBRunningTask(longRunningTaskName, executedBy, LongRunningTaskStatus.Pending);
            QueuedTask qt = new QueuedTask()
            {
                Name = longRunningTaskName,
                ExecuteLongRunningTask = longRunningTask,
                TaskId = runningTaskId
            };
            OrangeCoreDiagnostics.LogQueueingTask(qt);            
            _longRunningTaskQueue.Enqueue(qt);
            _taskEvent.Set();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskName"></param>
        /// <param name="executedBy"></param>
        /// <param name="isPending"></param>
        /// <returns></returns>
        private long InsertDBRunningTask(string taskName, string executedBy, string status = LongRunningTaskStatus.Processing)
        {
            long taskId = 0;
            using (OrangeEntities db = new OrangeEntities())
            {
                LongRunningTask lrt = new LongRunningTask()
                {
                    Name = taskName,
                    StartTimestamp = DateTime.Now,
                    ExecutedBy = executedBy,
                    Status = status
                };
                db.LongRunningTasks.Add(lrt);
                db.SaveChanges();
                taskId = lrt.Id;
            }
            return taskId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        private void UpdateDBRunningTask(long runningTaskId, string status)
        {
            using (OrangeEntities db = new OrangeEntities())
            {
                LongRunningTask lrt = db.LongRunningTasks.First(t => t.Id == runningTaskId);
                lrt.Status = status;
                db.SaveChanges();
            }
        }

        private void UpdateDBFinishedTask(long runningTaskId, string status = LongRunningTaskStatus.Complete, string errorMessage = null)
        {
            using (OrangeEntities db = new OrangeEntities())
            {
                LongRunningTask lrt = db.LongRunningTasks.First(t => t.Id == runningTaskId);
                lrt.EndTimestamp = DateTime.Now;
                lrt.Status = status;
                lrt.ErrorMessage = errorMessage;
                db.SaveChanges();
            }
        }

        #endregion

        #region Long Running Tasks
       
        public void UpdateMarketPlaceCategories(string siteName)
        {
            EnqueueLongRunningTask(LongRunningTaskNames.Generate(LongRunningTaskNames.UpdateMarketPlaceCategories, siteName),
                                   "User",
                                   () => {
                                       _api.UpateMarketPlaceCategories(siteName);
                                   });
        }

        public void UpdateMarketPlaceHotLists(string siteName)
        {
            EnqueueLongRunningTask(LongRunningTaskNames.Generate(LongRunningTaskNames.UpdateMarketPlaceHotLists, siteName),
                                   "User",
                                   () => {
                                       _api.UpdateMarketPlaceHotLists(siteName);
                                   });
        }

        public void UpdateReportAnalytics(long reportId)
        {
            //Not yet implemented.
        }

        #endregion

        #region Short Running Tasks
       
        #endregion
    }

    public class TaskSchedule
    {        
        public string Name { get; set; }
        public long Id { get; set; }
        public DateTime StartTime { get; set; }        
        public bool Enabled { get; set; }
        public bool ActiveSunday { get; set; }
        public bool ActiveMonday { get; set; }
        public bool ActiveTuesday { get; set; }
        public bool ActiveWednesday { get; set; }
        public bool ActiveThursday { get; set; }
        public bool ActiveFriday { get; set; }
        public bool ActiveSaturday { get; set; }
        public DateTime? LastTriggered { get; set; }  

        public bool IsTriggerTime()
        {
            DateTime now_ = DateTime.Now;
            if (Enabled)
            {
                const int TriggerWindowSize = 15;
                DateTime previousTriggerTime = (LastTriggered != null ? LastTriggered.Value : StartTime);
                //Return true if...
                //1. This schedule is active today.
                //2. This schedule was not triggered today
                //3. The current time is on or past the trigger time.
                //4. The current time is still within the trigger window.
                return IsActiveToday() &&
                       previousTriggerTime.Date < now_.Date &&
                       StartTime.TimeOfDay <= now_.TimeOfDay &&
                       (now_.TimeOfDay - StartTime.TimeOfDay).TotalMinutes > TriggerWindowSize;
            }
            else
                return false;
        }      

        public bool IsActiveToday()
        {
            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return ActiveSunday;
                case DayOfWeek.Monday:
                    return ActiveMonday;
                case DayOfWeek.Tuesday:
                    return ActiveTuesday;
                case DayOfWeek.Wednesday:
                    return ActiveWednesday;
                case DayOfWeek.Thursday:
                    return ActiveThursday;
                case DayOfWeek.Friday:
                    return ActiveFriday;
                case DayOfWeek.Saturday:
                    return ActiveSaturday;
                default:
                    throw new InvalidOperationException("Unexpected day of week");
            }            
        }
    }
    
    public class QueuedTask
    {
        public string Name { get; set; }
        public long TaskId { get; set; }
        public Action ExecuteLongRunningTask { get; set; }
    }    
}