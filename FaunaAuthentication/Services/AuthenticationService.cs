using System;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Errors;
using Domain.Identity;
using FaunaDB.Client;
using FaunaDB.Types;
using static FaunaDB.Query.Language;

namespace FaunaAuthentication.Services
{
    public class AuthenticationService<TUser> : IAuthenticationService<TUser> 
        where TUser : IIdentityUser
    {
        private const string SignInFunction = "SignIn";
        public string DefaultAuthenticationToken { get; set; }
        public bool IsUserLoggedIn { get; set; }
        private string EndPoint { get; set; }
        private HttpClient HttpClient { get; set; }
        private string _authToken;
        public string AuthenticationToken
        {
            get => _authToken;
            set
            {
                _authToken = value;
                UserManager.Token = value;
                _db = new FaunaClient(value, EndPoint, httpClient: HttpClient);
            }
        }

        public TUser User { get; set; }

        private FaunaClient _db;
        public IUserManager<TUser> UserManager { get; set; }

        private readonly ILogger<AuthenticationService<TUser>> _l;
        
        public AuthenticationService(IUserManager<TUser> manager, string defaultToken, HttpClient client = null, string endPoint = "https://db.fauna.com:443", ILogger<AuthenticationService<TUser>> logger = null)
        {
            HttpClient = client ?? new HttpClient();
            _l = logger ?? new BasicLogger<AuthenticationService<TUser>>();
            EndPoint = endPoint;
            UserManager = manager;
            DefaultAuthenticationToken = defaultToken;
            AuthenticationToken = defaultToken;
        }


        public async Task<TUser> SignInWithAuthToken(string token)
        {
            IsUserLoggedIn = false;
            try
            {
                AuthenticationToken = token;
                // The current user is this.
                var userRef = await _db.ExecuteQuery<RefV>(CurrentIdentity());
                var user = await UserManager.GetUserAsync(userRef.Id);
                if (user == null) throw new InvalidAuthenticationTokenException();
                IsUserLoggedIn = true;
                return user;
            }
            catch (Exception e)
            {
                AuthenticationToken = DefaultAuthenticationToken;
                HandleError("Invalid authentication token.", e);
                throw new InvalidAuthenticationTokenException(e);
            }
        }

        public async Task<TUser> SignUpAsync(TUser user, string password)
        {
            try
            {
                return await UserManager.CreateUserAsync(user, password);
            }
            catch (Exception e)
            {
                HandleError("Could not sign up", e);
                throw;
            }
        }

        public async Task<TUser> SignInAsync(string username, string password)
        {
            try
            {
                var result = await _db.Query(Call(Function(SignInFunction), username, password));
                var token = result.To<string>("secret");
                // Update authentication token.
                AuthenticationToken = token;
                // Get user profile from the database provider.
                var userRef = result.To<RefV>("instance");
                var user = await UserManager.GetUserAsync(userRef.Id);
                // the authentication was successful.
                User = user;
                IsUserLoggedIn = true;
                return user;
            }
            catch (Exception e)
            {
                HandleError("Could not sign in", e);
                if (e.Message.Contains("disabled"))
                    throw new AccountDisabledException();
                throw new InvalidCredentials();
            }
        }
        
        private void HandleError(string action, Exception e)
        {
            action += $"=> {e.Message} => {e}";
            _l.E(action);
        }

        public async Task SignOutAsync()
        {
            try
            {
                await _db.Query(Logout(true));
                AuthenticationToken = DefaultAuthenticationToken;
                IsUserLoggedIn = false;
            }
            catch (Exception e)
            {
                HandleError("Could not sign out.", e);
                throw;
            }
        }

    }
}