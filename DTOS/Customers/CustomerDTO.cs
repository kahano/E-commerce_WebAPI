using E_commercial_Web_RESTAPI.DTOS.Payments;
using E_commercial_Web_RESTAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace E_commercial_Web_RESTAPI.DTOS.Customers
{
    public record CustomerDTO(

        long Id ,
        [Required]
        [MaxLength(25, ErrorMessage = "Name can not be over 25 characters")]
        string Name,

        [Required]
        [MaxLength(10, ErrorMessage = "PhoneNumber can not be over 10 digits")]
        string PhoneNumber ,

        List<PaymentDTO> payments

        );

}
