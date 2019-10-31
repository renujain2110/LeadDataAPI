using LeadDataAPI.Helpers;
using LeadDataAPI.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LeadDataAPI.Services
{
    /// <summary>
    /// User service for authentication
    /// </summary>
    public class UserService : IUserService
    {
        //Sample data
        private List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "Test1", Username = "test1", Password = "test" },
            new User { Id = 2, FirstName = "Test2", Username = "test2", Password = "test" }
        };

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Authenticates the user
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <returns>Matching user object/null</returns>
        public User Authenticate(string username, string password)
        {
           
            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            //Before returning user, lets remove the password
            user.Password = null;
            return user;
        }

    }
}
