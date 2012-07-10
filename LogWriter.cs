using System.IO;

namespace Youzee.Util.Logger
{
    /**
     * Log writer
     * 
     * @copyright (C) Youzee (2012)
     * @author Keyvan Akbary <keyvan@youzee.com>
     * @version 1.0
     */
    public interface LogWriter
    {
        /**
         * @param Log log
         * @param LogElement logElement
         */
        void WriteLog(Log log, LogElement logElement);
    }
}
