using System.ComponentModel.DataAnnotations;

namespace E_commercial_Web_RESTAPI.DTOS.Account
{
    public class UserDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
