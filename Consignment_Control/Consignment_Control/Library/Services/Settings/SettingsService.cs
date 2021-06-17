using Consignment_Control.Library.Data;
using Consignment_Control.Library.Data.SQLDomain.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consignment_Control.Library.Services.Settings
{
    public class SettingsService : ISettingsService
    {

        public IList<ConsigneeDetailsData> GetConsigneeList(string com)
        {
            try
            {
                var dt = new List<ConsigneeDetailsData>();
                using (var db = new MySqlContext())
                {
                    db.Configuration.AutoDetectChangesEnabled = false;
                    db.Configuration.ProxyCreationEnabled = false;


                    var query = "Select * from consignee_file where com='" + com + "'";

                    dt = db.Database.SqlQuery<ConsigneeDetailsData>(query).ToList();
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        public ConsigneeDetailsData GetSpecificConsigneeData(int id)
        {
            try
            {
                var dt = new ConsigneeDetailsData();
                using (var db = new MySqlContext())
                {
                    db.Configuration.AutoDetectChangesEnabled = false;
                    db.Configuration.ProxyCreationEnabled = false;


                    var query = "Select * from consignee_file where ID='" + id + "'";

                    dt = db.Database.SqlQuery<ConsigneeDetailsData>(query).FirstOrDefault();
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        public IList<ConsigneeDetailsData> GetSameConsignee_Ah_code(string com, string ah_code)
        {
            try
            {
                using (var db = new MySqlContext())
                {
                    return db.ConsigneeData.Where(x => x.Com == com && x.Ah_code == ah_code).ToList();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        public void AddNewConsignee(ConsigneeDetailsData Consignee_entity)
        {
            try
            {
             
                using (var db = new MySqlContext())
                {
                   
                    db.ConsigneeData.Add(Consignee_entity);
                    db.SaveChanges();
                }
          
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        public void EditConsignee(string com, ConsigneeDetailsData Consignee_entity)
        {

            try
            {

                //var data = new ConsigneeDetailsData();
                using (var db = new MySqlContext())
                {
                    var data = db.ConsigneeData.FirstOrDefault(x => x.ID == Consignee_entity.ID);

                    data.Ah_name = Consignee_entity.Ah_name;
                    data.Status = Consignee_entity.Status;

                    db.SaveChanges();

                }


            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}