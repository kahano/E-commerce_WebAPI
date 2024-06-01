using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace E_commercial_Web_RESTAPI.Models
{
    public class Customer
    {

        [Key]
        public long Id { get; set; } 

        [Required]
        [MaxLength(25, ErrorMessage = "Name can not be over 25 characters")]
        public  string Name { get; set; } 

        [Required]
        [MaxLength(10, ErrorMessage = "PhoneNumber can not be over 10 digits")]
        public  string PhoneNumber { get; set; } = string.Empty;

        public List<Payment> payments { get; set; } = new List<Payment>();

       

    }
}
