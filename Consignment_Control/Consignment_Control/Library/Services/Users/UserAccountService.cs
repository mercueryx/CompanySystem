using Consignment_Control.Library.Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consignment_Control.Library.Services.Users
{
    public partial class UserAccountService : IUserAccountService
    {

        #region Fields
        private readonly IUserService _userService;
        private readonly IEncryptionService _encryptionService;
        #endregion

        #region Ctor

        public UserAccountService( IUserService userService, IEncryptionService encryptionService)
        {
        
            this._userService = userService;
            this._encryptionService = encryptionService;
        }
        #endregion

        public UserLoginResults ValidateUser(string username, string password)
        {
            var user = this._userService.GetUserByUsername(username);
            if (user == null)
                return UserLoginResults.UserNotExists;
          

            // Check Password
            string hashedPassword = this._encryptionService.CreatePasswordHash(password, user.Pwd_salt);

            if (hashedPassword != user.Pwd)
                return UserLoginResults.WrongPassword;

            return UserLoginResults.Successful;
        }
    }
}