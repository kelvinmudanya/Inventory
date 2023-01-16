using Inventory.Abstract;
using System;
using System.Collections.Generic;

namespace Inventory.Domain
{
    public class Sale:BaseEntity
    {
        public Sale()
        {
            EntityGuid = Guid.NewGuid();
        }
        public List<Item> Items
        {
            get { return Items ??= new List<Item>(); }
            set { Items = value; }
        }

        public Customer Customer { get; set; }
        public string SalesNumber { get; set; }

    }
}
