using System.Threading.Tasks;
using Domain.Model;

namespace Domain.Services
{
    public interface IAuthenticationService<T>
    {
        public string DefaultAuthenticationToken { get; set; }
        public bool IsUserLoggedIn { get; set; }
        public string AuthenticationToken { get; set; }
        public AppUser User { get; set; }
        public Task<T> SignInAsync(string username, string password);
        public Task SignOutAsync();
        public IUserManager<T> UserManager { get; set; }

        public Task<AppUser> SignInWithAuthToken(string token);
        public Task<T> SignUpAsync(AppUser user, string password);
    }
}