using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consignment_Control.Library.Data.SQLDomain.StockTake
{
    [System.ComponentModel.DataAnnotations.Schema.Table("consignee_stocktake_log")]
    public class StockTakeLogData
    {
        public int ID { get; set; }

        public string  Tranx_no { get; set; }

        public string Ah_code { get; set; }

        public string Com { get; set; }

        public string  Catalog { get; set; }


        public string Description { get; set; }

        public int On_hand_qty { get; set; }

        public int Counted_qty { get; set; }

        public int Variant_qty { get; set; }

        public string Variant_type { get; set; }

        public string Status { get; set; }

        public DateTime Add_dt { get; set; }

        public string Add_usn { get; set; }

        public string Remark { get; set; }


    }
}