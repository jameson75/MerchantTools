using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherPark.TriggerOrange.Core.ApplicationServices
{
    public static class ExceptionExtension
    {
        /// <summary>
        /// Concatenates the messages and stack trace of an exception hierarchy and
        /// returns the results.
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static string GetCompleteDetails(this Exception exception)
        {
            StringBuilder sb = new StringBuilder();
            Exception _ex = exception;
            while(_ex != null) 
            {
                sb.AppendLine(_ex.Message);
                sb.Append(_ex.GetSpecialExceptionDetails());
                sb.AppendLine(_ex.StackTrace);                
                _ex = _ex.InnerException;
            }
            return sb.ToString();
        }

        /// <summary>
        /// Returns information on special types of exceptions (ie: DbUpdateException, WebException) 
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        private static string GetSpecialExceptionDetails(this Exception exception)
        {
            StringBuilder sb = new StringBuilder();
            if(exception is System.Data.Entity.Infrastructure.DbUpdateException)
            {
                //TODO: populate string builder with possible validation error info from exception.
            }
            else if(exception is System.Net.WebException)
            {
                //TODO: populate string builder with network info from exception.
            }
            return sb.ToString();
        }
    }
}
