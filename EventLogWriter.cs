using System.IO;
using System;
using System.Diagnostics;
using System.Threading;

namespace Youzee.Util.Logger
{
    /**
     * Event log writer
     * 
     * @copyright (C) Youzee (2012)
     * @author Keyvan Akbary <keyvan@youzee.com>
     * @version 1.0
     */
    public class EventLogWriter : LogWriter
    {
        private const string SOURCE_NAME = "YouzeeLogger";

        /**
         * @see FileWriter
         */
        public void WriteLog(Log log, LogElement logElement)
        {
            if (EventLogSourceDoesNotExist())
            {
                CreateEventLogSourceForLevel(log.Level);
            }

            WriteLogToEventLog(log);
        }

        private bool EventLogSourceDoesNotExist()
        {
            return !EventLog.SourceExists(SOURCE_NAME);
        }

        private void CreateEventLogSourceForLevel(string logLevel)
        {
            EventLog.CreateEventSource(SOURCE_NAME, logLevel);
        }

        private void WriteLogToEventLog(Log log)
        {
            EventLog eventLog = new EventLog();
            eventLog.Source = SOURCE_NAME;
            EventLogEntryType level = DetermineLogEventLevelFromLog(log);
            eventLog.WriteEntry(log.Message, level);
        }

        private EventLogEntryType DetermineLogEventLevelFromLog(Log log)
        {
            if (log.Level == Log.ERROR_LEVEL)
            {
                return EventLogEntryType.Error;
            }

            return EventLogEntryType.Information;
        }
    }
}
