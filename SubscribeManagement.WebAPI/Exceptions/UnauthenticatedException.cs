using System;

namespace SubscribeManagement.WebAPI.Exceptions
{
    public class UnauthenticatedException : ApplicationException
    {
        public UnauthenticatedException() { }

        public UnauthenticatedException(string message) : base(message) { }
    }
}
