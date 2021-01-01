#region Using

using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using SmartAdminMvc.Controllers;
using SmartAdminMvc.Entity;
using SmartAdminMvc.Identity;

#endregion

namespace SmartAdminMvc
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            //AreaRegistration.RegisterAllAreas();
            IdentityConfig.RegisterIdentities();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
             AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ProjectDb db = new ProjectDb();
            RoleStore<ApplicationRole> roleStore = new RoleStore<ApplicationRole>(db);
            RoleManager<ApplicationRole> roleManager = new RoleManager<ApplicationRole>(roleStore);

            if (!roleManager.RoleExists("Admin"))
            {
                ApplicationRole adminRole = new ApplicationRole("Admin", "Sistem yöneticisi");
                roleManager.Create(adminRole);
            }
            if (!roleManager.RoleExists("Editor"))
            {
                ApplicationRole adminRole = new ApplicationRole("Editor", "Editor");
                roleManager.Create(adminRole);
            }

            if (!roleManager.RoleExists("User"))
            {
                ApplicationRole userRole = new ApplicationRole("User", "Sistem kullanıcısı, yorum eklemek için gereklidir");
                roleManager.Create(userRole);
            }
            // Rol tanımlama adımları
        }
        protected void Session_End(object sender, EventArgs e)
        {

            System.Diagnostics.Debug.WriteLine("Session_End");
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Session_Start");
        }



    }
}
