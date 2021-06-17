using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using Consignment_Control.Library.Data.SQLDomain.Users;
using MySql.Data.Entity;
using Consignment_Control.Library.Data.SQLDomain.StockTake;
using Consignment_Control.Library.Data.SQLDomain.Inventory;
using Consignment_Control.Library.Data.SQLDomain.Report;
using Consignment_Control.Library.Data.SQLDomain.Settings;

namespace Consignment_Control.Library.Data
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MySqlContext : DbContext
    {

        public MySqlContext() : base(nameOrConnectionString: "ConsignDB") { }
        public DbSet<User> Users { set; get; }

        public DbSet<StockTakeData> Stockdata { set; get; }


        //public DbSet<TranxNumberData> TranxNo { set; get; }

        public DbSet<StockTakeLogData>StockLog { set; get; }

        public DbSet<AdjustmentData> AdjustLog { set; get; }

        public DbSet<ConsigneeDetailsData> ConsigneeData { set; get; }


        public DbSet<CertifyLogData> CertifyTakeLog { set; get; }

        //public DbSet<TransactionData> TranxData { set; get; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // To remove the requests to the Migration History table
            Database.SetInitializer<MySqlContext>(null);
            // To remove the plural names   
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}