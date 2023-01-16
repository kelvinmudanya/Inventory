

using Inventory.Domain.Dto;
using System.Collections.Generic;

namespace Inventory.ViewModel
{
    public class SaleViewModel
    {
        public List<ItemDto> Items { get; set; }
        public CustomerDto Customer { get; set; }
    }
}
