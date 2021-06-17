using BasePlatform.Library.Framework.Controllers;
using Consignment_Control.Library.Data.SQLDomain.Inventory;
using Consignment_Control.Library.Framework.Controllers;
using Consignment_Control.Library.Framework.Listing;
using Consignment_Control.Library.Services.Authentication;
using Consignment_Control.Library.Services.General;
using Consignment_Control.Library.Services.Inventory;
using Consignment_Control.Library.Setting;
using Consignment_Control.Models.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Consignment_Control.Controllers
{
    public class InventoryController : BaseAuthorizeController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IInventoryService _inventoryService;
        private readonly IConsigneeService _consigneeService;

        public InventoryController(

        
           IAuthenticationService authenticationService,
            IInventoryService inventoryService,
            IConsigneeService consigneeService

      )
        {


      
            this._authenticationService = authenticationService;
            this._inventoryService = inventoryService;
            this._consigneeService = consigneeService;


        }

        public ActionResult Inventory()
        {
            var model = new InventoryDetailsModel();
            var user = this._authenticationService.GetAuthenticatedUser();
            var datas = this._consigneeService.GetAhCode_Name(user.Com);
            var selected_value = Request.QueryString["ah_code"];
            if (datas.Count > 0)
            {
                foreach (var item in datas)
                {
                    model.SelectAccountHolder.Add(new SelectListItem
                    {
                        Text = item.Ah_name,
                        Value = item.Ah_code,
                        Selected = (selected_value == item.Ah_code) ? true : false

                    });
                }
            }

        
            return View(string.Format(FileSetting.VIEW_APPLICATION_PATH, "View", "Inventory"), model);
        }



        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult DisplayConInvDetails(string ah_code)
        {
            var user = this._authenticationService.GetAuthenticatedUser();
            var data = this._inventoryService.GetInventoryDetails_Consignee(ah_code, user.Com);
            return Json((new DataSourceResult()
            {
                data = data,
                recordsFiltered = data.Count(),
                recordsTotal = data.Count(),
                //draw = dataSourceRequest.Draw,
            }));
        }



        public ActionResult Adjustment(int id)
        {
            if (id <= 0)
                return RedirectToAction("List");

            var data = this._inventoryService.GetSelectedItem_Inventory(id);
            var model = GetCatalogDetails(data);

            return View(string.Format(FileSetting.VIEW_APPLICATION_PATH, "Adjust", "Inventory"), model);
        
        }

        public ActionResult List()
        {
            var model = new InventoryDetailsModel();
            return View(model);
        }

        private InventoryDetailsModel GetCatalogDetails(InventoryData data)
        {
            if (data == null)
                return new InventoryDetailsModel();

            var model = new InventoryDetailsModel()
            {
                ID = data.ID,
                Ah_Code = data.Ah_code,
                Catalog = data.Catalog,
                Description = data.Description,
                On_hand_qty = data.On_hand_qty,
                Adjust_qty = 0,
                Rmk = "",
            };
            return model;
        }



        [HttpPost, ValidateAntiForgeryToken, FormValueRequired("submit-button")]
        public ActionResult Adjustment(InventoryDetailsModel model)
        {
            try
            {
                var user = this._authenticationService.GetAuthenticatedUser();
                var Adjust_data = new AdjustmentData();


                string tranx_no = DateTime.Now.ToString("yyMMddhhmmss");
                tranx_no = "A00" + tranx_no;


                if (model.Var_type == "IN")
                {
                    Adjust_data.On_hand_qty = model.On_hand_qty + model.Adjust_qty;

                }
                else
                {
                    if (model.Adjust_qty > model.On_hand_qty)
                    {
                        base.WarningNotification("Deduct qty cannot more than on hand qty.");
                        return RedirectToAction("Adjustment");
                    }
                    Adjust_data.On_hand_qty = model.On_hand_qty - model.Adjust_qty;

                }
          
                Adjust_data.Com = user.Com;
                Adjust_data.Tranx_no = tranx_no;
                Adjust_data.Status = "C";
                Adjust_data.Ah_code = model.Ah_Code;
                Adjust_data.Catalog = model.Catalog;
                Adjust_data.Description = model.Description;
                Adjust_data.Var_qty = model.Adjust_qty;
                Adjust_data.Var_type = model.Var_type;
                Adjust_data.Var_cate= model.Var_cate;
                Adjust_data.Add_dt = DateTime.Now;
                Adjust_data.Add_usn = user.Name;
                Adjust_data.Rmk = model.Rmk;


                this._inventoryService.StockAdjustment(user.Com, model.Ah_Code, Adjust_data, Adjust_data.On_hand_qty,model.ID);
                base.SuccessNotification("Adjustment successul.");
                //return RedirectToAction("Adjustment");

                return Redirect(Url.Action("Inventory", "Inventory") + "?Ah_code=" + model.AccountHolderCode);
            }
            catch (Exception ex)
            {

                base.ErrorNotification(ex.ToString());
                return RedirectToRoute("Adjustment");
            }
        }




     }
}