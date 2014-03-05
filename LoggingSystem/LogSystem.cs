using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;
using LoggingSystem.Interfaces;
using LoggingSystem.Models;
using NLog;

namespace LoggingSystem
{
    public class LogSystem
    {
        private readonly ILogPublisherFactory _logPublisherFactory;
        public Guid UserId { get; set; }

        public LogSystem(Guid userId, ILogPublisherFactory logPublisherFactory)
        {
            _logPublisherFactory = logPublisherFactory;
            UserId = userId;
        }


        public void Log(Logger logger, LoggingLevel level, string message, KeyValueCollection nameValueCollection, string sku = "Undefined", [CallerMemberName] string caller = "", [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string sourceFilePath = "")
        {
            OutputLog(null, logger, level,message,nameValueCollection,sku,caller, sourceLineNumber, sourceFilePath);
        }
        public void Log(Exception exception, Logger logger, LoggingLevel level, string message, KeyValueCollection nameValueCollection, string sku = "Undefined", [CallerMemberName] string caller = "", [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string sourceFilePath = "")
        {
            OutputLog(exception, logger, level, message, nameValueCollection, sku, caller, sourceLineNumber, sourceFilePath);
        }

        private void OutputLog(Exception exception, Logger logger, LoggingLevel level, string message, KeyValueCollection nameValueCollection, string sku, string caller, int sourceLineNumber, string sourceFilePath)
        {
            try
            {
                var logPublisher = _logPublisherFactory.CreatePublisher(level, logger);
                if (logPublisher.IsLogLevelEnabled == false) return;

                var formattedMessage = GenerateJsonFormattedMessage(caller, nameValueCollection, sku, message, exception, level.ToString(), sourceLineNumber, sourceFilePath);

                if (exception == null)
                {
                    logPublisher.LogMessage(formattedMessage);
                }
                else
                {
                    logPublisher.LogException(formattedMessage, exception);
                }

            }
            // ReSharper disable EmptyGeneralCatchClause
            catch (Exception)
            // ReSharper restore EmptyGeneralCatchClause
            {
                //Error logging should never cause exceptions
            }

        }

        private string GenerateJsonFormattedMessage(string caller, IEnumerable<KeyValue> nameValueCollection, string sku, string message, Exception ex, string logType, int sourceLineNumber, string sourceFilePath)
        {
            dynamic expandoObject = new ExpandoObject();
            expandoObject.LogType = logType;
            expandoObject.DateTime = CurrentDateTime();
            expandoObject.Method = caller;
            expandoObject.LineNumber = sourceLineNumber;
            expandoObject.UserId = UserId;
            expandoObject.Message = message;
            if (sku != "Undefined")
            {
                expandoObject.Sku = sku;
            }
            AddParamsToExpando(nameValueCollection, expandoObject);
            if (ex != null)
            {
                expandoObject.Exception = ex.ToString();
            }
            expandoObject.FilePath = sourceFilePath;

            string formattedMessage = (Newtonsoft.Json.JsonConvert.SerializeObject(expandoObject)).ToString();
            return formattedMessage;
        }

        private void AddParamsToExpando(IEnumerable<KeyValue> nameValueCollection, dynamic expandoObject)
        {
            if (nameValueCollection == null) return;

            var dictionaryExpando = expandoObject as IDictionary<String, Object>;

            foreach (var nameValue in nameValueCollection)
            {
                dictionaryExpando[nameValue.Key] = nameValue.Value;
            }
        }

        private static DateTime CurrentDateTime()
        {
            return AmbientDate.HasValue ? AmbientDate.GetValueOrDefault() : DateTime.Now;
        }

        public static DateTime? AmbientDate = null;

    }
}
