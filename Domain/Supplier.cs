using Inventory.Abstract;
using System;
using System.Collections.Generic;

namespace Inventory.Domain
{
    public class Supplier:BaseEntity
    {
        public Supplier()
        {
            EntityGuid = Guid.NewGuid();
        }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public List<ContactPerson> ContactPersons { get; set; }
    }
}
