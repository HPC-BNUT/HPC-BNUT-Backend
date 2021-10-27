using System;

namespace Framework.Domain.Exceptions
{
    public class InvalidEntityStateException : Exception
    {
        public InvalidEntityStateException(object entity, string message)
            : base($"Can not change state of {entity} because of {message}.")
        {
        }
    }
}