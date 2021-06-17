using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consignment_Control.Library.Data.SQLDomain.StockTake
{
    [System.ComponentModel.DataAnnotations.Schema.Table("stock_take_certify_log")]
    public partial class CertifyLogData
    {
        public int ID { get; set; }

        public string Com { get; set; }

        public string Tranx_no { get; set; }

        public string Ah_code { get; set; }

        public DateTime Certify_dt { get; set; }

        public string Certify_usn { get; set; }
    }
}