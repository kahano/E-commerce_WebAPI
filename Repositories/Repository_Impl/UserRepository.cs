using E_commercial_Web_RESTAPI.Data;
using E_commercial_Web_RESTAPI.DTOS.Account;
using E_commercial_Web_RESTAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using System.Linq.Expressions;

namespace E_commercial_Web_RESTAPI.Repositories.Repository_Impl
{
    //public class UserRepository : IUserRepository
    //{
    //    private readonly UserManager<AppUser> _userManager;
    //    private readonly RoleManager<IdentityRole> _roleManager;
    //    private readonly ITokenService _tokenService;
    //    private ApplicationDBcontext _context;
    //    private readonly SignInManager<AppUser> _signInManager;

    //    public UserRepository(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ITokenService tokenService
    //        , ApplicationDBcontext context, SignInManager<AppUser> signInManager)
    //    {
    //        _userManager = userManager;
    //        _roleManager = roleManager;
    //        _tokenService = tokenService; 
    //        _context = context;
    //        _signInManager = signInManager;

            
    //    }
    //    public async Task<NewUserDTO> Login(LoginDTO login)
    //    {
    //        var user = await _userManager.Users.FirstOrDefaultAsync(s => s.UserName == login.UserName);

    //        var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
    //        if (user is null || !result.Succeeded) return new NewUserDTO();

    //        return 

    //            new NewUserDTO
    //            {

    //                UserName = user.UserName,
    //                Email = user.Email,
    //                token = _tokenService.createToken(user)

    //            };
    //    }

    //    public async Task<NewUserDTO> Register(RegisterDTO register)
    //    {
    //        var appuser = new AppUser()
    //        {
    //            UserName = register.UserName,
    //            Email = register.Email,
    //        };

    //        try {

    //            IdentityResult result = await _userManager.CreateAsync(appuser, register.Password);
    //           // var role = await _userManager.AddToRoleAsync(appuser, Role.Customer.ToString()); // default

    //            if (result.Succeeded)
    //            {
    //                if ( await _roleManager.FindByNameAsync(Role.Admin.ToString()) is null)
    //                {
    //                    await _roleManager.CreateAsync(new IdentityRole(Role.Admin.ToString()));
                        
    //                    await _roleManager.CreateAsync(new IdentityRole(Role.Customer.ToString()));

    //                }
    //                var role = await _userManager.AddToRoleAsync(appuser, Role.Admin.ToString());



    //                await _signInManager.SignInAsync(appuser, isPersistent: false);

    //                if (role.Succeeded)
    //                {
    //                    var userDTO = new NewUserDTO()
    //                    {

    //                        UserName = register.UserName,
    //                        Email = register.Email,
    //                        token = _tokenService.createToken(appuser)

    //                    };

    //                }
    //                else
    //                {

    //                    foreach (IdentityError error in result.Errors)
    //                    {
    //                       throw new ArgumentException($"Errors {error.Description}");
    //                    }
                        
    //                }
    //            }            
                

    //        }catch(Exception e)
    //        {
               
               
    //        }
    //        return new NewUserDTO();
    //    }

       
    //}
}
