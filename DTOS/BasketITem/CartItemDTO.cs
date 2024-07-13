using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_commercial_Web_RESTAPI.DTOS.BasketITem
{
    public class CartItemDTO
    {
        public long Id { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

  
        public string imageurl { get; set; }



    }
}
