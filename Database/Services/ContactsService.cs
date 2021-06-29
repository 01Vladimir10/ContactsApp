using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Database.Utils;
using Domain.Data;
using Domain.Model;
using FaunaDB.Client;
using FaunaDB.Query;
using FaunaDB.Types;
using static FaunaDB.Query.Language;

namespace Database.Services
{
    public class ContactsService : IContactsService
    {
        #region Properties
        public string AuthToken
        {
            get => _authToken;
            set
            {
                _authToken = value;
                _db = new FaunaClient(AuthToken, EndPoint, httpClient: Client);
            }
        }
        public string EndPoint { get; set; }
        public HttpClient Client { get; set; }
        #endregion
        #region Members
        private FaunaClient _db;
        private string _authToken;
        #endregion
        #region Connstants
        private const string ContactsByUserIdIndex = "contacts_by_userid";
        private const string ContactsCollection = "Contacts";
        private const string ContactsByUserIdAndStateIndex = "contacts_by_userid_and_state";
        private const string CreateContactFunction = "CreateContact";
        #endregion
        #region Constructor
        public ContactsService(string authToken, string endPoint = "https://db.fauna.com:443", HttpClient client = null)
        {
            EndPoint = endPoint;
            Client = client ?? new HttpClient();
            AuthToken = authToken;
        }
        #endregion
        #region SimpleQueries
        public async Task<IEnumerable<Contact>> GetContactsByUser(string userId)
            => ValueArrayToContact(
                await _db.ExecuteQuery<ArrayV>(
                    Map(
                        Paginate(
                            Match(Index(ContactsByUserIdIndex), userId)
                        ),
                        x => Get(x)
                    ),
                    "data")
            );

        public async Task<IEnumerable<Contact>> GetActiveContactsByUser(string userId)
            => ValueArrayToContact(
                await _db.ExecuteQuery<ArrayV>(
                    Map(
                        Paginate(
                            Match(
                                Index(ContactsByUserIdAndStateIndex),
                                userId,
                                false)
                        ),
                        x => Get(x)
                    ),
                    "data")
            );

        public async Task<IEnumerable<Contact>> GetDeletedContactsByUser(string userId)
            => ValueArrayToContact(
                await _db.ExecuteQuery<ArrayV>(
                    Map(
                        Paginate(
                            Match(
                                Index(ContactsByUserIdAndStateIndex),
                                userId,
                                true)
                        ),
                        x => Get(x)
                    ),
                    "data")
            );

        #endregion
        #region PaginationQueries
        public async Task<IPaginator<Contact>> GetContactsPaginatorByUser(string userId, int size = 50)
            => await new FaunaPaginator<Contact>(
                    _db,
                    Match(Index(ContactsByUserIdIndex), userId),
                    ValueArrayToContact,
                    size)
                .FetchFirstPage();


        public async Task<IPaginator<Contact>> GetActiveContactsPaginatorByUser(string userId, int size = 50)
            => await new FaunaPaginator<Contact>(
                    _db,
                    Match(Index(ContactsByUserIdAndStateIndex), userId, false),
                    ValueArrayToContact,
                    size)
                .FetchFirstPage();

        public async Task<IPaginator<Contact>> GetDeletedContactsPaginatorByUser(string userId, int size = 50)
            => await new FaunaPaginator<Contact>(
                    _db,
                    Match(Index(ContactsByUserIdAndStateIndex), userId, true),
                    ValueArrayToContact,
                    size)
                .FetchFirstPage();

        #endregion
        #region CreateFunctions
        public async Task<Contact> CreateContact(Contact contact)
            => await _db.ExecuteQuery<Contact>(Call(Function(CreateContactFunction), contact.Encode()));
        #endregion
        #region DeletionFunctions
        public async Task DeleteContact(Contact contact)
            => await DeleteContact(contact.ContactId);

        public async Task DeleteContact(IEnumerable<Contact> contacts)
            => await _db.Query(
                Map(
                    contacts.Select(c => c.ContactId).Encode(),
                    contactId => Update(
                        Ref(Collection(ContactsCollection), contactId),
                        Obj("data", Obj("IsDeleted", true))
                    )
                )
            );

        public async Task DeleteContact(string contactId)
            => await _db.Query(
                Update(
                    GetRef(contactId),
                    Obj("data", Obj("IsDeleted", true))
                ));

        public Task PermanentlyDeleteContact(Contact contact)
            => DeleteContact(contact.ContactId);
        
        public async Task PermanentlyDeleteContact(IEnumerable<Contact> contacts)
            => await _db.Query(
                Map(
                    contacts.Select(c => c.ContactId).Encode(),
                    contactId => Delete(
                            Ref(Collection(ContactsCollection), contactId)
                        )
                )
            );

        public async Task PermanentlyDeleteContact(string contactId)
            => await _db.Query(
                Delete(GetRef(contactId))
                );
        #endregion
        #region UpdateFunctions
        public async Task<Contact> UpdateContact(Contact contact)
            => await _db.ExecuteQuery<Contact>(
                Update(
                            GetRef(contact.ContactId),
                            Obj("data", contact.Encode())
                        )
                );

        public async Task<IEnumerable<Contact>> UpdateContact(IEnumerable<Contact> contacts)
            => ValueArrayToContact(
                await _db.ExecuteQuery<ArrayV>(
                        Map(
                            contacts.Encode(), 
                            c => Select(
                                "data",
                                    Update(
                                        Ref(
                                            Collection(ContactsCollection), 
                                            Select("ContactId", c)
                                        ),
                                        Obj("data", c)
                                    )
                                )
                            )
                        )
            );
        #endregion
        #region RestoreFunctions
        public async Task RestoreContact(Contact contact)
            => await RestoreContact(contact.ContactId);
        public async Task RestoreContact(string contactId)
            => await _db.Query(
                Update(
                    GetRef(contactId),
                    Obj("data", Obj("IsDeleted", false))
                )
            );

        public async Task RestoreContact(IEnumerable<Contact> contacts)
            => await _db.Query(
                    Map(
                                contacts.Select(c => c.ContactId).Encode(),
                                c => Update(
                                        Ref(
                                            Collection(ContactsCollection), c
                                            ),
                                        Obj("data", Obj("IsDeleted", false))
                                    )
                        )
                );
        #endregion
        #region Helpers
        private static IEnumerable<Contact> ValueArrayToContact(Value value)
        {
            var array = value.Decode<ArrayV>();
            return array.Select(data => data.At("data").Decode<Contact>()).ToList();
        }
        private static Expr GetRef(string contactId) => Ref(Collection(ContactsCollection), contactId);
        #endregion
    }
}