using Consignment_Control.Library.Data.SQLDomain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consignment_Control.Library.Services.General
{
    public partial interface IConsigneeService
    {
        IList<Consignee> GetAhCode_Name(string com);

        Consignee GetAhNameBy_Ah_code(string com, string ah_code);
    }
}