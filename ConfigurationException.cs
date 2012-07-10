using System;

namespace Youzee.Util.Logger
{
    /**
     * Configuration exception raised when the configuration is not valid
     * 
     * @copyright (C) Youzee (2012)
     * @author Keyvan Akbary <keyvan@youzee.com>
     * @version 1.0
     */
    public class ConfigurationException : SystemException
    {
        public ConfigurationException()
            : base("You must define log rules")
        {
        }
    }
}
