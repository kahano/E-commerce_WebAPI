using E_commercial_Web_RESTAPI.DTOS.Customers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace E_commercial_Web_RESTAPI.Models
{
    public class Payment : BaseEntity
    {
 
        public string Currency { get; set; }

     

        public long amount { get; set; }

        public long CustomerId { get; set; } 

        public Customer customer { get; set; }

        public string? AppUserId { get; set; }
        public AppUser User { get; set; }

        [ForeignKey("Cart")]
        public long basketId { get; set; }
        public Cart Cart { get; set; }

        public string source { get; set; } 

        public string description { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.Now;


    }
}
