using System.Net.Http;
using System.Threading.Tasks;
using Domain.Identity;
using Domain.Model;
using FaunaDB.Client;
using FaunaDB.Types;
using static FaunaDB.Query.Language;

namespace FaunaAuthentication.Services
{
    public class UserManager<T> : IUserManager<T> where T: IIdentityUser
    {
        private string _token;
        private string EndPoint { get;}
        private HttpClient HttpClient { get; }

        private const string UsersCollection = "Users";
        public string Token
        {
            get => _token;
            set
            {
                _token = value;
                // Update toke on the database.
                _db = new FaunaClient(value, EndPoint, httpClient: HttpClient);
            }
        }

        private const string CreateUserFunction = "CreateUser";
        private FaunaClient _db;

        public UserManager(string token, HttpClient client = null, string endPoint = "https://db.fauna.com:443")
        {
            HttpClient = client ?? new HttpClient();
            EndPoint = endPoint;
            Token = token;
        }

        public async Task<T> CreateUserAsync(T user, string password)
        {
            var data = Encoder.Encode(user);
            var nUser = await _db.ExecuteQuery<T>(Call(Function(CreateUserFunction), data, password));
            return nUser;        
        }

        public async Task<T> UpdateUserAsync(T user)
        {
            var data = Encoder.Encode(user);
            var updatedUser = await _db.ExecuteQuery<T>(Update(Ref(Collection(UsersCollection), user.UserId), Obj("data", data)), "data");
            return updatedUser;
        }

        public async Task<T> UpdateUserAsync(T user, string password)
        {
            var data = Encoder.Encode(user);
            var result = await _db.ExecuteQuery<T>(Update(
                Ref(Collection("Users"), user.UserId), 
                Obj("data", data, "password", password)
                ),
                "data");
            return result;
        }

        public async Task<T> GetUserAsync(string userId)
        {
            return await _db.ExecuteQuery<T>(Get(Ref(Collection(UsersCollection), userId)), "data");
        }

        public async Task DeleteUserAsync(T user)
        {
            await _db.Query(Delete(Ref(Collection(UsersCollection), user.UserId)));
        }
    }
}