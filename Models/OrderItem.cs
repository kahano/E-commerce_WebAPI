using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace E_commercial_Web_RESTAPI.Models
{
    public class OrderItem : BaseEntity
    {
        


        public string ProductName { get; set; }

        public string PictureUrl { get; set; }

        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [ForeignKey("Order")]
        [JsonIgnore]
        [IgnoreDataMember]
        public long OrderId { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public Order Order { get; set; }

        [ForeignKey("Product")]
        [JsonIgnore]
        [IgnoreDataMember]
        public long ProductId { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public Product Product { get; set; }
    }
}
