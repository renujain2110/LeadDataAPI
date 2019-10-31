using LeadDataAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeadDataAPI.Services
{
    /// <summary>
    /// User service for authentication
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Authenticates the user
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <returns>Matching user object/null</returns>
        User Authenticate(string username, string password);
    }
}
