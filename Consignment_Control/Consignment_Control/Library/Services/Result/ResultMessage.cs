using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consignment_Control.Library.Services.Result
{
    public enum ResultMessage
    {
        /// <summary>
        /// Representing login successful
        /// </summary>
        Successful = 1,
        /// <summary>
        /// Representing System error
        /// </summary>
        SystemError = 2,
        /// <summary>
        /// Representing wrong password
        /// </summary>
        CheckingError = 3,
    
      
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