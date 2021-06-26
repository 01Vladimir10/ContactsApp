using System;

namespace Domain.Errors
{
    public class InvalidCredentials : Exception
    {
        public override string Message { get; } = "Invalid username or password.";
    }
}