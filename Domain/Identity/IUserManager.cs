using System.Threading.Tasks;

namespace Domain.Identity
{
    public interface IUserManager<T>
    {
        public string Token { get; set; }
        public Task<T> CreateUserAsync(T user, string password);
        public Task<T> UpdateUserAsync(T user);
        public Task<T> UpdateUserAsync(T user, string password);
        
        public Task<T> GetUserAsync(string userId);
        public Task DeleteUserAsync(T user);
        
    }
}