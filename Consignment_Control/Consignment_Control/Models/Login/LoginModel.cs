using Consignment_Control.Library.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using Consignment_Control.Validation.Login;

namespace Consignment_Control.Models.Login
{

    [Validator(typeof(LoginValidator))]
    public class LoginModel : BaseModel
    {

        [AllowHtml]
        [DisplayName("Username")]
        public string Username { get; set; }

        [AllowHtml]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }
    }
}