using E_commercial_Web_RESTAPI.Helpers;
using E_commercial_Web_RESTAPI.Models;
using E_commercial_Web_RESTAPI.Repositories;

namespace E_commercial_Web_RESTAPI.Interfaces
{
    public interface ICart : IRepository<Cart>
    {
        Cart CreateCart(string UserId);
       
        Cart? GetCart(string UserId);
       

      
     





    }
}
