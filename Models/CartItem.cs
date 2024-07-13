using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace E_commercial_Web_RESTAPI.Models
{
    public class CartItem : BaseEntity
    {
        
       
        [Required]
        [MaxLength(25, ErrorMessage = "Name can not be over 25 characters")]
        public string ProductName { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [Required]
        public string? imageurl { get; set; }

     
        public string? Brand { get; set; }

    
        public string? Type { get; set; }

        [ForeignKey("Product")]
        [JsonIgnore]
        [IgnoreDataMember]
        public long ProductId { get; set; }
        public Product Product { get; set; }


        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        public AppUser User { get; set; }

        [ForeignKey("Cart")]
        [JsonIgnore]
        [IgnoreDataMember]
        public long CartId { get; set; }
        public Cart Cart { get; set;}
    }
}
