using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Consignment_Control.Library.Data.SQLDomain.General;
using Consignment_Control.Library.Data.SQLDomain.StockTake;
namespace Consignment_Control.Library.Services.StockTake
{
    public partial interface IStockTakeService
    {
        IList <StockTakeData> GetAllCatalogByAhCode(string Ah_code,string com);


        StockTakeData GetSelectedItem_Inventory(int id);

        #region Checking
        IList<StockTakeLogData> GetLog_Catalog(string com, string ah_code);



        #endregion

      


        void StockTakeSubmit(string com,string ah_code, List<StockTakeLogData> stock_log, string tranx_no);

        void StockTakeCertify(string com, string ah_code, List<StockTakeLogData> stock_log, string tranx_no,CertifyLogData certifylog_entity);

        IList <StockTakeLogData>GetAllStockTakeLog(string com, string ah_code,string status);

        IList<PdfData> GetInventory_AhCode(string ah_code, string com);

        IList<StockTakeLogData> GetOpenStockTakeLog(string ah_code, string com);

        IList<StockTakeLogData> GetStockTakeLog_NotInStatus(string com, string ah_code, string status);

        IList<StockTakeLogData> GetStockTakeLog_Certify(string com, string ah_code, string status);
        StockTakeLogData GetLogVariantQty_Id(int id);

        void StockDelivery(string com, string ah_code, string catalog, int var_qty, string tranx_no,int id);
    }
}