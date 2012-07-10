using System.Configuration;

namespace Youzee.Util.Logger
{
    /**
     * Log configuration section
     * 
     * @copyright (C) Youzee (2012)
     * @author Keyvan Akbary <keyvan@youzee.com>
     * @version 1.0
     */
    class LoggerSection : ConfigurationSection
    {
        public LoggerSection()
        {
        }

        [ConfigurationProperty("", IsDefaultCollection = true)]
        public LogCollection Logger
        {
            get
            {
                return (LogCollection)base[""];
            }
        }
    }
}
