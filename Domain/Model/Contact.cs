using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Model 
{

    public class Contact 
    {
        public string ContactId { get; set; }
        public string UserId { get; set; }
        public string LabelId { get; set; }
        public string DisplayName { get; set; }
        public string NickName { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }
        public string Department { get; set; }
        public string Notes { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Birthday { get; set; }
        public IEnumerable<Email> Emails { get; set; }
        public IEnumerable<Address> Addresses { get; set; }
        public IEnumerable<PhoneNumber> PhoneNumbers { get; set; }
        public IEnumerable<CustomField> CustomFields { get; set; }

        public override string ToString()
        {
            return $"{nameof(ContactId)}: {ContactId}, {nameof(UserId)}: {UserId}, {nameof(LabelId)}: {LabelId}, {nameof(DisplayName)}: {DisplayName}, {nameof(NickName)}: {NickName}, {nameof(Company)}: {Company}, {nameof(Title)}: {Title}, {nameof(Department)}: {Department}, {nameof(Notes)}: {Notes}, {nameof(IsFavorite)}: {IsFavorite}, {nameof(IsDeleted)}: {IsDeleted}, {nameof(Birthday)}: {Birthday}, {nameof(Emails)}: {Emails}, {nameof(Addresses)}: {Addresses}, {nameof(PhoneNumbers)}: {PhoneNumbers}, {nameof(CustomFields)}: {CustomFields}";
        }
    }    

}