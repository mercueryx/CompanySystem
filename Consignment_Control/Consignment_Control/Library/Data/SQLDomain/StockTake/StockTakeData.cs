using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consignment_Control.Library.Data.SQLDomain.StockTake
{
    [System.ComponentModel.DataAnnotations.Schema.Table("consignee_inventory")]
    public partial class StockTakeData
    {
        public int ID { get; set; }

        public string Ah_code { get; set; }

        public string Com { get; set; }

        public string Catalog { get; set; }

        public string Description { get; set; }

        public int On_hand_qty { get; set; }

        public int Counted_qty { get; set; }


        public int To_bill_qty { get; set; }

        public int Pro_qty { get; set; }

        //public int On_hold_qty { get; set; }

        //public int Counted_on_hold_qty { get; set; }
    }
}