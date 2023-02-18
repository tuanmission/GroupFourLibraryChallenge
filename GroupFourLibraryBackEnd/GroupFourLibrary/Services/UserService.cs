using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GroupFourLibrary.ViewModels;
namespace GroupFourLibrary.Services
{
    public interface IUserService
    {
        public Task<UserServiceResponse> LoginUser(LoginUserViewModel viewMdl);

    }
    public class UserService :IUserService
    {
        private UserManager<IdentityUser> _usermgr;

        private RoleManager<IdentityRole> _rolemgr;
        private IConfiguration _configuration;

        public UserService(UserManager<IdentityUser> manager, RoleManager <IdentityRole> rolemanager, IConfiguration conf)
        {
            this._usermgr = manager;
            this._rolemgr = rolemanager;
            this._configuration = conf;
        }

        public async Task<UserServiceResponse> LoginUser(LoginUserViewModel viewMdl)
        {
            if(viewMdl == null)
            {
                throw new NullReferenceException("Model is null!");
            }

            var user = await _usermgr.FindByNameAsync(viewMdl.UserName);
            if(user == null)
            {
                return new UserServiceResponse
                {
                    IsSuccess = false,
                    Message = "User Doesn't exist"
                };

            }

            var passwordResult = await _usermgr.CheckPasswordAsync(user, viewMdl.Password);
            if (!passwordResult)
            {
                return new UserServiceResponse
                {
                    IsSuccess = false,
                    Message = "Invalid password!"
                };
            }

            string key = _configuration["AuthSettings:Key"];
            var roles = await _usermgr.GetRolesAsync(user);
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim("UserName", user.UserName));
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            } 

            var encryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],
                audience: _configuration["Authsettings:Audience"],
                claims: claims.ToArray(),
                expires: DateTime.Now.AddHours(2),
                signingCredentials: new SigningCredentials(encryptionKey, SecurityAlgorithms.HmacSha256)
                );
            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserServiceResponse
            {
                Message = tokenAsString,
                IsSuccess = true,
                ExpireDate = token.ValidTo
            };


        }

    }
}
