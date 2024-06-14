using System;

namespace Necnat.Abp.NnLibCommon.Helpers.AccessTokenManager
{
    [Serializable]
    public class AccessTokenException : Exception
    {
        public AccessTokenException() { }

        public AccessTokenException(string message)
            : base(message) { }

        public AccessTokenException(string message, Exception inner)
            : base(message, inner) { }
    }
}
