using Inventory.Abstract;


namespace Inventory.Domain
{
    public class User:BaseEntity
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string SecondaryPhoneNumber { get; set; }
        public string EmailAdress { get; set; }
        public string Location { get; set; }
    }
}
