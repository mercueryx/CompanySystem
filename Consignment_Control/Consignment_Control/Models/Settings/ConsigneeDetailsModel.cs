using Consignment_Control.Library.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Consignment_Control.Models.Settings
{
    public class ConsigneeDetailsModel : BaseModel
    {

        [DisplayName("ID")]
        public int ID { get; set; }


        [DisplayName("Consignee Code")]
        public string Ah_code { get; set; }


        [DisplayName("Consignee Name")]
        public string Ah_name { get; set; }



        [DisplayName("Status")]
        public string Status { get; set; }


        public IList<SelectListItem> SelectStatus { get; set; } = new List<SelectListItem>();
    }
}