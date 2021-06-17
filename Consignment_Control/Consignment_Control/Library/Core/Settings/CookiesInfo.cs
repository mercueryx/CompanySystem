using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consignment_Control.Library.Core.Settings
{
    public partial class CookiesInfo
    {
        /// <summary>
        /// User Setting Cookies's name
        /// </summary>
        public const string Name = "Base.User";

        /// <summary>
        /// To store the user guid, for user setting cookies
        /// </summary>
        public const string USERPREFERENCENAME = "User";

        /// <summary>
        /// To store the user login date, for user setting cookies
        /// </summary>
        public const string SESSIONDATEPREFERENCE = "SessionDate";

        /// <summary>
        /// To Store the notification at the ViewData
        /// </summary>
        public const string NOTIFICATIONTEMPNAME = "{0}";

        /// <summary>
        /// PreCache / Httpcontext 's Name
        /// </summary>
        public const string PRECACHENAME = "sys_cache_per_request";

        /// <summary>
        /// persistent cookies setting date 
        /// </summary>
        public const int COOKIES_LONGTERM_SETTING_DAYS = 30;
    }
}