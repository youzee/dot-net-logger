using System;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Youzee.Util.Logger;
using Moq;

namespace YouzeeUtilLoggerTest
{
    /**
     * Test suite for Logger class
     * 
     * @copyright (C) Youzee (2012)
     * @author Keyvan Akbary <keyvan@youzee.com>
     * @version 1.0
     */
    [TestClass]
    public class LoggerTest
    {
        private Mock<LoggerConfiguration> LoggerConfigurationMock;
        private Mock<LogWriter> LogWriterMock;

        [TestInitialize()]
        public void SetUp()
        {
            LoggerConfigurationMock = new Mock<LoggerConfiguration>();
            LogWriterMock = new Mock<LogWriter>();
        }

        [TestMethod]
        [ExpectedException(typeof(ConfigurationException))]
        public void UndefinedLogElementConfigurationShouldThrowConfigurationException()
        {
            ConfigurationShouldNotHaveInfoLogElement();
            ConfigurationShouldNotHaveDebugLogElement();
            ConfigurationShouldNotHaveErrorLogElement();
            ConfigurationShouldNotHaveWildcardLogElement();

            Logger logger = new Logger(LogWriterMock.Object, LoggerConfigurationMock.Object);
            logger.Info("test");
        }

        private void ConfigurationShouldNotHaveInfoLogElement()
        {
            ConfigurationShouldNotHaveLogElementWithLogLevel(Logger.INFO_LEVEL);
        }

        private void ConfigurationShouldNotHaveLogElementWithLogLevel(string logLevel)
        {
            LoggerConfigurationMock
                .Setup(loggerConfiguration => loggerConfiguration.GetLogElementsForLevel(logLevel))
                .Throws<ConfigurationException>();
        }

        private void ConfigurationShouldNotHaveDebugLogElement()
        {
            ConfigurationShouldNotHaveLogElementWithLogLevel(Logger.DEBUG_LEVEL);
        }

        private void ConfigurationShouldNotHaveErrorLogElement()
        {
            ConfigurationShouldNotHaveLogElementWithLogLevel(Logger.ERROR_LEVEL);
        }

        private void ConfigurationShouldHaveLogElement(LogElement logElement)
        {
            ArrayList list = new ArrayList();
            list.Add(logElement);

            LoggerConfigurationMock
                .Setup(loggerConfiguration => loggerConfiguration.GetLogElementsForLevel(logElement.Level))
                .Returns(list);
        }

        private void ConfigurationShouldNotHaveWildcardLogElement()
        {
            LoggerConfigurationMock
                .Setup(loggerConfiguration => loggerConfiguration.GetLogElementsForLevel(Logger.WILDCARD_LOG_FILENAME))
                .Throws<ConfigurationException>();
        }

        [TestMethod]
        public void ShouldWriteToWildcardLogFile()
        {
            ConfigurationShouldHaveOnlyWildcardLogElementWithFileName("all");
            LogWriterShouldWriteToLogFile("all");

            Logger logger = new Logger(LogWriterMock.Object, LoggerConfigurationMock.Object);
            logger.Info("test");
            logger.Debug("test");
            logger.Error("test");

            LogWriterMock.VerifyAll();
        }

        private void LogWriterShouldWriteToLogFile(string filePath)
        {
            LogWriterMock
               .Setup(logWriter => logWriter.WriteLog(
                   It.IsAny<Log>(),
                   It.Is<LogElement>(logElement => (logElement.FilePath == filePath))
               ))
               .Verifiable();
        }

        private void ConfigurationShouldHaveOnlyWildcardLogElementWithFileName(string fileName, bool disabled = false)
        {
            ConfigurationShouldNotHaveInfoLogElement();
            ConfigurationShouldNotHaveDebugLogElement();
            ConfigurationShouldNotHaveErrorLogElement();
            ConfigurationShouldHaveLogElement(new LogElement(Logger.WILDCARD_LOG_FILENAME, fileName, disabled));
        }

        private void LogWriterShouldWriteToLogLevelFile(string logLevel)
        {
            LogWriterMock
                .Setup(logWriter => logWriter.WriteLog(
                    It.Is<Log>(log => (log.Level == logLevel)),
                    It.IsAny<LogElement>()
                ))
                .Verifiable();
        }

        [TestMethod]
        public void ShouldWriteToErrorLogFile()
        {
            ConfigurationShouldHaveLogElement(new LogElement(Logger.ERROR_LEVEL, "error"));

            LogWriterShouldWriteToLogLevelFile("error");

            Logger logger = new Logger(LogWriterMock.Object, LoggerConfigurationMock.Object);
            logger.Error("test");

            LogWriterMock.VerifyAll();
        }

        [TestMethod]
        public void ShouldWriteToInfoLogFile()
        {
            ConfigurationShouldHaveLogElement(new LogElement(Logger.INFO_LEVEL, "info"));

            LogWriterShouldWriteToLogLevelFile("info");

            Logger logger = new Logger(LogWriterMock.Object, LoggerConfigurationMock.Object);
            logger.Info("test");

            LogWriterMock.VerifyAll();
        }

        [TestMethod]
        public void ShouldWriteToDebugLogFile()
        {
            ConfigurationShouldHaveLogElement(new LogElement(Logger.DEBUG_LEVEL, "debug"));

            LogWriterShouldWriteToLogLevelFile("debug");

            Logger logger = new Logger(LogWriterMock.Object, LoggerConfigurationMock.Object);
            logger.Debug("test");

            LogWriterMock.VerifyAll();
        }

        [TestMethod]
        public void DisabledLogShouldNotWriteAnyLogFile()
        {
            ConfigurationShouldHaveOnlyWildcardLogElementWithFileName("all", true);

            LogWriterShouldNotWriteToLogLevelFile("all");

            Logger logger = new Logger(LogWriterMock.Object, LoggerConfigurationMock.Object);
            logger.Debug("test");
        }

        private void LogWriterShouldNotWriteToLogLevelFile(string logLevel)
        {
            LogWriterMock
                .Setup(LogWriter => LogWriter.WriteLog(
                    It.Is<Log>(log => (log.Level == logLevel)),
                    It.IsAny<LogElement>()
                ))
                .Throws<ConfigurationException>();
        }
    }
}
