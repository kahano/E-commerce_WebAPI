using E_commercial_Web_RESTAPI.DTOS.OrderItem;

namespace E_commercial_Web_RESTAPI.DTOS.Order
{
    public class OrderDTO
    {
        public long OrderId { get; set; }

        public string customerId { get; set; }
        public string CustomerName { get; set; }

        public string Address { get; set; }

        public decimal total { get; set; }
        public DateTime OrderDate { get; set; }

        public List<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>();
    }
}
