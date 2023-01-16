using Inventory.Abstract;
namespace Inventory.Domain
{
    public class Customer:BaseEntity
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
    }
}
