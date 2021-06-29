using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model;

namespace Domain.Data
{
    public interface IContactsService
    {
        public Task<IEnumerable<Contact>> GetContactsByUser(string userId);
        public Task<IEnumerable<Contact>> GetActiveContactsByUser(string userId);
        public Task<IEnumerable<Contact>> GetDeletedContactsByUser(string userId);
        public Task<IPaginator<Contact>> GetContactsPaginatorByUser(string userId, int size = 50);
        public Task<IPaginator<Contact>> GetActiveContactsPaginatorByUser(string userId, int size = 50);
        public Task<IPaginator<Contact>> GetDeletedContactsPaginatorByUser(string userId, int size = 50);
        public Task<Contact> CreateContact(Contact contact);
        public Task DeleteContact(Contact contact);
        public Task DeleteContact(IEnumerable<Contact> contact);
        public Task DeleteContact(string contactId);
        public Task PermanentlyDeleteContact(Contact contact);
        public Task PermanentlyDeleteContact(IEnumerable<Contact> contact);
        public Task PermanentlyDeleteContact(string contactId);
        public Task<Contact> UpdateContact(Contact contact);
        public Task<IEnumerable<Contact>> UpdateContact(IEnumerable<Contact> contact);
        public Task RestoreContact(Contact contact);
        public Task RestoreContact(string contactId);
        public Task RestoreContact(IEnumerable<Contact> contacts);

    }
}