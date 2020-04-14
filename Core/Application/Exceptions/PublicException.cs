using System;

namespace Application.Exceptions
{
    public abstract class PublicException : Exception
    {
        protected PublicException()
        {
        }

        protected PublicException(string message) : base(message)
        {
        }

        protected PublicException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}