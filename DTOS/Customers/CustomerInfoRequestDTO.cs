using System.ComponentModel.DataAnnotations;

namespace E_commercial_Web_RESTAPI.DTOS.Customers
{
    public class CustomerInfoRequestDTO
    {

        [Required]
        [MaxLength(25, ErrorMessage = "Name can not be over 25 characters")]
        public required  string Name { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "PhoneNumber can not be over 10 digits")]
        public required string PhoneNumber { get; set; }
    };

}
