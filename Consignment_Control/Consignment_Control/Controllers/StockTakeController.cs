using BasePlatform.Library.Framework.Controllers;
using Consignment_Control.Library.Data.SQLDomain.Inventory;
using Consignment_Control.Library.Data.SQLDomain.StockTake;
using Consignment_Control.Library.Framework.Controllers;
using Consignment_Control.Library.Framework.Listing;
using Consignment_Control.Library.Services.Authentication;
using Consignment_Control.Library.Services.General;
using Consignment_Control.Library.Services.StockTake;
using Consignment_Control.Library.Setting;
using Consignment_Control.Models.StockTake;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Consignment_Control.Controllers
{
    public class StockTakeController : BaseAuthorizeController
    {
        private readonly IStockTakeService _stockTakeService;
        private readonly IConsigneeService _consigneeService;
        private readonly IAuthenticationService _authenticationService;
        public StockTakeController(

             IStockTakeService stockTakeService,
             IConsigneeService consigneeService,
             IAuthenticationService authenticationService
        )
        {


            this._stockTakeService = stockTakeService;
            this._consigneeService = consigneeService;
            this._authenticationService = authenticationService;
        }


        #region StockTake Create
        public ActionResult Create()
        {

            var model = new StockTakeModel();
            var user = this._authenticationService.GetAuthenticatedUser();
            var datas = this._consigneeService.GetAhCode_Name(user.Com);

            var selected_value = Request.QueryString["ah_code"];
            if (datas.Count > 0)
            {
                foreach (var item in datas)
                {
                    model.SelectAccountHolder.Add(new SelectListItem
                    {
                        Text = item.Ah_name, Value = item.Ah_code,
                        Selected = (selected_value==item.Ah_code)?true:false

                    });
                }
            }
       
            return View(string.Format(FileSetting.VIEW_APPLICATION_PATH, "Create", "StockTake"), model);

        }


        [HttpPost, ValidateAntiForgeryToken, FormValueRequired("submit-button")]
        public ActionResult Create(StockTakeModel model)
        {
            try
            {
                string var_type = "";
                int var_qty = 0;
                var user = this._authenticationService.GetAuthenticatedUser();
                var list = model.Inv_list;


                string tranx_no = DateTime.Now.ToString("yyMMddhhmmss");
                tranx_no = "ST0" + tranx_no;

                //var stock_entity = new List<StockTakeData>();
                var log_entity = new List<StockTakeLogData>();
                var adjust_entity = new List<AdjustmentData>();



                foreach (var stock_take_list in list)
                {

                 
                

                    if (stock_take_list.On_hand_qty > stock_take_list.Counted_qty)
                    {
                        var_type = "OUT";
                        var_qty = stock_take_list.On_hand_qty - stock_take_list.Counted_qty;



                    }

                    else if (stock_take_list.On_hand_qty == stock_take_list.Counted_qty)
                    {
                        var_type = "-";
                        var_qty = 0;

                    }
                 



                    log_entity.Add(new StockTakeLogData {

                        Tranx_no = tranx_no,
                        Ah_code = model.AccountHolderCode,
                        Com = user.Com,
                        Catalog = stock_take_list.Catalog,
                        Description = stock_take_list.Description,
                        On_hand_qty = stock_take_list.On_hand_qty,
                        Counted_qty = stock_take_list.Counted_qty, 
                        Variant_qty = var_qty,
                        Variant_type=var_type,
                        Status = "O",
                        Add_dt = DateTime.Now,
                        Add_usn = user.Name

                    });

        


              
                }
                this._stockTakeService.StockTakeSubmit(user.Com, model.AccountHolderCode,log_entity,tranx_no);
                    base.SuccessNotification("Submit successful.");
           
        
                return Redirect(Url.Action("Create", "StockTake") + "?Ah_code=" + model.AccountHolderCode);

            }
            catch (Exception ex)
            {
                base.ErrorNotification(ex.ToString());
                return RedirectToAction("Create");
            }
      

        }


        [HttpPost, ValidateAntiForgeryToken, FormValueRequired("print-button"), ActionName("Create")]
        public ActionResult Print(StockTakeModel model)
        {
            var doc = new Document(PageSize.A4);
            try
            {

                using (MemoryStream ms = new MemoryStream())
                {


                    //string path = Server.MapPath("PDFs");
                    var user = this._authenticationService.GetAuthenticatedUser();
                    var list = new List<PdfData>();
                    var data = this._stockTakeService.GetInventory_AhCode(model.AccountHolderCode, user.Com);
                    var ah_name_data = this._consigneeService.GetAhNameBy_Ah_code(user.Com,model.AccountHolderCode);
                    
                   

                    PdfWriter writer =  PdfWriter.GetInstance(doc, ms);

                    PdfPTable table = new PdfPTable(3);

                    table.TotalWidth = 550f;

                    table.LockedWidth = true;
                    float[] widths = new float[] { 390f, 80f, 80f};
                    table.SetWidths(widths);
                    PdfPCell header = new PdfPCell(new Phrase(ah_name_data.Ah_name));


                    BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                    iTextSharp.text.Font footer_fonts = new iTextSharp.text.Font(bfTimes, 15, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
                    iTextSharp.text.Font time_fonts = new iTextSharp.text.Font(bfTimes, 15, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);

                    PdfPCell footer = new PdfPCell(new Phrase("Sign:", footer_fonts));
                    PdfPCell time_dtl = new PdfPCell(new Phrase("Date: "+DateTime.Now.ToString("dd-MM-yyyy"), time_fonts));
                    doc.Open();
                    header.Colspan = 3;
                    footer.Colspan = 3;
                    time_dtl.Colspan = 3;

                    table.AddCell(header);

                    addCell(table, "Desciption", 2, 10);
                    addCell(table, "On Hand", 2, 10);

                    addCell(table, "Counted Qty", 2, 10);
                    //addCell(table, "Counted Qty", 2, 8);
                    //addCell(table, "Counted on Hold", 2, 8);

                    foreach (var data_list in data)
                    {
                        doc.Add(new Paragraph());
                        addCell(table, data_list.Description, 2, 10);
                        addCell(table, data_list.On_hand_qty.ToString(), 2, 10);
                        addCell(table, "", 2, 10);

                  

                    }

                    //doc.Add(new Paragraph());
                    ////addCell(table, "Sign:", 2, 15);


                    //footer.Rowspan = 3;
                    ////cell.Colspan = rowspan;
                    //footer.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    //footer.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                    //table.AddCell(footer);
                    table.AddCell(footer);
                    table.AddCell(time_dtl);

                    doc.Add(table);
                    doc.Close();
                 

                    byte[] bytes = ms.ToArray();
                    ms.Close();
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment; filename="+DateTime.Now.ToString("yyMMddHHMMss") + model.AccountHolderCode + ".pdf");
                    Response.ContentType = "application/pdf";
                    Response.Buffer = true;
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();
                    Response.Close();



                }

                /*tring path = Url.Action("Create", "StockTake") + "?Ah_code=" + model.AccountHolderCode;*/
                //base.SuccessNotification("Download Successful.");

                return Redirect(Url.Action("Create", "StockTake") + "?Ah_code=" + model.AccountHolderCode);
                //return Response.AddHeader("Refresh","3; url="+ path);
                //return new HttpUnauthorizedResult();


            }
            catch (Exception ex)
            {
                base.ErrorNotification(ex.ToString());
                return RedirectToAction("Create");
            }
            //finally
            //{
            //    doc.Close();
            //}


        }


        private static void addCell(PdfPTable table, string text, int rowspan,int fontsize)
        {
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            iTextSharp.text.Font times = new iTextSharp.text.Font(bfTimes, fontsize, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.Rowspan = rowspan+1;
            //cell.Colspan = rowspan;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(cell);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult DisplayConsigneeInventory(string ah_code)
        {
            var user = this._authenticationService.GetAuthenticatedUser();
            var data = this._stockTakeService.GetAllCatalogByAhCode(ah_code, user.Com);
            return Json((new DataSourceResult()
            {
                data = data,
                recordsFiltered = data.Count(),
                recordsTotal = data.Count(),
                //draw = dataSourceRequest.Draw,
            }));
        }

        public ActionResult List()
        {
            var model = new StockTakeModel();
            return View(model);
        }

  
        private StockTakeModel GetCatalogDetails(StockTakeData data)
        {
            if (data == null)
                return new StockTakeModel();

            var model = new StockTakeModel()
            {
                ID = data.ID,
                Ah_Code = data.Ah_code,
                Catalog = data.Catalog,
                Description = data.Description,
                On_hand_qty = data.On_hand_qty,
                Counted_qty = data.Counted_qty,
             
            };
            return model;
        }


        //[HttpPost, ValidateAntiForgeryToken, FormValueRequired("save-button")]
        //public ActionResult Edit(StockTakeModel model)
        //{
        //try
        //{
        //    bool same_row_status = false;
        //    if (ModelState.IsValid)
        //    {

        //        var user = this._authenticationService.GetAuthenticatedUser();


        //        check got different user entry this log before or not
        //        var check = this._stockTakeService.GetLog_Catalog_DifferentUser(user.Com, model.Ah_Code, model.Catalog, user.Name);
        //        if (check != null)
        //        {
        //            base.WarningNotification("Cannot stock take for this consignee because already edited by other user.");
        //            return Redirect(Url.Action("Create", "StockTake") + "?Ah_code=" + model.Ah_Code);
        //        }


        //        check got same log entry before for this user or not

        //        var data = this._stockTakeService.GetLog_Catalog(user.Com, model.Ah_Code, model.Catalog, user.Name);

        //        if (data == null)
        //            {
        //                same_row_status = false;

        //            }
        //            else
        //            {
        //                same_row_status = true;


        //            }




        //        var stock_entity = new StockTakeData()
        //        {
        //            ID = model.ID,
        //            Counted_on_hold_qty = model.Counted_on_hold_qty,
        //            Counted_qty = model.Counted_qty,


        //        };

        //        var log_entity = new StockTakeLogData()
        //        {
        //            Tranx_no = "",
        //            Ah_code = model.Ah_Code,
        //            Com = user.Com,
        //            Catalog = model.Catalog,
        //            Description = model.Description,
        //            On_hand_qty = model.On_hand_qty,
        //            Counted_qty = model.Counted_qty,
        //            On_hold_qty = model.On_hold_qty,
        //            Counted_on_hold_qty = model.Counted_on_hold_qty,
        //            Status = "O",
        //            Add_dt = DateTime.Now,
        //            Add_usn = user.Name
        //        };

        //        this._stockTakeService.StockTakeItemCount(log_entity, stock_entity, same_row_status);


        //        base.SuccessNotification("Save successful.");


        //        return Redirect(Url.Action("Create", "StockTake") + "?Ah_code=" + model.Ah_Code);

        //    }
        //    this._stockTakeService.StockTakeItemCount()
        //    return RedirectToAction("Create");
        //}
        //catch (Exception ex)
        //{

        //    base.ErrorNotification(ex.ToString());
        //    return RedirectToAction("Create");
        //}
        //}


        //public void ClearStockTakeData()
        //{
        //    var user = this._authenticationService.GetAuthenticatedUser();
        //    this._stockTakeService.ClearTemporaryStockTake(user.Com,user.Name);

        //}

        #endregion


        #region Certify
        public ActionResult Certify()
        {
            var user = this._authenticationService.GetAuthenticatedUser();
            //display status
            var model = new StockTakeModel();

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


            return View(string.Format(FileSetting.VIEW_APPLICATION_PATH, "Certify", "StockTake"), model);
        }



        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult DisplayOpen_CertifiedStockLog(string ah_code)
        {


            var user = this._authenticationService.GetAuthenticatedUser();
            var data = this._stockTakeService.GetStockTakeLog_NotInStatus(user.Com, ah_code, "O");
            return Json((new DataSourceResult()
            {
                data = data,
                recordsFiltered = data.Count(),
                recordsTotal = data.Count(),
                //draw = dataSourceRequest.Draw,
            }));
        }



        [HttpPost, ValidateAntiForgeryToken, FormValueRequired("certify-button")]

        public ActionResult Certify(StockTakeModel model)
        {

            try
            {
                var user = this._authenticationService.GetAuthenticatedUser();
                var list = model.Log_List;
                string Transaction_No ="";
                var log_entity = new List<StockTakeLogData>();
                foreach (var stock_take_list in list)
                {
                    log_entity.Add(new StockTakeLogData
                    {
                        ID = stock_take_list.ID,
                        Tranx_no = stock_take_list.Tranx_no,
                        Ah_code = model.AccountHolderCode,
                        Com = user.Com,
                        Catalog = stock_take_list.Catalog,                     
                        Variant_qty = stock_take_list.Variant_qty,
                      
                       
                    });
                    Transaction_No = stock_take_list.Tranx_no;
                }


                var certifylog_entity = new CertifyLogData();
                certifylog_entity.Tranx_no = Transaction_No;
                certifylog_entity.Ah_code = model.AccountHolderCode;
                certifylog_entity.Certify_dt = DateTime.Now;
                certifylog_entity.Certify_usn = user.Name;
                certifylog_entity.Com = user.Com;




                this._stockTakeService.StockTakeCertify(user.Com, model.AccountHolderCode, log_entity, Transaction_No,certifylog_entity);
                base.SuccessNotification("Certified successful.");

                return Redirect(Url.Action("Certify", "StockTake") + "?Ah_code=" + model.AccountHolderCode);
            }
            catch (Exception ex)
            {
                base.ErrorNotification(ex.ToString());
                return RedirectToAction("Certify");
            }


        }


        [HttpPost, ValidateAntiForgeryToken, FormValueRequired("print-button"), ActionName("Certify")]
        public ActionResult PrintCertify(StockTakeModel model)
        {
            var doc = new Document(PageSize.A4);
            try
            {

                using (MemoryStream ms = new MemoryStream())
                {
                    //string path = Server.MapPath("PDFs");
                    var user = this._authenticationService.GetAuthenticatedUser();
                    //var list = new List<StockTakeLogData>();
                    var data = this._stockTakeService.GetOpenStockTakeLog(model.AccountHolderCode, user.Com);

                    if (data.Count == 0)
                    {
                        base.WarningNotification("No stock take to print out.");
                        return RedirectToAction("Certify");

                    }
                    var ah_name_data = this._consigneeService.GetAhNameBy_Ah_code(user.Com, model.AccountHolderCode);



                    PdfWriter writer = PdfWriter.GetInstance(doc, ms);

                    PdfPTable table = new PdfPTable(3);

                    table.TotalWidth = 550f;

                    table.LockedWidth = true;
                    float[] widths = new float[] { 390f, 80f, 80f };
                    table.SetWidths(widths);
                    PdfPCell header = new PdfPCell(new Phrase(ah_name_data.Ah_name));
                    PdfPCell Time_details = new PdfPCell(new Phrase("Sign:"));

                    BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                    iTextSharp.text.Font footer_fonts = new iTextSharp.text.Font(bfTimes, 15, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
                    iTextSharp.text.Font time_fonts = new iTextSharp.text.Font(bfTimes, 15, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);

                    PdfPCell footer = new PdfPCell(new Phrase("Sign:", footer_fonts));
                    PdfPCell time_dtl = new PdfPCell(new Phrase("Date: " + DateTime.Now.ToString("dd-MM-yyyy"), time_fonts));
                    doc.Open();
                    header.Colspan = 3;
                    footer.Colspan = 3;
                    time_dtl.Colspan = 3;

                    table.AddCell(header);

                    addCell(table, "Desciption", 2, 10);
                    addCell(table, "On Hand", 2, 10);

                    addCell(table, "Counted Qty", 2, 10);
                    //addCell(table, "Counted Qty", 2, 8);
                    //addCell(table, "Counted on Hold", 2, 8);

                    foreach (var data_list in data)
                    {
                        doc.Add(new Paragraph());
                        addCell(table, data_list.Description, 2, 10);
                        addCell(table, data_list.On_hand_qty.ToString(), 2, 10);
                        addCell(table, data_list.Counted_qty.ToString(), 2, 10);

                        //addCell(table, "", 2, 8);


                        //addCell(table, "", 2, 8);


                    }
                    table.AddCell(footer);
                    table.AddCell(time_dtl);

                    doc.Add(table);
                    doc.Close();


                    byte[] bytes = ms.ToArray();
                    ms.Close();
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment; filename=CertifyForm" + model.AccountHolderCode + DateTime.Now.ToString("yyMMddHHMMss") + ".pdf");
                    Response.ContentType = "application/pdf";
                    Response.Buffer = true;
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();
                    Response.Close();



                }

                /*tring path = Url.Action("Create", "StockTake") + "?Ah_code=" + model.AccountHolderCode;*/
                //base.SuccessNotification("Download Successful.");

                return Redirect(Url.Action("Certify", "StockTake") + "?Ah_code=" + model.AccountHolderCode);
                //return Response.AddHeader("Refresh","3; url="+ path);
                //return new HttpUnauthorizedResult();


            }
            catch (Exception ex)
            {
                base.ErrorNotification(ex.ToString());
                return RedirectToAction("Certify");
            }
        }



        public ActionResult Delivery()
        {
            var user = this._authenticationService.GetAuthenticatedUser();
            //display status
            var model = new StockTakeModel();

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


            return View(string.Format(FileSetting.VIEW_APPLICATION_PATH, "Delivery", "StockTake"), model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult DisplayCertifiedStockLog(string ah_code)
        {


            var user = this._authenticationService.GetAuthenticatedUser();
            var data = this._stockTakeService.GetStockTakeLog_Certify(user.Com, ah_code, "C");
            return Json((new DataSourceResult()
            {
                data = data,
                recordsFiltered = data.Count(),
                recordsTotal = data.Count(),
                //draw = dataSourceRequest.Draw,
            }));
        }


        public ActionResult Delivered(int Id)
        {
            try
            {
                string catalog,tranx_no,ah_code;
                int var_qty;
                var user = this._authenticationService.GetAuthenticatedUser();
                var data = this._stockTakeService.GetLogVariantQty_Id(Id);
                catalog = data.Catalog;
                var_qty = data.Variant_qty;
                tranx_no = data.Tranx_no;
                ah_code = data.Ah_code;

                this._stockTakeService.StockDelivery(user.Com, ah_code, catalog, var_qty, tranx_no, Id);
                base.SuccessNotification("Delivery successful.");

                return Redirect(Url.Action("Certify", "StockTake") + "?Ah_code=" + ah_code);
            }
            catch (Exception ex)
            {
                base.ErrorNotification(ex.ToString());
                return RedirectToAction("Certify");
            }
        }



      
            #endregion


            #region View StockTake
            public ActionResult ViewStockTake()
        {

            var user = this._authenticationService.GetAuthenticatedUser();
            //display status
            var model = new StockTakeModel();
          
            var datas = this._consigneeService.GetAhCode_Name(user.Com);
        
            if (datas.Count > 0)
            {
                foreach (var item in datas)
                {
                    model.SelectAccountHolder.Add(new SelectListItem
                    {
                        Text = item.Ah_name,
                        Value = item.Ah_code,
                    

                    });
                }
            }


            return View(string.Format(FileSetting.VIEW_APPLICATION_PATH, "View", "StockTake"), model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult DisplayAllStockLog(string ah_code)
        {

       
            var user = this._authenticationService.GetAuthenticatedUser();
            var data = this._stockTakeService.GetAllStockTakeLog(user.Com,ah_code,"C");
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