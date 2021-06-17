using Consignment_Control.Library.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consignment_Control.Library.Data.SQLDomain.Users
{
    [System.ComponentModel.DataAnnotations.Schema.Table("user_account")]
    public partial class User   
    {
        /// <summary>
        /// Get or Set id
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Get or Set name
        /// </summary>
        public string Name { get; set; }



        /// <summary>
        /// Get or Set username
        /// </summary>
        public string Username { get; set; }


        /// <summary>
        /// Get or Set email
        /// </summary>
        public string Email { get; set; }


        /// <summary>
        /// Get or Set pwd_salt
        /// </summary>
        public string Pwd_salt { get; set; }

        /// <summary>
        /// Get or Set password
        /// </summary>
        public string Pwd { get; set; }

        /// <summary>
        /// Get or Set company
        /// </summary>
        public string Com { get; set; }


        /// <summary>
        /// Get or Set add time
        /// </summary>

        public DateTime Add_dt { get; set; }

        /// <summary>
        /// Get or Set add user
        /// </summary>
        public string Add_usn { get; set; }


        //public override string ToString()
        //{
        //    return "ID: " + this.Id.ToString() +
        //        ", Name: " + this.Name.ToString() +
        //        ", Username: " + this.Username.ToString() +
        //        ", Email: " + this.Email.ToString() +      
        //        ", PasswordSalt: " + this.Pwd_salt.ToString() +
        //        ", Password: " + this.Pwd.ToString() +            
        //        ", Company: " + this.Com.ToString() +
        //        ", Add Time: " + this.Add_dt.ToString("yyyy-MM-dd HH:mm:ss") +
        //        ", Add User: " + this.Add_usn.ToString();
        //}

    }
}