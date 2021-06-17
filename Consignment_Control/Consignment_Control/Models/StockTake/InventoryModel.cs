using Consignment_Control.Library.Framework.Mvc;
using System;
using System.Collections.Generic;

using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Consignment_Control.Models.StockTake
{
    public class InventoryModel : BaseModel
    {
     
        public int ID { get; set; }

    
        public string Catalog { get; set; }

     
        public string Description { get; set; }

    
        public int On_hand_qty { get; set; }

    
        public int Counted_qty { get; set; }

      
        public int On_hold_qty { get; set; }

   
        public int Counted_on_hold_qty { get; set; }

  
        //public string Ah_Code { get; set; }
    }
}