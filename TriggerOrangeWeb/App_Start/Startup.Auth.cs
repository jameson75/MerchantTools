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
            CreateBlogs();
            CreateSystemProductLists();
        }

        private void CreateAdmin()
        {
            //TODO:
            //Store this information in the config files
            string systemAdminName = "meganova75";
            string password = "trigg3rBaby75";
            if (!UserAuthServices.UserExists(systemAdminName))
                UserAuthServices.Register(systemAdminName, "meganova75@gmail.com", password, password, TriggerOrangeRoles.Admin);
        }

        private void CreateBlogs()
        {
            using (var db = new TriggerOrange.Core.Data.OrangeEntities())
            {
                foreach (var blogPage in BlogPages.All)
                {
                    if (db.Blogs.All(x => x.Id != blogPage.Id))
                        db.Blogs.Add(new Core.Data.Blog()
                        {
                            Id = blogPage.Id,
                            Name = blogPage.Name,
                            Title = blogPage.Caption
                        });
                }
                db.SaveChanges();                
            }
        }

        private void CreateSystemProductLists()
        {
            using (var db = new TriggerOrange.Core.Data.OrangeEntities())
            {
                foreach (var listName in SystemProductLists.Names)
                {
                    if (db.Reports.All(x => x.Name != listName))
                        db.Reports.Add(new Core.Data.Report()
                        {
                            Comments = $"System {listName} list",
                            RunDate = DateTime.Now,
                            IsPublic = false,
                            Name = listName,                          
                        });
                    db.SaveChanges();
                }
            }
        }
    }
}