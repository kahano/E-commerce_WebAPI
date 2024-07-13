using E_commercial_Web_RESTAPI.DTOS.OrderItem;
using E_commercial_Web_RESTAPI.Models;
using Stripe;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commercial_Web_RESTAPI.DTOS.Order
{
    public class OrderRequestDTO
    {
 
        public string UserId { get; set; }


        public string Address { get; set; }

  
   



    }
}
