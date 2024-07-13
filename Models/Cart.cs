using Stripe;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace E_commercial_Web_RESTAPI.Models
{
    public class Cart : BaseEntity

    {

      
        public IEnumerable<CartItem> Items { get; set; }= new List<CartItem>();

    
        public long amount { get; set; }

        
        public string? SessionId { get; set; }
        
        public string? PaymentId { get; set; }

        [Required]
        public string UserId { get; set; }

      

    }
}
