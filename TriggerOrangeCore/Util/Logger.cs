using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.VisualBasic.Logging;
using VBLogFileLocation = Microsoft.VisualBasic.Logging.LogFileLocation;

namespace CipherPark.TriggerOrange.Core.ApplicationServices
{   
    public interface ILogger
    {
        void LogException(Exception exception);

        void LogInfo(string info);

        void LogWarning(string warning);

        void LogError(string error);

        void StartLogicalOperation(string p);

        void EndLogicalOperation();
    }    
   
    /// <summary>
    /// Creates a logger which outputs trace messages to console and, optionally, file.
    /// </summary>
    public class DefaultLogger : ILogger
    {
        FileLogTraceListener _fileTraceListener = null;
        TraceOptions _options = TraceOptions.None;

        /// <summary>
        /// Creates a logger which outputs trace messages to console and, optionally, file.
        /// </summary>
        public DefaultLogger()
        {          
            _options = TraceOptions.DateTime |
                       TraceOptions.ProcessId |
                       TraceOptions.LogicalOperationStack |
                       TraceOptions.ThreadId;
            Trace.Listeners.Remove("Default");
            ConsoleTraceListener ctl = new ConsoleTraceListener();
            ctl.Filter = new DefaultLoggerTraceFilter();
            ctl.TraceOutputOptions = _options;
            Trace.Listeners.Add(ctl);             
            Trace.AutoFlush = true;          
        }

        #region ILogger Implementation

        public void LogException(Exception exception)
        {
            lock (this) { Trace.TraceError(_Pre(exception.GetCompleteDetails())); }
        }

        public void LogInfo(string info)
        {
            lock (this) { Trace.TraceInformation(_Pre(info)); }
        }

        public void LogWarning(string warning)
        {
            lock (this) { Trace.TraceWarning(_Pre(warning)); }
        }

        public void LogError(string error)
        {
            lock (this) { Trace.TraceError(_Pre(error)); }
        }

        public void StartLogicalOperation(string p)
        {
            lock (this) { Trace.CorrelationManager.StartLogicalOperation(p); }
        }

        public void EndLogicalOperation()
        {
            lock (this) { Trace.CorrelationManager.StopLogicalOperation(); }
        }            

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        public void LogStart(string title)
        {           
            string caption = string.Format("LOG SESSION START - {0} ", title);            
            string border = string.Join(string.Empty, Enumerable.Repeat("*", caption.Length));
            Trace.WriteLine(string.Empty);
            Trace.WriteLine(border);
            Trace.WriteLine(caption);
            Trace.WriteLine(border);
            Trace.WriteLine(string.Empty); 
        }        

        /// <summary>
        /// Enables or Disables sending logging information to a file on disk. File logging is disabled by default.
        /// </summary>
        /// <param name="enable"></param>
        /// <param name="predefinedLocation"></param>
        /// <param name="customFileLocation"></param>
        /// <param name="maxFileSize">The max file size of of the log file. The default is 100MB (104857600 bytes)</param>
        public void EnableFileLogging(bool enable, LogFileLocation? predefinedLocation = null, string customFileLocation = null, int maxFileSize = 104857600 )
        {
            if (enable)
            {           
                if (_fileTraceListener == null)                
                    _fileTraceListener = new FileLogTraceListener();
                
                if (!Trace.Listeners.Contains(_fileTraceListener))
                    Trace.Listeners.Add(_fileTraceListener);                
              
                if (predefinedLocation == null)
                    throw new ArgumentNullException("'predefinedLocation' was null while 'enable' was set to 'true'.");
  
                //IMPORTANT!: We must set the file location first because other properties
                //of the FileLogTraceListener Demand Permission for writing to the specified location.
                //(See source code at http://referencesource.microsoft.com/#Microsoft.VisualBasic/Logging/FileLogTraceListener.vb) 

                _fileTraceListener.Location = (VBLogFileLocation)predefinedLocation.Value;

                if (predefinedLocation.Value == LogFileLocation.Custom && customFileLocation == null)
                    throw new ArgumentNullException("'customFileLocation' was null while 'predefinedLocation' was set to 'Custom'");

                if(customFileLocation != null)
                    _fileTraceListener.CustomLocation = customFileLocation;

                _fileTraceListener.Filter = new DefaultLoggerTraceFilter();
                _fileTraceListener.Delimiter = Environment.NewLine;
                _fileTraceListener.TraceOutputOptions = _options;
                _fileTraceListener.LogFileCreationSchedule = LogFileCreationScheduleOption.Daily;
                _fileTraceListener.MaxFileSize = maxFileSize;  
            }
            
            else 
            {
                if(Trace.Listeners.Contains(_fileTraceListener))
                    Trace.Listeners.Remove(_fileTraceListener);
            }            
        }

        /// <summary>
        /// Gets the name of the log file this default logger instance writes to.
        /// Returns null if file logging hasn't been enabled.
        /// </summary>       
        public string LogFileName 
        {
            get { 
                if(Trace.Listeners.Contains(_fileTraceListener))
                    return _fileTraceListener.FullLogFileName;                    
                else 
                    return null;
            }
        }

        /// <summary>
        /// Returns the directory of the log file if a custom location has be specified.
        /// </summary>
        public string LogFileDirectory
        {
            get
            {
                if (Trace.Listeners.Contains(_fileTraceListener) &&
                    _fileTraceListener.Location == VBLogFileLocation.Custom)
                    return _fileTraceListener.CustomLocation;
                else
                    return null;
            }
        }
                   

        /// <summary>
        /// Performs pre-processing/formatting to the trace ouput stream and input message.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>The result of the processed input</returns>
        private string _Pre(string input)
        {
            Trace.WriteLine(string.Empty);
            Trace.WriteLine(string.Join(string.Empty, Enumerable.Repeat("-", 30)));
            Trace.WriteLine(string.Empty);
            return input.TrimEnd();
        }
    }
    
    internal class DefaultLoggerTraceFilter : TraceFilter
    {
        public override bool ShouldTrace(TraceEventCache cache, string source, TraceEventType eventType, int id, string formatOrMessage, object[] args, object data1, object[] data)
        {           
            //TODO: Add filtering support for DefaultLogger.
            return true;
        }
    }

    public enum LogFileLocation
    {
        // Summary:
        //     Use the path of the current system's temporary folder.
        TempDirectory = 0,
        //
        // Summary:
        //     Use the path for a user's application data.
        LocalUserApplicationDirectory = 1,
        //
        // Summary:
        //     Use the path for the application data that is shared among all users.
        CommonApplicationDirectory = 2,
        //
        // Summary:
        //     Use the path for the executable file that started the application.
        ExecutableDirectory = 3,
        //
        // Summary:
        //     If the string specified by Microsoft.VisualBasic.Logging.FileLogTraceListener.CustomLocation
        //     is not empty, then use it as the path. Otherwise, use the path for a user's
        //     application data.
        Custom = 4,
    }
}
