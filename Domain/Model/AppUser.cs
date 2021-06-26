using Domain.Identity;

namespace Domain.Model {
    public class AppUser : IIdentityUser {
        public string UserId { get; set; }
        public string Username { get; set; }
        
        public bool IsEnabled { get; set; }
        
        public bool ForceResetPasswordAtNextLogin { get; set; }
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public override string ToString()
        {
            return $"AppUser {{UserId: {UserId}, Username: {Username}, IsEnabled: {IsEnabled}, ForceResetPasswordAtNextLogin: {ForceResetPasswordAtNextLogin}, DisplayName: {DisplayName}, Name: {Name}, LastName: {LastName}}}";
        }
    }
}