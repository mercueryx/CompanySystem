using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consignment_Control.Models.StockTake
{
    public class StockTakeLogModel
    {
        public int ID { get; set; }

        public string Tranx_no { get; set; }


        public string Catalog { get; set; }


        //public string Description { get; set; }


        //public int On_hand_qty { get; set; }


        public int Counted_qty { get; set; }


        //public int On_hold_qty { get; set; }


        //public int Counted_on_hold_qty { get; set; }


        public int Variant_qty { get; set; }
    }
}