using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace Consignment_Control.Library.Services.Common
{
    public interface ICookieService
    {
        /// <summary>
        /// Get Users cookies
        /// </summary>
        /// <returns></returns>
        HttpCookie GetUserCookie();

        /// <summary>
        /// Set Cookies to null / Reset
        /// </summary>
        /// <param name="values">Collections</param>
        /// <param name="removeValues">Remove Value</param>
        void SetCookie(NameValueCollection values, IList<string> removeValues = null);

        /// <summary>
        /// Set Value into Cookies
        /// </summary>
        /// <param name="key">Cookies Name</param>
        /// <param name="value">Cookies Value</param>
        void SetCookie(string key, string value);

        /// <summary>
        /// Set User Value into Cookies
        /// </summary>
        /// <param name="username"></param>
        void SetUserCookie(string username);

        /// <summary>
        /// Set Session Date Into Cookies
        /// </summary>
        /// <param name="sessionDate"></param>
        void SetSessionDateCookie(DateTime sessionDate);

        /// <summary>
        /// Clear All Cookies Value
        /// </summary>
        void ClearAuthorizationDetails();
    }
}