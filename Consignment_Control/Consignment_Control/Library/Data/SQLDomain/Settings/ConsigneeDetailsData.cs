using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consignment_Control.Library.Data.SQLDomain.Settings
{
    [System.ComponentModel.DataAnnotations.Schema.Table("consignee_file")]
    public partial class ConsigneeDetailsData
    {

        public int ID { get; set; }

        public string Com { get; set; }

        public string Ah_code { get; set; }

        public string Ah_name { get; set; }

        public string Loc { get; set; }

        public string Status { get; set; }

        public DateTime Add_dt { get; set; }

        public string Add_usn { get; set; }
    }
}