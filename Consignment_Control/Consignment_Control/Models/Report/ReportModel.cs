using Consignment_Control.Library.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Consignment_Control.Models.Report
{
    public class ReportModel : BaseModel
    {

        [DisplayName("Consignee")]
        public string Ah_code { get; set; }

        public string Status { get; set; }

        [DisplayName("Report Category")]
        public string Category { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public IList<SelectListItem> SelectAccountHolder { get; set; } = new List<SelectListItem>();

        public IList<SelectListItem> SelectCategory{ get; set; } = new List<SelectListItem>();

        public IList<TransactionModel> Tranx_list { get; set; } = new List<TransactionModel>();

        public TransactionModel Tranx_Data { get; set; }
    }
}