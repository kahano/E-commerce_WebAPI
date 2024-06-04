using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace E_commercial_Web_RESTAPI.Models
{
    public class Payment
    {
        public long Id { get; set; }
        public Currency Currency { get; set; }

        public long amount { get; set; }

        public long CustomerId { get; set; }

        public Customer customer { get; set; }

        public int AppUserId { get; set; }
        public AppUser User { get; set; }

        public string? source { get; set; } 

        public string description { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.Now;


    }
}
