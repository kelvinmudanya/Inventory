
using Inventory.Abstract;

namespace Inventory.Domain
{
    public class ReceiveItem:BaseEntity
    {
        public Item Item { get; set; }
        public decimal Amount { get; set; }
    }
}
