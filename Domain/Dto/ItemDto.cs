
using System;

namespace Inventory.Domain.Dto
{
    public class ItemDto
    {
        public Guid EntityGuid { get; set; }
        public int AmountOrdered { get; set; }
    }
}
