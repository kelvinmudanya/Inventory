

namespace Inventory.Domain
{
    public class OrderNumber
    {
        public OrderNumber(string orderNumbers)
        {
            OrderNumbers = orderNumbers;

        }
        public int Id { get; set; }
        public string OrderNumbers { get; set; }
    }
}
