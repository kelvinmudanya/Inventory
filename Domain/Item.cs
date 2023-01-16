using Inventory.Abstract;
using System;
using System.Collections.Generic;

namespace Inventory.Domain
{
    public class Item:BaseEntity
    {
        public Item()
        {
           EntityGuid = Guid.NewGuid();
        }
        public string Status { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal AmountInStock { get; set; }
        public decimal AMountOrdered { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public List<Order> Orders { get; set; }
        public List<Sale> Sales { get; set; }
        public Category Category { get; set; }
    }
}