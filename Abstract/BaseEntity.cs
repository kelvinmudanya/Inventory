using System;


namespace Inventory.Abstract
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public string CreatedBy { get; set; }
        public Guid EntityGuid { get; set; }
    }
}
