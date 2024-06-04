using E_commercial_Web_RESTAPI.Models;
using Stripe;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace E_commercial_Web_RESTAPI.DTOS.Payments
{
    public class PaymentDTO
    {
        public long Id { get; set; }

        [Required]
        [Range(50, 1000000)]
        public long amount { get; set; }
        [Required]
        public string? source { get; set; } 

        [Required]
        public string Currency { get; set; } = string.Empty;


        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public long? CustomerId { get; set; }

        public string? CreatedBy { get; set; } 

       
    }
}