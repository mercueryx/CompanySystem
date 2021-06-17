using Consignment_Control.Library.Core.Settings;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
namespace Consignment_Control.Library.Services.Common
{
    /// <summary>
    /// Cookies Services
    /// </summary>
    public partial class CookieService : ICookieService
    {
        #region Fields
        private readonly HttpContextBase _httpContext;
        #endregion

        #region Ctor
        /// <summary>
        /// Initiliaze
        /// </summary>
        /// <param name="httpContext">Httpcontext</param>
        public CookieService(HttpContextBase httpContext)
        {
            this._httpContext = httpContext;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Get User Cookies
        /// </summary>
        /// <returns></returns>
        public virtual HttpCookie GetUserCookie()
        {
            if (this._httpContext == null || this._httpContext.Request == null)
                return null;
            return this._httpContext.Request.Cookies[CookiesInfo.Name];
        }

        /// <summary>
        /// Set Value into Cookies
        /// </summary>
        /// <param name="key">Cookies Name</param>
        /// <param name="value">Cookies Value</param>
        public virtual void SetCookie(string key, string value)
        {
            var preferences = new NameValueCollection();
            preferences.Add(key, value);
            this.SetCookie(preferences);
        }

        /// <summary>
        /// Set Cookies Value
        /// </summary>
        /// <param name="values">Value</param>
        /// <param name="removeValues">Value will be remnoved</param>
        public virtual void SetCookie(NameValueCollection values, IList<string> removeValues = null)
        {
            try
            {
                if (removeValues == null)
                    removeValues = new List<string>();

                if (this._httpContext != null)
                {
                    var existingCookie = this.GetUserCookie();
                    var cookie = new HttpCookie(CookiesInfo.Name);
                    cookie.HttpOnly = true;

                    if (values != null)
                        for (var i = 0; i < values.Count; i++)
                            cookie.Values.Add(values.GetKey(i), values[values.GetKey(i)]);

                    if (existingCookie != null && existingCookie.Values != null)
                        for (var i = 0; i < existingCookie.Values.Count; i++)
                            if (!removeValues.Contains(existingCookie.Values.GetKey(i)))
                            {
                                var hasPreference = false;
                                if (cookie.Values != null)
                                    for (var j = 0; j < cookie.Values.Count; j++)
                                        if (existingCookie.Values.GetKey(i).Equals(cookie.Values.GetKey(j), StringComparison.InvariantCultureIgnoreCase))
                                        {
                                            hasPreference = true;
                                            break;
                                        }

                                if (!hasPreference)
                                    cookie.Values.Add(existingCookie.Values.GetKey(i), existingCookie.Values[existingCookie.Values.GetKey(i)]);
                            }

                    cookie.Expires = DateTime.Now.AddDays(CookiesInfo.COOKIES_LONGTERM_SETTING_DAYS);
                    this._httpContext.Response.Cookies.Remove(CookiesInfo.Name);
                    this._httpContext.Response.Cookies.Add(cookie);
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Set user cookies
        /// </summary>
        /// <param name="userGuid">User GUID</param>
        public virtual void SetUserCookie(string userGuid)
        {
            this.SetCookie(CookiesInfo.USERPREFERENCENAME, userGuid);
        }

        /// <summary>
        /// Set Session Date
        /// </summary>
        /// <param name="sessionDate">Date</param>
        public virtual void SetSessionDateCookie(DateTime sessionDate)
        {
            this.SetCookie(CookiesInfo.SESSIONDATEPREFERENCE, sessionDate.ToString());
        }

        /// <summary>
        /// Clear All Cookies
        /// </summary>
        public virtual void ClearAuthorizationDetails()
        {
            var preferences = new NameValueCollection();

            var removeValues = new List<string>();
            removeValues.Add(CookiesInfo.USERPREFERENCENAME);
            this.SetCookie(preferences, removeValues);
        }
        #endregion
    }
}