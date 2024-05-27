using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace E_commercial_Web_RESTAPI.Models.Payment.Payment
{
    public class Payment
    {
        public long Id { get; set; }
        public Currency Currency { get; set; } 

        public long amount { get; set; }


        [ForeignKey("customer")]
        public long CustomerId { get; set; } 

        public Customer customer { get; set; }

        public string source { get; set; } = string.Empty;

        public string description { get; set; } = string.Empty;

    }
}
