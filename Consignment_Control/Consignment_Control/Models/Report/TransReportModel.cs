using Consignment_Control.Library.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consignment_Control.Models.Report
{
    public class TransReportModel : BaseModel
    {
        public int ID { get; set; }

        //public string Com { get; set; }

        public string Tranx_no { get; set; }



        //public string Ah_code { get; set; }



        public string Description { get; set; }

        public int On_hand_qty { get; set; }



        public int Variant_qty { get; set; }

        public string Variant_type { get; set; }

        public string Variant_category { get; set; }

        public string Remark { get; set; }

        public string Add_time { get; set; }

        public string Add_user { get; set; }

    }
}