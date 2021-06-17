using Consignment_Control.Library.Data.SQLDomain.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consignment_Control.Library.Services.Inventory
{
    public partial interface IInventoryService
    {

       IList<InventoryData> GetInventoryDetails_Consignee(string ah_code, string com);
        InventoryData GetSelectedItem_Inventory(int id);

        void StockAdjustment(string com, string ah_code, AdjustmentData adjust_log, int total,int ID);

    }
}