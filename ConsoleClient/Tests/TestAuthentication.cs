using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Services;
using Domain.Common;
using FaunaAuthentication.Services;

namespace ConsoleClient.Tests
{
    public class TestAuthentication : ITest
    {
        private string Token { get; }
        public TestAuthentication(string token)
        {
            Token = token;
            Errors = new List<Exception>();
        }

        public string Name { get; } = "Authentication Test";

        public async Task Execute()
        {
            WasSuccessful = true;
            try
            {
                await TestCredential("enabled@test.com", "Pass1234!");
            }
            catch (Exception e)
            {
                HandleError(new Exception("Could not log in with correct credentials", e));
            }
            
            try
            {
                await TestCredential("enabled@test.com", "Pass123!");
                HandleError(new Exception("Incorrect credentials worked!"));
            }
            catch (Exception _)
            {
                // ignored
            }
            try
            {
                await TestCredential("disabled@test.com", "Pass1234!");
                HandleError(new Exception("Login to disabled account was successful."));
            }
            catch (Exception _)
            {
                // ignored
            }
        }
        
        private async Task TestCredential(string username, string password)
        {
            var service = new AuthenticationService(new UserManager(Token),  Token);
            await service.SignInAsync(username, password);
            await service.SignOutAsync();
        }
        private void HandleError(Exception exception)
        {
            WasSuccessful = false;
            Errors.Add(exception);
        }
        public bool WasSuccessful { get; set; }
        public IList<Exception> Errors { get; set; }
    }
}