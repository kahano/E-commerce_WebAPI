using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_commercial_Web_RESTAPI.DTOS.BasketITem
{
    public class BasketItemDTO
    {
        public long Id { get; set; }
        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

  
        public string imageurl { get; set; }

    
        public string Brand { get; set; }

    
        public string Type { get; set; }

    }
}
