using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consignment_Control.Library.Data.SQLDomain.StockTake
{
    [System.ComponentModel.DataAnnotations.Schema.Table("consignee_inventory")]
    public partial class PdfData
    {

 
        public string Description { get; set; }

        public int On_hand_qty { get; set; }

        public string Counted_qty { get; set; }

    
    }
}