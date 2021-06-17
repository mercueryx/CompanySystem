using Consignment_Control.Library.Core.Settings;
using Consignment_Control.Library.Data;
using Consignment_Control.Library.Data.SQLDomain.Users;
using Consignment_Control.Library.Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consignment_Control.Library.Services.Users
{
    public partial class UserService : IUserService
    {

        #region Fields
        private readonly IEncryptionService _encryptionService;
        //private readonly IRepository<User> _userRepository;
        #endregion

        #region Ctor
        public UserService(IEncryptionService encryptionService
         //, IWebHelper webHelper
         //, IRepository<User> userRepository
         //, IWorkContext workContext
         //, IDbContext dbContext
         //, IDataProvider dataProvider
         )
        {
            this._encryptionService = encryptionService;
            //this._webHelper = webHelper;
            //this._userRepository = userRepository;
            //this._dbContext = dbContext;
            //this._dataProvider = dataProvider;
            //this._workContext = workContext;
        }
        #endregion

        /// <summary>
        /// Get user by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>User or null if record not found</returns>

        public virtual User GetUserByUsername(string username)
        {
            using (var db = new MySqlContext())
            {
                // Insert method
            return db.Users.Where(x => x.Username == username).FirstOrDefault();
              
            }
            //return User.FirstOrDefault(x => x.DeactivatedRecord == false && x.Username == username);
        }

        public virtual void CheckAdministratorExist()
        {
            var adminData = this.GetUserByUsername("administrator");
            if (adminData == null)
            {
                string passwordSalt = this._encryptionService.CreateSaltKey(SecurityInfo.PASSWORD_SALT_LENGTH);
                User administrator = new User()
                {
                    Name = "Administrator",
                    Email = "administrator@mail.com",

                    Com="SP",
                    Pwd = this._encryptionService.CreatePasswordHash("abc123", passwordSalt),
                    Pwd_salt = passwordSalt,
                    Username = "administrator",
                    Add_dt=DateTime.Now,
                    Add_usn="Administrator"

                
                    //UpdatedBy = 1,
                    //UpdatedDate = DateTime.Now,
                   
                };

                using (var db = new MySqlContext())
                {
                    // Insert method
                    db.Users.Add(administrator);
                    db.SaveChanges();
              
                }
            }
        }

    
    }
}