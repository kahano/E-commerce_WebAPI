using System.ComponentModel.DataAnnotations;

namespace E_commercial_Web_RESTAPI.Helpers
{
    public class CustomerQueryObject
    {
       
        public string Id { get; set; }

        public string? Name { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
