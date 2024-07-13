using E_commercial_Web_RESTAPI.DTOS.OrderItem;

namespace E_commercial_Web_RESTAPI.DTOS.Order
{
    public class OrderDTO
    {
        public long OrderId { get; set; }

        public string UserId { get; set; }
        
        public string Name { get; set; }

        public string Address { get; set; }

        public decimal total { get; set; }
        public DateTime OrderDate { get; set; }

        public string OrderStatus { get; set; }

        public List<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>();
    }
}
