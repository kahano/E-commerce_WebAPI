using E_commercial_Web_RESTAPI.DTOS.BasketITem;
using E_commercial_Web_RESTAPI.Models;

namespace E_commercial_Web_RESTAPI.DTOS.Cart
{
    public class CartDTO
    {
        public long Id { get; set; }

        public long amount { get; set; }
        public List<CartItemDTO> BasketItems { get; set; } = new List<CartItemDTO>();
        public string Currency { get; set; }

        public string sources { get; set; } 
    }
}
