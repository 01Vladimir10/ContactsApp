using System;
using System.Collections.Generic;
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
        private AuthenticationService<AppUser> auth;

        public Program()
        {
            auth = new AuthenticationService<AppUser>(new UserManager<AppUser>(DefaultToken), DefaultToken);
        }
        
        static void Main(string[] args)
        {
            new Program().Init().Wait();
        }

        private async Task Init()
        {
            await SignInWithToken();
            await DeleteContact();
        }

        private async Task DeleteContact()
        {
            Console.WriteLine("Deleting contacts....");
            var contactsService = new ContactsService(auth.AuthenticationToken);
            await contactsService.DeleteContact(await contactsService.GetActiveContactsByUser(auth.User.UserId));
        }

        private async Task GetContacts()
        {
            Console.WriteLine("Fetching contacts....");
            var contactsService = new ContactsService(auth.AuthenticationToken);
            foreach (var contact in await contactsService.GetActiveContactsByUser(auth.User.UserId))
                Console.WriteLine(contact.DisplayName);
            Console.WriteLine("Done!");
        }

        private async Task PaginateContacts()
        {
            Console.WriteLine("Paginating contacts....");
            var contactsService = new ContactsService(auth.AuthenticationToken);
            var paginator = await contactsService.GetContactsPaginatorByUser(auth.User.UserId, 1);
            while (true)
            {
                Console.WriteLine($"Page #{paginator.CurrentPage.PageNumber}");
                Console.WriteLine("------------");
                foreach (var c in paginator.CurrentPage.Items)
                    Console.WriteLine(c.DisplayName);
                Console.WriteLine();
                if (!paginator.HasNextPage) break; 
                await paginator.NextPage();
            }
            Console.WriteLine("Reverse paging....");
            while (true)
            {
                Console.WriteLine($"Page #{paginator.CurrentPage.PageNumber}");
                Console.WriteLine("------------");
                foreach (var c in paginator.CurrentPage.Items)
                    Console.WriteLine(c.DisplayName);
                Console.WriteLine();
                if (!paginator.HasPreviousPage) break; 
                await paginator.PreviousPage();
            }
        }

        private async Task CreateContacts()
        {
            var contactsService = new ContactsService(auth.AuthenticationToken);
            foreach (var contact in MockContacts(10))
            {
                var c = await contactsService.CreateContact(contact);
                Console.WriteLine($"Contact created => {c}");
            }
        }
        private static IEnumerable<Contact> MockContacts(int count)
        {
            var contacts = new List<Contact>();
            for (var i = 0; i < count; i++)
            {
                contacts.Add(new Contact
                {
                    DisplayName = $"Contact #{i}",
                    IsDeleted = false,
                    IsFavorite = false,
                    NickName = $"El Contact #{i}",
                    Emails = new []
                    {
                        new Email
                        {
                            EmailAddress = $"work{i}@domain.com",
                            Label = "Work"
                        },
                        new Email
                        {
                            EmailAddress = $"personal{i}@domain.com",
                            Label = "Personal"
                        }
                    },
                    PhoneNumbers = new []
                    {
                        new PhoneNumber
                        {
                            Number = $"{i}000000000000",
                            Label = "Work"
                        },
                        new PhoneNumber
                        {
                            Number = $"{i}1111111111111",
                            Label = "Personal"
                        }
                    }
                });
            }

            return contacts;
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
            var user = await auth.SignInWithAuthToken("fnEEMrCZ5gACAAQxwBTXkAYEs8ZcqV199LrAvj5O7Rfv3zcRQ_8");
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