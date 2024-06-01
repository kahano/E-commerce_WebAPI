using E_commercial_Web_RESTAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace E_commercial_Web_RESTAPI.DTOS.Payments
{
    public class PaymentRequestDTO
    {

        [Required]
        [Range(50, 1000000)]
        public long amount { get; set; }


        [Required]
        public string? Currency { get; set; }


        [Required]
        public string source { get; set; } = string.Empty;




    };

}
