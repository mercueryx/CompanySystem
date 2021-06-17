using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consignment_Control.Library.Core.Settings
{
    public static class SecurityInfo
    {
        /// <summary>
        /// Password Key
        /// </summary>
        public const string SECURE_PRIVATE_KEY = "9476839857392056";

        /// <summary>
        /// Password SALT Length
        /// </summary>
        public const int PASSWORD_SALT_LENGTH = 10;

        /// <summary>
        /// Password Encrypt Length
        /// </summary>
        public const int PASSWORD_LENGTH = 10;
    }
}