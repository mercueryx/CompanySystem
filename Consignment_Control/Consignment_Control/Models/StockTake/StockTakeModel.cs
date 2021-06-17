
using Consignment_Control.Library.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Consignment_Control.Models.StockTake
{
    public class StockTakeModel : BaseModel
    {

        [DisplayName("ID")]
        public int ID { get; set; }

        [DisplayName("Catalog No")]
        public string Catalog { get; set; }
       
        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("On Hand Qty")]
        public int On_hand_qty { get; set; }

        [DisplayName("Counted Qty")]
        public int Counted_qty { get; set; }

        //[DisplayName("On Hold Qty")]
        //public int On_hold_qty { get; set; }

        //[DisplayName("Counted On Hold Qty")]
        //public int Counted_on_hold_qty { get; set; }

        [DisplayName("Ah Code")]
        public string Ah_Code { get; set; }

        [DisplayName("Consignee")]
        public string AccountHolderCode { get; set; }

        public IList<SelectListItem> SelectAccountHolder { get; set; } = new List<SelectListItem>();



        [DisplayName("Stock Take Status")]
        public string StatusCode { get; set; }


        public IList<InventoryModel> Inv_list { get; set; }= new List<InventoryModel>();


        public InventoryModel Inv_Data { get; set; }



        public IList<StockTakeLogModel> Log_List { get; set; } = new List<StockTakeLogModel>();

        public StockTakeLogModel Log_Data { get; set; }

        //public List<string> TestList { get; set; }

    }



}