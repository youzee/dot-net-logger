using System;

namespace Youzee.Util.Logger
{
    /**
     * Writing exception raised when it cant write over a log
     * 
     * @copyright (C) Youzee (2012)
     * @author Keyvan Akbary <keyvan@youzee.com>
     * @version 1.0
     */
    class WritingException : SystemException
    {
        public WritingException(string filePath)
            : base(String.Format("Cannot write log to {0}", filePath))
        {
        }
    }
}
