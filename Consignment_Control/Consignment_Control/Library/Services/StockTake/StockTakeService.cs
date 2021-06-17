using Consignment_Control.Library.Data;
using Consignment_Control.Library.Data.SQLDomain.StockTake;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Web;

namespace Consignment_Control.Library.Services.StockTake
{
    public class StockTakeService : IStockTakeService
    {
     
        #region Display Data

        public IList<StockTakeData> GetAllCatalogByAhCode(string ah_code,string com)
        {
            try
            {
                var dt = new List<StockTakeData>();
                using (var db = new MySqlContext())
                {
                    db.Configuration.AutoDetectChangesEnabled = false;
                    db.Configuration.ProxyCreationEnabled = false;

                
                    var query = "select ID,Com,Ah_code, Catalog, Description, On_hand_qty, Counted_qty,To_bill_qty,Pro_qty from consignee_inventory where ah_code='"+ah_code+"' and com='"+com+"'";

                    dt = db.Database.SqlQuery<StockTakeData>(query).ToList();
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual StockTakeData GetSelectedItem_Inventory(int id)
        {
            using (var db = new MySqlContext())
            {
                return db.Stockdata.FirstOrDefault(x => x.ID == id);
            }
         
            //throw new NotImplementedException();
        }

        public IList<StockTakeLogData> GetAllStockTakeLog(string com,string ah_code,string status)
        {
            try
            {
                var dt = new List<StockTakeLogData>();
                using (var db = new MySqlContext())
                {
                    dt = db.StockLog.Where(x => x.Com==com &&  x.Ah_code == ah_code && x.Status == status).ToList();
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IList<StockTakeLogData> GetStockTakeLog_NotInStatus(string com, string ah_code, string status)
        {
            try
            {
              
                var dt = new List<StockTakeLogData>();
                using (var db = new MySqlContext())
                {

                    dt = db.StockLog.Where(x => x.Com == com && x.Ah_code == ah_code && x.Status == status ).ToList();
                   // dt = db.StockLog.Where(x => x.Com == com && x.Ah_code == ah_code && x.Status != status && x.Status != "X" ).ToList();
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public IList<StockTakeLogData> GetStockTakeLog_Certify(string com, string ah_code, string status)
        {
            try
            {

                var dt = new List<StockTakeLogData>();
                using (var db = new MySqlContext())
                {

                    dt = db.StockLog.Where(x => x.Com == com && x.Ah_code == ah_code && x.Status == status && x.Variant_qty > 0).ToList();
                    // dt = db.StockLog.Where(x => x.Com == com && x.Ah_code == ah_code && x.Status != status && x.Status != "X" ).ToList();
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IList<PdfData> GetInventory_AhCode(string ah_code, string com)
        {
            try
            {
                var dt = new List<PdfData>();
                using (var db = new MySqlContext())
                {
                    db.Configuration.AutoDetectChangesEnabled = false;
                    db.Configuration.ProxyCreationEnabled = false;


                    var query = "select Ah_code,Description,On_hand_qty,Counted_qty   from consignee_inventory where ah_code='" + ah_code + "' and com='" + com + "'";

                    
                    dt = db.Database.SqlQuery<PdfData>(query).ToList();
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IList<StockTakeLogData> GetOpenStockTakeLog(string ah_code, string com)
        {
            try
            {
                var dt = new List<StockTakeLogData>();
                using (var db = new MySqlContext())
                {
                    db.Configuration.AutoDetectChangesEnabled = false;
                    db.Configuration.ProxyCreationEnabled = false;


                    var query = "select * from consignee_stocktake_log where ah_code='" + ah_code + "' and com='" + com + "' and status ='O'";


                    dt = db.Database.SqlQuery<StockTakeLogData>(query).ToList();
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public StockTakeLogData GetLogVariantQty_Id(int id)
        {
            try
            {
                var dt = new StockTakeLogData();
                using (var db = new MySqlContext())
                {
                    db.Configuration.AutoDetectChangesEnabled = false;
                    db.Configuration.ProxyCreationEnabled = false;


                    dt = db.StockLog.FirstOrDefault(x => x.ID == id);


                  
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Checking
        public IList<StockTakeLogData> GetLog_Catalog(string com, string ah_code)
        {
            using (var db = new MySqlContext())
            {
                return db.StockLog.Where(x => x.Com == com && x.Ah_code == ah_code  && x.Status == "O").ToList();

            }
        }

   
        #endregion


        #region Action
 

        public void StockTakeSubmit(string com, string ah_code,List<StockTakeLogData> stock_log,string tranx_no)
        {
            try
            {
                //int tem_counted_qty, tem_counted_on_hold_qty;
           


                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    using (var db = new MySqlContext())
                    {
                      
                        foreach (var each_log_entity in stock_log)
                        {
                            db.StockLog.Add(each_log_entity);
                            db.SaveChanges();
                        }


                        db.Configuration.AutoDetectChangesEnabled = false;
                        db.Configuration.ProxyCreationEnabled = false;


                        var query = "Update consignee_stocktake_log set Status ='X' where Com='"+com+"' and Ah_code='"+ah_code+"' and Status='O' and Tranx_no <>'"+ tranx_no+"'";

                        db.Database.ExecuteSqlCommand(query);

                    


                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }


        public void StockTakeCertify(string com, string ah_code, List<StockTakeLogData> stock_log, string tranx_no, CertifyLogData certifylog_entity)
        {
            try
            {
                //int tem_counted_qty, tem_counted_on_hold_qty;



                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    using (var db = new MySqlContext())
                    {

                        foreach (var each_stock_log_entity in stock_log)
                        {
                          
                            var data = db.Stockdata.FirstOrDefault(x => x.Com == com && x.Ah_code == ah_code && x.Catalog == each_stock_log_entity.Catalog);

                            data.On_hand_qty = each_stock_log_entity.Counted_qty;

                            data.To_bill_qty = data.To_bill_qty + each_stock_log_entity.Variant_qty;

                            db.SaveChanges();
                        }

                        //save certify log
                        db.CertifyTakeLog.Add(certifylog_entity);
                        db.SaveChanges();


                        db.Configuration.AutoDetectChangesEnabled = false;
                        db.Configuration.ProxyCreationEnabled = false;


                        var query = "Update consignee_stocktake_log set Status ='C' where Com='" + com + "' and Ah_code='" + ah_code + "' and Status='O' and Tranx_no ='" + tranx_no + "'";

                        db.Database.ExecuteSqlCommand(query);


                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }


        public void StockDelivery(string com, string ah_code, string catalog, int var_qty,string tranx_no,int id)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    using (var db = new MySqlContext())
                    {






                            var data = db.Stockdata.FirstOrDefault(x => x.Com == com && x.Ah_code == ah_code && x.Catalog == catalog);

                        

                            data.To_bill_qty = data.To_bill_qty - var_qty;

                            db.SaveChanges();
                       


                        db.Configuration.AutoDetectChangesEnabled = false;
                        db.Configuration.ProxyCreationEnabled = false;


                        var query = "Update consignee_stocktake_log set Status ='D' where ID='"+id+"'";

                        db.Database.ExecuteSqlCommand(query);


                    }
                    scope.Complete();
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