using System.IO;
using System;

namespace Youzee.Util.Logger
{
    /**
     * Log file writer
     * 
     * @copyright (C) Youzee (2012)
     * @author Keyvan Akbary <keyvan@youzee.com>
     * @version 1.0
     */
    public class FileLogWriter : LogWriter
    {
        private const string DATETIME_FORMAT = "ddd MMM dd HH':'mm':'ss yyyy";
        private const string LOG_LINE_FORMAT = "[{datetime}] [{level}] {message}";

        /**
         * @see FileWriter
         */
        public void WriteLog(Log log, LogElement logElement)
        {
            try
            {
                if (!File.Exists(logElement.FilePath))
                {
                    File.Create(logElement.FilePath);
                }
                WriteLogToFile(log, logElement);
            }
            catch (IOException)
            {
                throw new WritingException(logElement.FilePath);
            }
        }

        private void WriteLogToFile(Log log, LogElement logElement)
        {
            StreamWriter fileDescriptor = File.AppendText(logElement.FilePath);
            string logLine = CreateLogLine(log);
            fileDescriptor.WriteLine(logLine);
            fileDescriptor.Close();
        }

        private string CreateLogLine(Log log)
        {
            return LOG_LINE_FORMAT
                    .Replace("{datetime}", log.CreationDateTime.ToString(DATETIME_FORMAT))
                    .Replace("{level}", log.Level)
                    .Replace("{message}", log.Message);
        }
    }
}
