using Consignment_Control.Library.Data;
using Consignment_Control.Library.Framework.Controllers;
using Consignment_Control.Library.Services.Authentication;
using Consignment_Control.Library.Services.StockTake;
using Consignment_Control.Library.Services.Users;
using Consignment_Control.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Consignment_Control.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserAccountService _userAccountService;
        private readonly IUserService _userService;
   
        public HomeController(
           IAuthenticationService authenticationService
           , IUserAccountService userAccountService
            , IUserService userService
        
        )
        {
         
            this._authenticationService = authenticationService;
            this._userAccountService = userAccountService;
            this._userService = userService;
         
        }

        public ActionResult Index()
        {
          
            return View();
        }

    

        public ActionResult Login()
        {

        

            var model = new LoginModel();

            this._userService.CheckAdministratorExist();

            return View(model);
         
        }


        [HttpPost]
        public ActionResult Login(LoginModel model, string ReturnUrl = "")
        {
            if (ModelState.IsValid)
            {
                var result = this._userAccountService.ValidateUser(model.Username, model.Password);
                switch (result)
                {
                    case UserLoginResults.Successful:
                        var user = this._userService.GetUserByUsername(model.Username);
                        this._authenticationService.SignIn(user, true);

                        if (!string.IsNullOrEmpty(ReturnUrl))
                            return Redirect(ReturnUrl);
                        else
                            return RedirectToRoute("HomePage");
                    case UserLoginResults.WrongPassword:
                        //base.ErrorNotification("Incorrect Password.");
                        base.ModelState.AddModelError("Password", "Incorrect Password.");
                        break;
                    case UserLoginResults.UserNotExists:
                        base.ModelState.AddModelError("Username", "Username not exists.");
                        break;
                  
                }
            }

            return View(model);
        }
    }
}