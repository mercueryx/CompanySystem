using Consignment_Control.Library.Data;
using Consignment_Control.Library.Data.SQLDomain.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consignment_Control.Library.Services.Report
{
    public class ReportService : IReportService
    {

        public IList<VariantCategoryData> GetCategory(string com)
        {
            try
            {
                var dt = new List<VariantCategoryData>();
                using (var db = new MySqlContext())
                {
                    db.Configuration.AutoDetectChangesEnabled = false;
                    db.Configuration.ProxyCreationEnabled = false;


                    var query = "Select  distinct Var_cate from consignment_tranx where Com='" + com + "'";
                    //Select concat(ah_code,'-',ah_name) as Ah_code,Ah_name from consignee_file where Com = '" + com + "'
                    dt = db.Database.SqlQuery<VariantCategoryData>(query).ToList();
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public IList<TransactionData> GetTransactionData(int datefrom, int dateto, string var_cate, string ah_code, string com)
        {
            try
            {
                var dt = new List<TransactionData>();
                using (var db = new MySqlContext())
                {
                    db.Configuration.AutoDetectChangesEnabled = false;
                    db.Configuration.ProxyCreationEnabled = false;


                    var query = "select ID,Tranx_no,Description,On_hand_qty,Var_qty,Var_type,Var_cate,Rmk,Add_usn,DATE_FORMAT(Add_dt, '%Y-%m-%d') as Add_dt from consignment_tranx where ah_code='" + ah_code + "' and com='" + com + "' and var_cate='"+var_cate+ "' and convert(cast(add_dt as DATE), UNSIGNED) between '" + datefrom+"' and '"+dateto+"'";

                    dt = db.Database.SqlQuery<TransactionData>(query).ToList();
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}