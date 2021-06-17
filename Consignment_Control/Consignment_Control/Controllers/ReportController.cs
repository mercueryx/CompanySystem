using BasePlatform.Library.Framework.Controllers;
using Consignment_Control.Library.Framework.Controllers;
using Consignment_Control.Library.Framework.Listing;
using Consignment_Control.Library.Services.Authentication;
using Consignment_Control.Library.Services.General;
using Consignment_Control.Library.Services.Report;
using Consignment_Control.Library.Setting;
using Consignment_Control.Models.Report;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Excel = Microsoft.Office.Interop.Excel;
namespace Consignment_Control.Controllers
{
    public class ReportController : BaseAuthorizeController
    {
        private readonly IReportService _reportService;
        private readonly IConsigneeService _consigneeService;
        private readonly IAuthenticationService _authenticationService;
        public ReportController(

             IReportService reportService,
             IConsigneeService consigneeService,
             IAuthenticationService authenticationService
        )
        {


            this._reportService = reportService;
            this._consigneeService = consigneeService;
            this._authenticationService = authenticationService;
        }
        public ActionResult Report()
        {

            var model = new ReportModel();
            var user = this._authenticationService.GetAuthenticatedUser();
            var ahcode_data = this._consigneeService.GetAhCode_Name(user.Com);

            var selected_value = Request.QueryString["ah_code"];
            if (ahcode_data.Count > 0)
            {
                foreach (var item in ahcode_data)
                {
                    model.SelectAccountHolder.Add(new SelectListItem
                    {
                        Text = item.Ah_name,
                        Value = item.Ah_code,
                        Selected = (selected_value == item.Ah_code) ? true : false

                    });
                }
            }

            var var_cate_data = this._reportService.GetCategory(user.Com);
            if (var_cate_data.Count > 0)
            {
                foreach (var item in var_cate_data)
                {
                    model.SelectCategory.Add(new SelectListItem
                    {
                        Text = item.Var_cate,
                        Value = item.Var_cate,
                        Selected = (selected_value == item.Var_cate) ? true : false

                    });
                }
            }

            //return View(string.Format(FileSetting.VIEW_APPLICATION_PATH, "Create", "StockTake"), model);
            return View(string.Format(FileSetting.VIEW_APPLICATION_PATH, "Report", "Report"),model);

        }


        [HttpPost, ValidateAntiForgeryToken, FormValueRequired("submit-button")]
        public ActionResult Report(ReportModel model)
        {

            var doc = new Document(new Rectangle(288f,144f),10,10,10,10);
            doc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            try
            {

                using (MemoryStream ms = new MemoryStream())
                {


                    //string path = Server.MapPath("PDFs");
                    var user = this._authenticationService.GetAuthenticatedUser();
                    //var list = new List<PdfData>();
                    //var data = this._stockTakeService.GetInventory_AhCode(model.AccountHolderCode, user.Com);
                    //var ah_name_data = this._consigneeService.GetAhNameBy_Ah_code(user.Com, model.AccountHolderCode);
                    var data = model.Tranx_list;


                    PdfWriter writer = PdfWriter.GetInstance(doc, ms);

                    PdfPTable table = new PdfPTable(7);

                    //table.TotalWidth = 550f;

                    table.WidthPercentage = 100;

                    table.LockedWidth = true;
                    //float[] widths = new float[] { 390f, 80f, 80f };             
                    //table.SetWidths(widths);


                    table.SetTotalWidth(new float[] { 40,270,70,70,70, 40,270 });
                    //PdfPCell header = new PdfPCell(new Phrase(ah_name_data.Ah_name));

                    doc.Open();
                    //header.Colspan = 5;

                    //table.AddCell(header);

                    addCell(table, "ID", 2, 10);
                    addCell(table, "Description", 2, 10);

                    addCell(table, "On Hand Qty", 2, 10);
                    addCell(table, "Variant Qty", 2, 10);
                    addCell(table, "Variant Category", 2, 10);
                    addCell(table, "Type", 2, 10);
                    addCell(table, "Remark", 2, 10);



                    foreach (var data_list in data)
                    {
                        doc.Add(new Paragraph());
                        addCell(table, data_list.ID.ToString(), 2, 10);
                        addCell(table, data_list.Description, 2, 9);
                        addCell(table, data_list.On_hand_qty.ToString(), 2, 10);
                        addCell(table, data_list.Var_qty.ToString(), 2, 10);
                        addCell(table, data_list.Var_cate, 2, 9);
                        addCell(table, data_list.Var_type, 2, 10);
                        addCell(table, data_list.Rmk, 2, 9);
                      



                    }


                    doc.Add(table);
                    doc.Close();


                    byte[] bytes = ms.ToArray();
                    ms.Close();
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment; filename=Transaction_Report" + DateTime.Now.ToString("yyMMddHHMMss")+".pdf");
                    Response.ContentType = "application/pdf";
                    Response.Buffer = true;
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();
                    Response.Close();



                }


                //base.SuccessNotification("Report export to " + path);
                return RedirectToAction("Report");


            }
            catch (Exception ex)
            {
                base.ErrorNotification(ex.ToString());
                return RedirectToAction("Report");
            }

   
    


        }

        private static void addCell(PdfPTable table, string text, int rowspan, int fontsize)
        {
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            iTextSharp.text.Font times = new iTextSharp.text.Font(bfTimes, fontsize, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.Rowspan = rowspan + 1;
            //cell.Colspan = rowspan;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(cell);
        }

        //try
        //{
        //    var list = new List<TransReportModel>();
        //    var tranx_list = model.Tranx_list;
        //    foreach (var data_list in tranx_list)
        //    {
        //        list.Add(new TransReportModel
        //        {

        //            ID = data_list.ID,
        //            Tranx_no = data_list.Tranx_no,
        //            Description = data_list.Description,
        //            On_hand_qty = data_list.On_hand_qty,
        //            Variant_qty = data_list.Var_qty,
        //            Variant_type=data_list.Var_type,
        //            Variant_category = data_list.Var_cate,
        //            Remark =data_list.Rmk,
        //            Add_time = data_list.Add_dt,
        //            Add_user= data_list.Add_usn,
        //        }

        //    );

        //    }



        //    string filename = "Report" + DateTime.Now.ToString("yyMMddHHMMss") + ".xlsx";
        //    //string path = @"C:\Downloads\"+filename;
        //    string path = Server.MapPath("~/Report");
        //    if (!System.IO.File.Exists(path))
        //    {
        //        System.IO.File.Create(path);

        //    }

        //    path = path + filename;
        //    GenerateExcel(ConvertToDataTable(list), path);

        //    base.SuccessNotification("Report export to "+path);
        //    return RedirectToAction("Report");

        //}
        //catch (Exception ex)
        //{
        //    base.ErrorNotification(ex.ToString());
        //    return RedirectToAction("Report");
        //}

        //static DataTable ConvertToDataTable<T>(List<T> models)
        //{
        //    // creating a data table instance and typed it as our incoming model   
        //    // as I make it generic, if you want, you can make it the model typed you want.  
        //    DataTable dataTable = new DataTable(typeof(T).Name);

        //    //Get all the properties of that model  
        //    PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        //    // Loop through all the properties              
        //    // Adding Column name to our datatable  
        //    foreach (PropertyInfo prop in Props)
        //    {
        //        //Setting column names as Property names    
        //        dataTable.Columns.Add(prop.Name);
        //    }
        //    // Adding Row and its value to our dataTable  
        //    foreach (var item in models)
        //    {
        //        var values = new object[Props.Length];
        //        for (int i = 0; i < Props.Length; i++)
        //        {
        //            //inserting property values to datatable rows    
        //            values[i] = Props[i].GetValue(item, null);
        //        }
        //        // Finally add value to datatable    
        //        dataTable.Rows.Add(values);
        //    }
        //    return dataTable;
        //}

        //public static void GenerateExcel(DataTable dataTable, string path)
        //{

        //    DataSet dataSet = new DataSet();
        //    dataSet.Tables.Add(dataTable);

        //    // create a excel app along side with workbook and worksheet and give a name to it  
        //    Excel.Application excelApp = new Excel.Application();
        //    Excel.Workbook excelWorkBook = excelApp.Workbooks.Add();
        //    Excel._Worksheet xlWorksheet = excelWorkBook.Sheets[1];
        //    Excel.Range xlRange = xlWorksheet.UsedRange;
        //    foreach (DataTable table in dataSet.Tables)
        //    {
        //        //Add a new worksheet to workbook with the Datatable name  
        //        Excel.Worksheet excelWorkSheet = excelWorkBook.Sheets.Add();
        //        excelWorkSheet.Name = table.TableName;

        //        // add all the columns  
        //        for (int i = 1; i < table.Columns.Count + 1; i++)
        //        {
        //            excelWorkSheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
        //        }

        //        // add all the rows  
        //        for (int j = 0; j < table.Rows.Count; j++)
        //        {
        //            for (int k = 0; k < table.Columns.Count; k++)
        //            {
        //                excelWorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
        //            }
        //        }
        //    }
        //    excelWorkBook.SaveAs(path);
        //    excelWorkBook.Close();
        //    excelApp.Quit();
        //}



        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult DisplayTranxReport(ReportModel model)
        {
            var _dateFrom = 0;
            var _dateTo = 0;

            var user = this._authenticationService.GetAuthenticatedUser();

            if (string.IsNullOrEmpty(model.StartDate))
            {
                _dateFrom = Convert.ToInt32("00000000");

            }
            else
            {
                if (DateTime.TryParseExact(model.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime dtFrom))
                {
                    _dateFrom = Convert.ToInt32(dtFrom.ToString("yyyyMMdd"));

                }
            }

            if (string.IsNullOrEmpty(model.EndDate))
            {
                _dateTo = Convert.ToInt32("99999999");

            }
            else
            {
                if (DateTime.TryParseExact(model.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime dtTo))
                {
                    _dateTo = Convert.ToInt32(dtTo.ToString("yyyyMMdd"));

                }
            }

            if (_dateFrom > _dateTo)
            {
                base.WarningNotification("Start date cannot more than end date.");

            }
            var data = this._reportService.GetTransactionData(_dateFrom,_dateTo,model.Category,model.Ah_code, user.Com);
            return Json((new DataSourceResult()
            {
                data = data,
                recordsFiltered = data.Count(),
                recordsTotal = data.Count(),
                //draw = dataSourceRequest.Draw,
            }));
        }

    }
}