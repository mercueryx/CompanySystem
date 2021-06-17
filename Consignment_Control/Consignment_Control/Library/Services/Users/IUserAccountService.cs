using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consignment_Control.Library.Services.Users
{
    public partial interface IUserAccountService
    {
        /// <summary>
        /// Validates user login
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>Login results</returns>
        UserLoginResults ValidateUser(string username, string password);
    }
}