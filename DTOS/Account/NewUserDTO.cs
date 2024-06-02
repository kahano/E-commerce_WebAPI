using System.ComponentModel.DataAnnotations;

namespace E_commercial_Web_RESTAPI.DTOS.Account
{
    public class NewUserDTO
    {
        public UserDTO User { get; set; }   


        public string token { get; set; }
      
    }
}
