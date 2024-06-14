using System;

namespace Necnat.Abp.NnLibCommon.Exceptions
{
    [Serializable]
    public class SyncException : Exception
    {
        public SyncException() { }

        public SyncException(string message)
            : base(message) { }

        public SyncException(string message, Exception inner)
            : base(message, inner) { }
    }
}
