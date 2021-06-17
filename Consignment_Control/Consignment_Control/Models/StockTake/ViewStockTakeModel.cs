using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consignment_Control.Models.StockTake
{
    public class ViewStockTakeModel
    {
        public int ID { get; set; }

        public string Ah_code { get; set; }

        public string Catalog { get; set; }

        public string Description { get; set; }

        public int On_hand_qty { get; set; }

        public int Counted_qty { get; set; }

        public int On_hold_qty { get; set; }
    }
}