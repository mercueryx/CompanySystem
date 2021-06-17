using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consignment_Control.Library.Data.SQLDomain.Inventory
{
    [System.ComponentModel.DataAnnotations.Schema.Table("consignment_tranx")]
    public class AdjustmentData
    {
        public int ID { get; set; }

        public string Com { get; set; }

        public string Tranx_no { get; set; }

        public string Status { get; set; }

        public string Ah_code { get; set; }

        public string Catalog { get; set; }

        public string Description { get; set; }

        public string Loc { get; set; }

        public int On_hand_qty { get; set; }

        public int Var_qty { get; set; }

        public string Var_type { get; set; }

        public string Var_cate { get; set; }

        public string Rmk { get; set; }

        public string Add_usn { get; set; }

        public DateTime Add_dt { get; set; }
    }
}