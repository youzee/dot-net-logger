using System.Collections;
using System.Collections.Generic;
using System;

namespace Youzee.Util.Logger
{
    /**
     * Logger configuration based on a configuration file
     * 
     * @copyright (C) Youzee (2012)
     * @author Keyvan Akbary <keyvan@youzee.com>
     * @version 1.0
     */
    public class FileLoggerConfiguration : LoggerConfiguration
    {
        private const string LOGGER_SECTION_NAME = "logger";

        private Dictionary<string, ArrayList> LogLevelFiles;

        /**
         * Constructor
         */
        public FileLoggerConfiguration()
        {
            LogLevelFiles = new Dictionary<string, ArrayList>();
            ExtractLogConfiguration();
        }

        private void ExtractLogConfiguration()
        {
            try
            {
                LogCollection logCollection = GetLogCollectionFromConfiguration();

                foreach (LogElement log in logCollection)
                {
                    LoadLogElementIntoLogLevelFiles(log);
                }
            }
            catch (NullReferenceException e)
            {
                throw new ConfigurationException();
            }
        }

        private void LoadLogElementIntoLogLevelFiles(LogElement log)
        {
            ArrayList logs;
            if (!LogLevelFiles.TryGetValue(log.Level, out logs))
            {
                logs = new ArrayList();
                LogLevelFiles.Add(log.Level, logs);
            }
            logs.Add(log);
        }

        private LogCollection GetLogCollectionFromConfiguration()
        {
            LoggerSection loggerSection = (LoggerSection)System.Configuration.ConfigurationManager.GetSection(LOGGER_SECTION_NAME);
            return loggerSection.Logger;
        }

        /**
         * @see LoggerConfiguration
         */
        public ArrayList GetLogElementsForLevel(string level)
        {
            ArrayList logElements;

            if (!LogLevelFiles.TryGetValue(level, out logElements))
            {
                throw new LogLevelConfigurationNotFoundException(level);
            }
            return logElements;
        }
    }
}
