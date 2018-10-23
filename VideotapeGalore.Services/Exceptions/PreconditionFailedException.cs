using System;

namespace VideotapeGalore.Services.Exceptions
{
    public class PreconditionFailedException : Exception
    {
        public PreconditionFailedException()
        {
        }

        public PreconditionFailedException(string message) : base(message)
        {
        }
    }
}