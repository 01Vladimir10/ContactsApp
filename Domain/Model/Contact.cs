using System;
using System.Collections.Generic;

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
        public DateTime Birthday { get; set; }
        public IEnumerable<Email> Emails { get; set; }
        public IEnumerable<Address> Addresses { get; set; }
        public IEnumerable<PhoneNumber> PhoneNumbers { get; set; }
        public IEnumerable<CustomField> CustomFields { get; set; }

    }    

}