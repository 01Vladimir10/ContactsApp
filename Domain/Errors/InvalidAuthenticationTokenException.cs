using System;

namespace Domain.Errors
{
    public class InvalidAuthenticationTokenException : Exception
    {
        
        public InvalidAuthenticationTokenException() : base("The authentication token is not valid.")
        {
        }
        public InvalidAuthenticationTokenException(Exception cause) : base("The authentication token is not valid.", cause)
        {
        }
    }
}