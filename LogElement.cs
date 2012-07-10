using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Youzee.Util.Logger
{
    /**
     * Single log configuration element 
     * 
     * @copyright (C) Youzee (2012)
     * @author Keyvan Akbary <keyvan@youzee.com>
     * @version 1.0
     */
    public class LogElement : ConfigurationElement
    {
        private const string LEVEL_PARAMETER_NAME = "level";
        private const string FILE_PATH_PARAMETER_NAME = "filePath";
        private const string DISABLED_PATH_PARAMETER_NAME = "disabled";

        private const string TRUE_VALUE = "true";
        private const string FALSE_VALUE = "false";

        [ConfigurationProperty(LEVEL_PARAMETER_NAME, IsRequired = true)]
        public string Level
        {
            get
            {
                return (string)this[LEVEL_PARAMETER_NAME];
            }
            set
            {
                this[LEVEL_PARAMETER_NAME] = value;
            }
        }

        [ConfigurationProperty(FILE_PATH_PARAMETER_NAME, IsRequired = false)]
        public string FilePath
        {
            get
            {
                return (string)this[FILE_PATH_PARAMETER_NAME];
            }
            set
            {
                this[FILE_PATH_PARAMETER_NAME] = value;
            }
        }

        [ConfigurationProperty(DISABLED_PATH_PARAMETER_NAME, IsRequired = false)]
        public string Disabled
        {
            get
            {
                return (string)this[DISABLED_PATH_PARAMETER_NAME];
            }
            set
            {
                this[DISABLED_PATH_PARAMETER_NAME] = value;
            }
        }

        /**
         * @return bool
         */
        public bool IsEnabled()
        {
            return Disabled != TRUE_VALUE;
        }

        /**
         * Constructor
         */
        public LogElement()
        {
        }

        /**
         * Constructor
         * 
         * @param string level
         * @param string filePath
         */
        public LogElement(string level, string filePath)
        {
            Level = level;
            FilePath = filePath;
        }

        /**
         * Constructor
         * 
         * @param string level
         * @param string filePath
         * @param bool disabled
         */
        public LogElement(string level, string filePath, bool disabled)
        {
            Level = level;
            FilePath = filePath;
            Disabled = (disabled) ? TRUE_VALUE : FALSE_VALUE;
        }
    }
}
