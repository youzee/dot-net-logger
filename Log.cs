using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Youzee.Util.Logger
{
    /**
     * Class wich represents a log
     * 
     * @copyright (C) Youzee (2012)
     * @author Keyvan Akbary <keyvan@youzee.com>
     * @version 1.0
     */
    public class Log
    {
        public const string ERROR_LEVEL = "error";
        public const string DEBUG_LEVEL = "debug";
        public const string INFO_LEVEL = "debug";

        public string Level
        {
            get;
            private set;
        }

        public string Message
        {
            get;
            private set;
        }

        public DateTime CreationDateTime {
            get;
            private set;
        }

        /**
         * @param string logLevel
         * @param string message
         */
        public Log(string level, string message)
        {
            Level = level;
            Message = message;
            CreationDateTime = DateTime.Now;
        }
    }
}
