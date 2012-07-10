using System.Configuration;

namespace Youzee.Util.Logger
{
    /**
     * Log configuration element collection 
     * 
     * @copyright (C) Youzee (2012)
     * @author Keyvan Akbary <keyvan@youzee.com>
     * @version 1.0
     */
    class LogCollection : ConfigurationElementCollection
    {
        private const string LOG_ELEMENT_NAME = "log"; 

        protected override ConfigurationElement CreateNewElement()
        {
            return new LogElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((LogElement)element).Level;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }

        protected override string ElementName
        {
            get
            {
                return LOG_ELEMENT_NAME;
            }
        }
    }
}
