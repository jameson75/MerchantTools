using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using CipherPark.TriggerOrange.Web.Services;

[assembly: OwinStartup(typeof(CipherPark.TriggerOrange.Web.Startup))]

namespace CipherPark.TriggerOrange.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {          
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),                                           
            });

            CreateAdmin();                        
        }

        private void CreateAdmin()
        {
            string systemAdminName = "meganova75";
            string password = "trigg3rBaby75";
            if (!UserAuthServices.UserExists(systemAdminName))
                UserAuthServices.Register(systemAdminName, "meganova75@gmail.com", password, password,  TriggerOrangeRoles.Admin);
        }
    }
}