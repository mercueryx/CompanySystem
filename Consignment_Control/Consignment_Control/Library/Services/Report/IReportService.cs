using Consignment_Control.Library.Data.SQLDomain.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consignment_Control.Library.Services.Report
{
    public partial interface IReportService
    {
        IList<TransactionData> GetTransactionData(int datefrom, int dateto, string var_cate, string ah_code, string com);

        IList<VariantCategoryData> GetCategory(string com);

        //VariantCategoryData GetAhNameBy_Ah_code(string com, string ah_code);
    }
}