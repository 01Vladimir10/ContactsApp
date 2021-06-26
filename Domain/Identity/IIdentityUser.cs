namespace Domain.Identity
{
    public interface IIdentityUser
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public bool IsEnabled { get; set; }
        public bool ForceResetPasswordAtNextLogin { get; set; }
    }
}