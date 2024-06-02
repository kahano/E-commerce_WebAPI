
using E_commercial_Web_RESTAPI.Data;
using E_commercial_Web_RESTAPI.DTOS.Account;
using E_commercial_Web_RESTAPI.Models;
using E_commercial_Web_RESTAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System.Diagnostics.Eventing.Reader;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_commercial_Web_RESTAPI.Controllers
{
    [ApiController]
    [Route("api/account")]
 
    public class AccountController : ControllerBase
    {

        private readonly ApplicationDBcontext _context;

        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

     

        private readonly SignInManager<AppUser> _signInManager;

        private readonly IConfiguration _configuration;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager
            , IConfiguration configuration , ApplicationDBcontext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;


        }

        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody] RegisterDTO registerdto)
        {

           

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                };

                var appuser = new AppUser()
                {

                    UserName = registerdto.UserName,
                    Email = registerdto.Email,
                
                };

                try
                {

                    IdentityResult result = await _userManager.CreateAsync(appuser, registerdto.Password);

                    if (result.Succeeded)
                    {

                 
                         var roleResult = result;
                    
                        if( await _roleManager.FindByNameAsync(Role.Admin.ToString()) is null)
                        {
                            await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
                            roleResult = await _userManager.AddToRoleAsync(appuser, Role.Admin.ToString());

                        }
                       
                        
                        else

                        {
                           await _roleManager.CreateAsync(new IdentityRole { Name = "Customer" });
                           roleResult = await _userManager.AddToRoleAsync(appuser, Role.Customer.ToString());
                        }

                        

                        await _signInManager.SignInAsync(appuser, isPersistent: false);
                        if (roleResult.Succeeded)
                        {
                            return Ok(

                                new UserDTO
                                {

                                    UserName = appuser.UserName,
                                    Email = appuser.Email,
                                });
                        }
                        else
                        {
                            return StatusCode(500, roleResult.Errors);
                        }
                      

                    }
                    else
                    {
                        return StatusCode(500, result.Errors);
                    }

                    
                        
                           
                }
                catch (Exception e)
                {
                    return StatusCode(500,e.Message);
                
                }
            
        }

        [HttpPost("login")]

        public async Task<IActionResult> LoginDTO([FromBody] LoginDTO login)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(s => s.UserName == login.UserName);

            if (user == null) return Unauthorized("Invalid Username!");

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);

            if (!result.Succeeded) return Unauthorized("Username is not found and /or password not found");


            else
            {
                var roles = await _userManager.GetRolesAsync(user);
                var claims = new List<Claim>();

                claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
                claims.Add(new Claim(JwtRegisteredClaimNames.GivenName, user.UserName));
                claims.Add(new Claim(ClaimTypes.Role, roles.FirstOrDefault()));
                  



            
                var _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]));
                var cryptedCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
                var tokenDescriptor = new SecurityTokenDescriptor
                {

                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(7),
                    SigningCredentials = cryptedCredentials,
                    Issuer = _configuration["JWT:Issuer"],
                    Audience = _configuration["JWT:Audience"]
                };

                var tokenhandler = new JwtSecurityTokenHandler();
                var token = tokenhandler.CreateToken(tokenDescriptor);
                var tok = tokenhandler.WriteToken(token);
                return Ok(

                new NewUserDTO
                {

                    User = new UserDTO { UserName = user.UserName, Email = user.Email },
                    token = tok

                }); ;
            }

            

        }
    }

}
