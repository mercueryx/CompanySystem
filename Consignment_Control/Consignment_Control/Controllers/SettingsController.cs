using BasePlatform.Library.Framework.Controllers;
using Consignment_Control.Library.Data.SQLDomain.Settings;
using Consignment_Control.Library.Framework.Controllers;
using Consignment_Control.Library.Framework.Listing;
using Consignment_Control.Library.Services.Authentication;
using Consignment_Control.Library.Services.Settings;
using Consignment_Control.Library.Setting;
using Consignment_Control.Models.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Consignment_Control.Controllers.Settings
{
    public class SettingsController : BaseAuthorizeController
    {
        private readonly ISettingsService _settingsService;
      
        private readonly IAuthenticationService _authenticationService;
        public SettingsController(

             ISettingsService settingsService,
         
             IAuthenticationService authenticationService
        )
        {
            this._settingsService = settingsService;     
            this._authenticationService = authenticationService;
        }

        #region Consignee - Add

        public ActionResult AddConsignee()
        {
         
            return View(string.Format(FileSetting.VIEW_APPLICATION_PATH, "AddConsignee", "Settings"));

        }


        [HttpPost, ValidateAntiForgeryToken, FormValueRequired("submit-button")]
        public ActionResult AddConsignee(ConsigneeDetailsModel model)
        {

            try
            {
                var user = this._authenticationService.GetAuthenticatedUser();
                var consign_entity = new ConsigneeDetailsData();
                string ah_code = model.Ah_code;
                //check ah_code existed or not
                var check = this._settingsService.GetSameConsignee_Ah_code(user.Com, ah_code);

                if (check.Count > 0)
                {
                    base.WarningNotification("Same consignee code existed.");
                    return RedirectToAction("AddConsignee");
                }
                consign_entity.Status = "O";
                consign_entity.Ah_code = model.Ah_code;
                consign_entity.Ah_name = model.Ah_name;
                consign_entity.Com = user.Com;
                consign_entity.Add_dt = DateTime.Now;
                consign_entity.Add_usn = user.Name;

                this._settingsService.AddNewConsignee(consign_entity);

                base.SuccessNotification("Add consignee successful.");
                return View(string.Format(FileSetting.VIEW_APPLICATION_PATH, "ViewConsignee", "Settings"));
            }
            catch (Exception ex)
            {
                base.ErrorNotification(ex.ToString());
                return RedirectToAction("AddConsignee");
            }

        }

        #endregion

        #region Consignee - Edit

        public ActionResult EditConsignee(int id)
        {
            if (id <= 0)
                return RedirectToAction("List");

            var data = this._settingsService.GetSpecificConsigneeData(id);
            var model = GetConsigneeDetails(data);

            return View(string.Format(FileSetting.VIEW_APPLICATION_PATH, "EditConsignee", "Settings"), model);
        }

        private ConsigneeDetailsModel GetConsigneeDetails(ConsigneeDetailsData data)
        {
            if (data == null)
                return new ConsigneeDetailsModel();

            var model = new ConsigneeDetailsModel()
            {
                ID = data.ID,
                Ah_code = data.Ah_code,
                Ah_name = data.Ah_name,


            };

            model.SelectStatus.Add(new SelectListItem
            {
                Text = "ACTIVE",
                Value = "O",
                Selected = (data.Status == "O") ? true : false

            });

            model.SelectStatus.Add(new SelectListItem
            {
                Text = "DEACTIVE",
                Value = "X",
                Selected = (data.Status == "X") ? true : false

            });

            return model;
        }


        [HttpPost, ValidateAntiForgeryToken, FormValueRequired("submit-button")]
        public ActionResult EditConsignee(ConsigneeDetailsModel model)
        {

            try
            {
                var user = this._authenticationService.GetAuthenticatedUser();
                var consign_entity = new ConsigneeDetailsData();
                string ah_code = model.Ah_code;


                consign_entity.ID = model.ID;
                consign_entity.Ah_code = model.Ah_code;
                consign_entity.Ah_name = model.Ah_name;

                consign_entity.Status = model.Status;
                this._settingsService.EditConsignee(user.Com,consign_entity);


                base.SuccessNotification("Add consignee successful.");
                return View(string.Format(FileSetting.VIEW_APPLICATION_PATH, "ViewConsignee", "Settings"));
            }
            catch (Exception ex)
            {
                base.ErrorNotification(ex.ToString());
                return Redirect(Url.Action("EditConsignee", "Settings") + "/" + model.ID);

            }
       
        }

        #endregion

        #region Consignee - View
        public ActionResult List()
        {
            var model = new ConsigneeDetailsModel();
            return View(model);
        }

     
        public ActionResult ViewConsignee()
        {
           
            return View(string.Format(FileSetting.VIEW_APPLICATION_PATH, "ViewConsignee", "Settings"));

        }



        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult DisplayConsigneeList()
        {
            var user = this._authenticationService.GetAuthenticatedUser();
            var data = this._settingsService.GetConsigneeList(user.Com);
            return Json((new DataSourceResult()
            {
                data = data,
                recordsFiltered = data.Count(),
                recordsTotal = data.Count(),
                //draw = dataSourceRequest.Draw,
            }));
        }

        #endregion
    }
}