using System.Threading.Tasks;

namespace Domain.Identity
{
    public interface IAuthenticationService<T> where T : IIdentityUser
    {
        public string DefaultAuthenticationToken { get; set; }
        public bool IsUserLoggedIn { get; set; }
        public string AuthenticationToken { get; set; }
        public T User { get; set; }
        public Task<T> SignInAsync(string username, string password);
        public Task SignOutAsync();
        public IUserManager<T> UserManager { get; set; }

        public Task<T> SignInWithAuthToken(string token);
        public Task<T> SignUpAsync(T user, string password);
    }
}