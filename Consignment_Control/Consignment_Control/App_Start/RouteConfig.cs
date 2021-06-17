using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Consignment_Control
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

   
            #region Stock Take

            routes.MapRoute(
         name: "StockTake",
         url: "Home/StockTake",
         defaults: new { controller = "StockTake", action = "Create", id = UrlParameter.Optional }
        );

            routes.MapRoute(
        name: "GetConsigneeInv",
        url: "Home/GetCon_INV/byAjax",
        defaults: new { controller = "StockTake", action = "DisplayConsigneeInventory", id = UrlParameter.Optional }
        );


            routes.MapRoute(
        name: "ViewStockTake",
        url: "Home/ViewStockTake",
        defaults: new { controller = "StockTake", action = "ViewStockTake", id = UrlParameter.Optional }
        );
         


       

            #region View Stock Take Log


            routes.MapRoute(
        name: "GetStockTakeLog",
        url: "Home/GetStockTake_Log/byAjax",
        defaults: new { controller = "StockTake", action = "DisplayAllStockLog", id = UrlParameter.Optional }
        );
            #endregion


            #endregion

            #region Certify / Delivery


            routes.MapRoute(
        name: "DeliveredItem",
        url: "Home/StockTake/Delivery/{Id}",
        defaults: new { controller = "StockTake", action = "Delivered", id = UrlParameter.Optional }
        );


            routes.MapRoute(
        name: "StockTakeCertify",
        url: "Home/StockTake/Certify",
        defaults: new { controller = "StockTake", action = "Certify", id = UrlParameter.Optional }
        );

            routes.MapRoute(
   name: "GetOpen_CertifiedStockLog",
   url: "Home/GetOpen_Certified_STLog/byAjax",
   defaults: new { controller = "StockTake", action = "DisplayOpen_CertifiedStockLog", id = UrlParameter.Optional }
   );


            routes.MapRoute(
name: "GetCertifiedStockLog",
url: "Home/GetCertified_STLog/byAjax",
defaults: new { controller = "StockTake", action = "DisplayCertifiedStockLog", id = UrlParameter.Optional }
);


            routes.MapRoute(
    name: "ItemDelivery",
    url: "Home/ItemDeliveryControl",
    defaults: new { controller = "StockTake", action = "Delivery", id = UrlParameter.Optional }
    );


            #endregion

            #region Inventory

            routes.MapRoute(
        name: "ConsigneeInventoryDetails",
        url: "Home/GetCon_Inv_Dtl/byAjax",
        defaults: new { controller = "Inventory", action = "DisplayConInvDetails", id = UrlParameter.Optional }
        );
          

            routes.MapRoute(
        name: "AdjustInventory",
        url: "Home/StockTake/Adjustment/{id}",
        defaults: new { controller = "Inventory", action = "Adjustment", id = UrlParameter.Optional }
        );

            #endregion


            #region Report

            routes.MapRoute(
        name: "GetTransaction",
        url: "Home/Report/ViewTransaction",
        defaults: new { controller = "Report", action = "DisplayTranxReport", id = UrlParameter.Optional }
        );

            #endregion

            #region Settings

            routes.MapRoute(
              name: "AddConsignee",
              url: "Home/Settings/AddConsignee",
              defaults: new { controller = "Settings", action = "AddConsignee", id = UrlParameter.Optional }
              );


            routes.MapRoute(
        name: "EditConsigneeData",
        url: "Home/Settings/EditConsignee/{id}",
        defaults: new { controller = "Settings", action = "EditConsignee", id = UrlParameter.Optional }
        );


            routes.MapRoute(
        name: "ViewConsignee",
        url: "Home/Settings/ViewConsignee",
        defaults: new { controller = "Settings", action = "ViewConsignee", id = UrlParameter.Optional }
        );

                routes.MapRoute(
        name: "ConsigneeList",
        url: "Home/Settings/Ajax/ConsigneeList",
        defaults: new { controller = "Settings", action = "DisplayConsigneeList", id = UrlParameter.Optional }
        );

           
        //    routes.MapRoute(
        //name: "AdjustInventory",
        //url: "Home/StockTake/Adjustment/{id}",
        //defaults: new { controller = "Inventory", action = "Adjustment", id = UrlParameter.Optional }
        //);

            #endregion


            #region Page Route

            routes.MapRoute(
name: "HomePage",
url: "Home/Dashboard",
defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
);

            routes.MapRoute(
            name: "Default",
            url: "",
            defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional }
        );


            routes.MapRoute(
        name: "Inventory",
        url: "Home/Inventory",
        defaults: new { controller = "Inventory", action = "Inventory", id = UrlParameter.Optional }
        );

            routes.MapRoute(
        name: "Adjust",
        url: "Home/Adjustment",
        defaults: new { controller = "Inventory", action = "Adjustment", id = UrlParameter.Optional }
        );


            routes.MapRoute(
        name: "Report",
        url: "Home/Report",
        defaults: new { controller = "Report", action = "Report", id = UrlParameter.Optional }
        );


      
            #endregion
        }
      
    }
}
