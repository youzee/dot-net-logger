using System.Collections;

namespace Youzee.Util.Logger
{
    /**
     * Logger configuration
     * 
     * @copyright (C) Youzee (2012)
     * @author Keyvan Akbary <keyvan@youzee.com>
     * @version 1.0
     */
    public interface LoggerConfiguration
    {
        /**
         * @param string level
         */
        ArrayList GetLogElementsForLevel(string level);
    }
}
