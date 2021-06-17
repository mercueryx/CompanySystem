using Consignment_Control.Library.Data;
using Consignment_Control.Library.Data.SQLDomain.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace Consignment_Control.Library.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        #region Display Data

        public IList<InventoryData> GetInventoryDetails_Consignee(string ah_code, string com)
        {
            try
            {
                var dt = new List<InventoryData>();
                using (var db = new MySqlContext())
                {
                    db.Configuration.AutoDetectChangesEnabled = false;
                    db.Configuration.ProxyCreationEnabled = false;


                    var query = "select ID,Com,Ah_code, Catalog, Description, On_hand_qty, On_hold_qty,To_bill_qty,Pro_qty from consignee_inventory where ah_code='" + ah_code + "' and com='" + com + "'";

                    dt = db.Database.SqlQuery<InventoryData>(query).ToList();
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual InventoryData GetSelectedItem_Inventory(int id)
        {
            var dt = new InventoryData();
            using (var db = new MySqlContext())
            {
                db.Configuration.AutoDetectChangesEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;

  
               var query = "select ID,Com,Ah_code, Catalog, Description, On_hand_qty from consignee_inventory where ID='"+id+"'";

                dt = db.Database.SqlQuery<InventoryData>(query).FirstOrDefault();
            }
            return dt;
            //throw new NotImplementedException();
        }


        public void StockAdjustment(string com, string ah_code, AdjustmentData adjust_entity,int total,int ID)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {


                    using (var db = new MySqlContext())
                    {

                        db.AdjustLog.Add(adjust_entity);
                        db.SaveChanges();

                        var data = db.Stockdata.FirstOrDefault(x => x.Com == com && x.Ah_code == ah_code && x.ID == ID);

                        data.On_hand_qty = total;


                        db.SaveChanges();


                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        #endregion


    }
}