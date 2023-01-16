using Inventory.Abstract;
using System;

namespace Inventory.Domain
{
    public class ContactPerson:BaseEntity
    {
        public ContactPerson()
        {
            EntityGuid = Guid.NewGuid();
        }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}