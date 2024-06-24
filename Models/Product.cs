using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace E_commercial_Web_RESTAPI.Models
{
    public class Product : BaseEntity 
    {
        [MaxLength(300, ErrorMessage = "Name can not be over 200 characters")]
        public string Name { get; set; }

        [MaxLength(1000, ErrorMessage = "Description can not be over 500 characters")]
        public string Description { get; set; }

        //[JsonIgnore]
        //[IgnoreDataMember]
        public int Stock {  get; set; }

        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal price { get; set; }

        [JsonIgnore]
       // [IgnoreDataMember]
        public ICollection<Order> orders { get; set; } = new List<Order>();

        //[JsonIgnore]
        //[IgnoreDataMember]
        public int Quantity { get; set; }

        //[JsonIgnore]
        //[IgnoreDataMember]
        public string? imageurl { get; set; }
    }
}
