using Consignment_Control.Library.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consignment_Control.Models.Report
{
    public class TransactionModel : BaseModel
    {
        public int ID { get; set; }

        //public string Com { get; set; }

        public string Tranx_no { get; set; }

     

        //public string Ah_code { get; set; }

  

        public string Description { get; set; }

        public int On_hand_qty { get; set; }

     

        public int Var_qty { get; set; }

        public string Var_type { get; set; }

        public string Var_cate { get; set; }

        public string Rmk { get; set; }

        public string Add_dt { get; set; }

        public string Add_usn { get; set; }

    }
}