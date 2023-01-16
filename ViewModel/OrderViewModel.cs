

using Inventory.Domain.Dto;
using System;
using System.Collections.Generic;

namespace Inventory.ViewModel
{
    public class OrderViewModel
    {
        public Guid SupplierGuid { get; set; }
        public List<ItemDto> Item { get; set; }
    
    }
}
