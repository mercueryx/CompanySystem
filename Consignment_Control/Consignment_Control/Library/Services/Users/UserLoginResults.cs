using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consignment_Control.Library.Services.Users
{
    public enum UserLoginResults
    {
        /// <summary>
        /// Representing login successful
        /// </summary>
        Successful = 1,
        /// <summary>
        /// Representing username is not exists in the db
        /// </summary>
        UserNotExists = 2,
        /// <summary>
        /// Representing wrong password
        /// </summary>
        WrongPassword = 3,
    
      
        /// <summary>
        /// Representing the database occurs some error
        /// </summary>
        DatabaseNoConnection = 6,
        ///// <summary>
        ///// System off - System Maitenance
        ///// </summary>
        //SystemMaintenance = 7
    }
}