using E_commercial_Web_RESTAPI.DTOS.Cart;
using E_commercial_Web_RESTAPI.Models;
using Stripe;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace E_commercial_Web_RESTAPI.DTOS.Payments
{
    public class PaymentDTO
    {

        [Required]
        [Range(50, 1000000000)]
        public long amount { get; set; }
        [Required]
        public string? source { get; set; }

        [Required]
        public string Currency { get; set; }



        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public long customerId { get; set; } 
         
        public string CreatedBy { get; set; } 

       
    }
}