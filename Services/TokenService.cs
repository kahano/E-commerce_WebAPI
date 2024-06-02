using E_commercial_Web_RESTAPI.Models;
using E_commercial_Web_RESTAPI.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_commercial_Web_RESTAPI.Services
{
    //public class TokenService : ITokenService
    //{
        //private readonly IConfiguration _configuration;
        //private readonly SymmetricSecurityKey _key;

        //public TokenService(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //    _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]));


        //}
        //public string createToken(AppUser user)
        //{
        //    var claims = new List<Claim>()
        //    {
        //        new Claim(JwtRegisteredClaimNames.Email,user.Email),
        //        new Claim(JwtRegisteredClaimNames.GivenName,user.UserName)
              


        //    };
        //    var cryptedCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {

        //        Subject = new ClaimsIdentity(claims),
        //        Expires = DateTime.Now.AddDays(7),
        //        SigningCredentials = cryptedCredentials,
        //        Issuer = _configuration["JWT:Issuer"],
        //        Audience = _configuration["JWT:Audience"]
        //    };

        //    var tokenhandler = new JwtSecurityTokenHandler();
        //    var token = tokenhandler.CreateToken(tokenDescriptor);
        //    return tokenhandler.WriteToken(token);


        //}
    //}
}
