using System;
using System.Threading.Tasks;
using Database.Services;
using Domain.Model;
using FaunaAuthentication.Services;

namespace ConsoleClient
{
    
    /*
     *
     * sam@untome.com
     * Pass1234!
     */
    class Program
    {
        public static readonly string DefaultToken = "fnAEMmSopXACA7m95-mzKsKLVuly_vUPP-iHBRcQ";
        private AuthenticationService auth;

        public Program()
        {
            auth = new AuthenticationService(new UserManager(DefaultToken), DefaultToken);
        }
        
        static void Main(string[] args)
        {
            new Program().Init().Wait();
        }

        private async Task Init()
        {
            await SignInWithToken();
        }

        private async Task SignIn()
        {
            Console.WriteLine("Signing in as vlad@domain.com...");
            var user = await auth.SignInAsync("vlad@domain.com", "Pass1234$");
            Console.WriteLine("Welcome! ");
            Console.WriteLine($"Token: {auth.AuthenticationToken}");
            Console.WriteLine($"User: {user}");
        }

        private async Task<AppUser> SignInWithToken()
        {
            Console.WriteLine("Signing in with token...");
            var user = await auth.SignInWithAuthToken("fnEEMmpTlZACAwQxwBTXkAYEMkDAnMUKSsWfObYC2t7l88BO0_U");
            Console.WriteLine($"Welcome {user.Name}");
            Console.WriteLine(user);
            return user;
        }
        
    
        private async Task<AppUser> CreateUser()
        {
            var user = new AppUser
            {
                Username = "vlad@domain.com",
                DisplayName = "Vladimir Gonzalez",
                LastName = "Gonzalez",
                Name = "Vladimir"
            };
            var pass = "Pass1234$";
            var createdUser = await auth.SignUpAsync(user, pass);
            return createdUser;
        }
    }
}