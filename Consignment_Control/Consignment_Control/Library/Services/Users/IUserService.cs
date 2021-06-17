using Consignment_Control.Library.Data.SQLDomain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consignment_Control.Library.Services.Users
{
    public partial interface IUserService
    {
        /// <summary>
        /// Get user by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>User or null if record not found</returns>
        /// 
        User GetUserByUsername(string username);

        void CheckAdministratorExist();

    }
}