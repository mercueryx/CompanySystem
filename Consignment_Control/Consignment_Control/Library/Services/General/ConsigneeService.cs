using Consignment_Control.Library.Data;
using Consignment_Control.Library.Data.SQLDomain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consignment_Control.Library.Services.General
{
    public class ConsigneeService : IConsigneeService
    {
        public IList<Consignee> GetAhCode_Name(string com)
        {
            try
            {
                var dt = new List<Consignee>();
                using (var db = new MySqlContext())
                {
                    db.Configuration.AutoDetectChangesEnabled = false;
                    db.Configuration.ProxyCreationEnabled = false;


                    var query = "Select  Ah_code,Ah_name from consignee_file where Com='" + com + "'";
                    //Select concat(ah_code,'-',ah_name) as Ah_code,Ah_name from consignee_file where Com = '" + com + "'
                    dt = db.Database.SqlQuery<Consignee>(query).ToList();
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Consignee GetAhNameBy_Ah_code(string com,string ah_code)
        {
            try
            {
                var dt = new Consignee();
                using (var db = new MySqlContext())
                {
                    db.Configuration.AutoDetectChangesEnabled = false;
                    db.Configuration.ProxyCreationEnabled = false;


                    var query = "Select Ah_name from consignee_file where Com='" + com + "' and Ah_code='"+ah_code+"'";
                    //Select concat(ah_code,'-',ah_name) as Ah_code,Ah_name from consignee_file where Com = '" + com + "'
                    dt = db.Database.SqlQuery<Consignee>(query).FirstOrDefault();
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