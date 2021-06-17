using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Consignment_Control.Library.Core.Settings;
using Consignment_Control.Library.Data.SQLDomain.Users;
using Consignment_Control.Library.Services.Common;
using Consignment_Control.Library.Services.Users;

namespace Consignment_Control.Library.Services.Authentication
{
    public partial class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly HttpContextBase _httpContext;
        private readonly IUserService _userService;
        private readonly ICookieService _cookieService;
        private readonly TimeSpan _expirationTimeSpan;
        private User _cachedUser;
        #endregion

        #region Ctor
        public AuthenticationService(HttpContextBase httpContext, IUserService userService, ICookieService cookieService)
        {

            this._httpContext = httpContext;
            this._userService = userService;
            this._expirationTimeSpan = FormsAuthentication.Timeout;
            this._cookieService = cookieService;

        }


        #endregion
        public User GetAuthenticatedUser()
        {
            if (this._cachedUser != null)
                return this._cachedUser;

            try
            {
                if (this._httpContext == null ||
                    this._httpContext.Request == null ||
                    !this._httpContext.Request.IsAuthenticated ||
                    !(this._httpContext.User.Identity is FormsIdentity))
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }

            var formsIdentity = (FormsIdentity)this._httpContext.User.Identity;
            var user = this.GetAuthenticatedUserFromTicket(formsIdentity.Ticket);
            if (user != null )
                this._cachedUser = user;
            return this._cachedUser;
        }

        public void SignIn(User user, bool createPersistentCookie)
        {
            var now = DateTime.UtcNow.ToLocalTime();
            var ticket = new FormsAuthenticationTicket(1,
                user.Username,
                now,
                createPersistentCookie ? now.Add(_expirationTimeSpan) : now.AddDays(CookiesInfo.COOKIES_LONGTERM_SETTING_DAYS),
                createPersistentCookie,
                user.Username,
                FormsAuthentication.FormsCookiePath);
            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

            cookie.HttpOnly = true;
            if (ticket.IsPersistent)
                cookie.Expires = ticket.Expiration;

            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if (FormsAuthentication.CookieDomain != null)
                cookie.Domain = FormsAuthentication.CookieDomain;

            var usercookies = this._cookieService.GetUserCookie();

            // if cookies setting was null , then Set tthe default value 
            if (usercookies == null)
                _cookieService.SetUserCookie(user.Username);

            this._httpContext.Response.Cookies.Add(cookie);
            this._cachedUser = user;
        }

        public void SignOut()
        {
            this._cachedUser = null;
            this._cookieService.ClearAuthorizationDetails();
            System.Web.HttpContext.Current.Session.Abandon();
            FormsAuthentication.SignOut();
        }

        public virtual User GetAuthenticatedUserFromTicket(FormsAuthenticationTicket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException("ticket");

            var username = ticket.UserData;

            if (String.IsNullOrWhiteSpace(username))
                return null;
            var user = this._userService.GetUserByUsername(username);
            return user;
        }
    }
}