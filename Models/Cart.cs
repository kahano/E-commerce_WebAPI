using Stripe;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace E_commercial_Web_RESTAPI.Models
{
    public class Cart : BaseEntity

    {

      
        public List<BasketItem> BasketItems { get; set; }= new List<BasketItem>();

    
        public long amount { get; set; }

        //[ForeignKey("payment")]
        //public long paymentId {  get; set; }
        
        //public Payment payment { get; set; }
        //public string sources { get; set; }

        //public string Currency {  get; set; }

        //public string? ClientSecret { get; set; }
        //public string? PaymentIntentId { get; set; }

        


        [Required]
        public long customerId { get; set; }

      

    }
}
