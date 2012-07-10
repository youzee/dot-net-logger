using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;

namespace Youzee.Util.Logger
{
    /**
     * Youzee simple logger
     * 
     * @copyright (C) Youzee (2012)
     * @author Keyvan Akbary <keyvan@youzee.com>
     * @version 1.1
     */
    public class Logger
    {
        public const string WILDCARD_LOG_FILENAME = "*";

        private LogWriter LogWriter;
        private LoggerConfiguration LoggerConfiguration;

        public Logger(LogWriter logWriter, LoggerConfiguration loggerConfiguration)
        {
            LogWriter = logWriter;
            LoggerConfiguration = loggerConfiguration;
        }

        public void Error(string message)
        {
            Log log = new Log(Youzee.Util.Logger.Log.ERROR_LEVEL, message);
            WriteLogForEveryLogLevelConfiguration(log);
        }

        public void Debug(string message)
        {
            Log log = new Log(Youzee.Util.Logger.Log.DEBUG_LEVEL, message);
            WriteLogForEveryLogLevelConfiguration(log);
        }

        public void Info(string message)
        {
            Log log = new Log(Youzee.Util.Logger.Log.INFO_LEVEL, message);
            WriteLogForEveryLogLevelConfiguration(log);
        }

        private void WriteLogForEveryLogLevelConfiguration(Log log)
        {
            ArrayList logElements = GetLogElementsForLogLevel(log.Level);
            foreach (LogElement logElement in logElements)
            {
                WriteLogIfLogElementIsEnabled(log, logElement);
            }
        }

        private void WriteLogIfLogElementIsEnabled(Log log, LogElement logElement)
        {
            if (logElement.IsEnabled())
            {
                LogWriter.WriteLog(log, logElement);
            }
        }

        private ArrayList GetLogElementsForLogLevel(string logLevel)
        {
            try
            {
                return LoggerConfiguration.GetLogElementsForLevel(logLevel);
            }
            catch (LogLevelConfigurationNotFoundException)
            {
                return LoggerConfiguration.GetLogElementsForLevel(WILDCARD_LOG_FILENAME);
            }
        }
    }
}
