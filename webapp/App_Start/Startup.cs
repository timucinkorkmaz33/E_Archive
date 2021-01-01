using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SmartAdminMvc.Identity;

[assembly: OwinStartup(typeof(SmartAdminMvc.App_Start.Startup))]

namespace SmartAdminMvc.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
    LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<IdentityConfig.ApplicationUserManager, ApplicationUser>(
            validateInterval: TimeSpan.FromMinutes(15),
            regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager)),
                },
                SlidingExpiration = false,
                ExpireTimeSpan = TimeSpan.FromMinutes(30)
            });
        }
    }
}
