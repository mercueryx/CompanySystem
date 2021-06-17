using Consignment_Control.Library.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Consignment_Control.Models.Inventory
{
    public class InventoryDetailsModel : BaseModel
    {

        [DisplayName("ID")]
        public int ID { get; set; }

        [DisplayName("Ah Code")]
        public string Ah_Code { get; set; }


        [DisplayName("Catalog No")]
        public string Catalog { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("On Hand Qty")]
        public int On_hand_qty { get; set; }

        //[DisplayName("To Bill Qty")]
        //public int To_bill_qty { get; set; }

        //[DisplayName("On Hold Qty")]
        //public int On_hold_qty { get; set; }

        //[DisplayName("Process Qty")]
        //public int Pro_qty { get; set; }

        public string Var_type { get; set; }

        [DisplayName("Adjust Qty")]
        public int Adjust_qty { get; set; }

        [DisplayName("Remark")]
        public string Rmk  { get; set; }

        [DisplayName("Consignee")]
        public string AccountHolderCode { get; set; }


     
        public string Var_cate { get; set; }

        public IList<SelectListItem> SelectAccountHolder { get; set; } = new List<SelectListItem>();

    }
}