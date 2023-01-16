using Inventory.Domain.Dto;
using System.Collections.Generic;

namespace Inventory.ViewModel
{
    public class UpdateSupplierViewModel
    {

        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public List<ContactPersonDto> ContactPersons { get; set; }
    }
}
