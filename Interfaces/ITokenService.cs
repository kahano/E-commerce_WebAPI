using E_commercial_Web_RESTAPI.Models;

namespace E_commercial_Web_RESTAPI.Interfaces
{
    public interface ITokenService
    {
        string createToken (AppUser user);
    }
}
