using Consignment_Control.Library.Data.SQLDomain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consignment_Control.Library.Services.Authentication
{
    public partial interface IAuthenticationService
    {
        /// <summary>
        /// Sign in with specific user account
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="createPersistentCookie">Create persistent cookie</param>
        void SignIn(User user, bool createPersistentCookie);

        /// <summary>
        /// Sign out from authenticated user
        /// </summary>
        void SignOut();

        /// <summary>
        /// Gets signed in user
        /// </summary>
        /// <returns>User</returns>
        User GetAuthenticatedUser();

        /// <summary>
        /// Get the user info from the authentication ticket
        /// </summary>
        /// <param name="ticket">Form Ticket</param>
        /// <returns></returns>
        //User GetAuthenticatedUserFromTicket(FormsAuthenticationTicket ticket);
    }
}