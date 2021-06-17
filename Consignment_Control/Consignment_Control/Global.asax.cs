using Autofac;
using Autofac.Integration.Mvc;
using Consignment_Control.Library.Data;
using Consignment_Control.Library.Services.Authentication;
using Consignment_Control.Library.Services.Common;
using Consignment_Control.Library.Services.General;
using Consignment_Control.Library.Services.Inventory;
using Consignment_Control.Library.Services.Report;
using Consignment_Control.Library.Services.Security;
using Consignment_Control.Library.Services.Settings;
using Consignment_Control.Library.Services.StockTake;
using Consignment_Control.Library.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Consignment_Control
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RegisterAutoFac();

            using (var db = new MySqlContext())
            {
                var aa = db.Users.AsNoTracking().Where(x => x.ID == 0).FirstOrDefault();
            }

        }

        protected void RegisterAutoFac()
        {
            var builder = new ContainerBuilder();

            //HTTP context and other related stuff
            builder.Register(c =>
                new HttpContextWrapper(HttpContext.Current) as HttpContextBase)
                .As<HttpContextBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Request)
                .As<HttpRequestBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Response)
                .As<HttpResponseBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Server)
                .As<HttpServerUtilityBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Session)
                .As<HttpSessionStateBase>()
                .InstancePerLifetimeScope();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterSource(new ViewRegistrationSource());

            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();

            builder.RegisterType<UserAccountService>().As<IUserAccountService>().InstancePerLifetimeScope();

            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();

            // Cookies Service
            builder.RegisterType<CookieService>().As<ICookieService>().InstancePerLifetimeScope();

            // Security Service 
            builder.RegisterType<EncryptionService>().As<IEncryptionService>().InstancePerLifetimeScope();      

            // Authentication
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();

            builder.RegisterType<StockTakeService>().As<IStockTakeService>().InstancePerLifetimeScope();

            builder.RegisterType<ConsigneeService>().As<IConsigneeService>().InstancePerLifetimeScope();

            builder.RegisterType<InventoryService>().As<IInventoryService>().InstancePerLifetimeScope();

            builder.RegisterType<ReportService>().As<IReportService>().InstancePerLifetimeScope();

            builder.RegisterType<SettingsService>().As<ISettingsService>().InstancePerLifetimeScope();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
