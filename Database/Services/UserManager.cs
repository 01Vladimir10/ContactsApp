using System.Net.Http;
using System.Threading.Tasks;
using Domain.Model;
using Domain.Services;
using FaunaDB.Client;
using FaunaDB.Types;
using static FaunaDB.Query.Language;

namespace Database.Services
{
    public class UserManager : IUserManager<AppUser>
    {
        private string _token;
        public string EndPoint { get;}
        public HttpClient HttpClient { get; }

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

        public async Task<AppUser> CreateUserAsync(AppUser user, string password)
        {
            var data = Encoder.Encode(user);
            var nUser = await _db.ExecuteQuery<AppUser>(Call(Function(CreateUserFunction), data, password));
            return nUser;        
        }

        public async Task<AppUser> UpdateUserAsync(AppUser user)
        {
            var data = Encoder.Encode(user);
            var updatedUser = await _db.ExecuteQuery<AppUser>(Update(Ref(Collection(UsersCollection), user.UserId), Obj("data", data)), "data");
            return updatedUser;
        }

        public async Task<AppUser> UpdateUserAsync(AppUser user, string password)
        {
            var data = Encoder.Encode(user);
            var result = await _db.ExecuteQuery<AppUser>(Update(
                Ref(Collection("Users"), user.UserId), 
                Obj("data", data, "password", password)
                ),
                "data");
            return result;
        }

        public async Task<AppUser> GetUserAsync(string userId)
        {
            return await _db.ExecuteQuery<AppUser>(Get(Ref(Collection(UsersCollection), userId)), "data");
        }

        public async Task DeleteUserAsync(AppUser user)
        {
            await _db.Query(Delete(Ref(Collection(UsersCollection), user.UserId)));
        }
    }
}