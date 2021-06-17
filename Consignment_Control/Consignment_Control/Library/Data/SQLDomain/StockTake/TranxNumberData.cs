using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consignment_Control.Library.Data.SQLDomain.StockTake
{
    [System.ComponentModel.DataAnnotations.Schema.Table("consignee_stocktake_log")]
    public class TranxNumberData
    {
       
        public string  Tranx_no { get; set; }
    }
}