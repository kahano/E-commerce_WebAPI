using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_commercial_Web_RESTAPI.DTOS.OrderItem
{
    public class OrderItemDTO
    {
     
        public long ProductItemId { get; set; }

        public string ProductName { get; set; }

        public string PictureUrl { get; set; }

     
        public decimal Price { get; set; }


        public int Quantity { get; set; }
    }
}
