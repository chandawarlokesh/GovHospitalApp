using System;

namespace GovHospitalApp.Core.Application.Exceptions
{
    public abstract class PublicException : Exception
    {
        public PublicException()
        {
        }

        public PublicException(string message) : base(message)
        {
        }

        public PublicException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
