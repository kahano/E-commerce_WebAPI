using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace E_commercial_Web_RESTAPI.Models
{
    public class Order : BaseEntity
    {
     
        public string? customerName { get; set; }
        public string Address { get; set; }
        public List<OrderItem> OrderItems { get;  set; } = new List<OrderItem>();

        [Column(TypeName = "decimal(18,2)")]
        public decimal total { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;

       // public OrderStatus Status { get; set; } = OrderStatus.Pending; // default 

        public string Status = OrderStatus.Pending.ToString();



        [ForeignKey("User")]
        public string UserId { get; set; }
        public AppUser User { get; set; }

        public string? PaymentId { get; set; }


        public static string ConvertToStatusEnum(OrderStatus status)
        {
            return status.ToString();
        }

    }
}
