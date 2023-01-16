using Inventory.Abstract;
using System;
using System.Collections.Generic;

namespace Inventory.Domain
{
    public class Order:BaseEntity
    {
        public Order()
        {
            EntityGuid = Guid.NewGuid();
        }

        private List<Item> _Items;
        public List<Item> Items
        {
            get 
            {
                if (_Items is null)
                    _Items = new List<Item>();

                return _Items;
            }
            set { _Items = value; }
        }
       public string OrderNumber { get; set; }
        public Supplier Supplier { get; set; }
    }
}
