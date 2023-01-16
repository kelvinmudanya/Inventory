using Inventory.Abstract;
using System;
using System.Collections.Generic;

namespace Inventory.Domain
{
    public class Category:BaseEntity
    {
        public Category()
        {
            EntityGuid = Guid.NewGuid();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Item> Items { get; set; }
    }
}
