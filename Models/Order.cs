using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace E_commercial_Web_RESTAPI.Models
{
    public class Order : BaseEntity
    {
     
        public long CustomerId { get; set; }

        public long paymentId { get; set; }
        public string Address { get; set; }
        public List<OrderItem> OrderItems { get;  set; } = new List<OrderItem>();

        [Column(TypeName = "decimal(18,2)")]
        public decimal total { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public OrderStatus Status { get; set; } = OrderStatus.Pending; // default 

        //[JsonIgnore]
        //[IgnoreDataMember]

        //public Payment Payment { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]

        public Customer Customer { get; set; }
    }
}
