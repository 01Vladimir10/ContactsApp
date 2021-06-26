using System;

namespace Domain.Errors
{
    public class AccountDisabledException : Exception
    {
        public override string Message { get; } = "The account is disabled, please contact the administrator.";
    }
}