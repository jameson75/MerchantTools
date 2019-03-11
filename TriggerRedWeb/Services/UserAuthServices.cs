using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebHttpContext = System.Web.HttpContext;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using CipherPark.TriggerOrange.Web.Models;

namespace CipherPark.TriggerOrange.Web.Services
{
    public static class UserAuthServices
    {
        public static IdentityUser Login(string userName, string password)
        {
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);
            var user = manager.Find(userName, password);
            if (user != null)
            {
                var authenticationManager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
                var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authenticationManager.SignIn(new AuthenticationProperties()
                {
                    IsPersistent = false,
                }, userIdentity);
            }
            return user;
        }

        public static void Logout()
        {
            var authenticationManager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.SignOut();
        }

        public static IdentityResult Register(string userName, string email, string password, string confirmPassword, string role = TriggerOrangeRoles.Customer)
        {            
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);

            if (manager.FindByName(userName) != null)
                return new IdentityResult("Username already exists");

            else if (manager.FindByEmail(email) != null)
                return new IdentityResult("Email already exists");

            else
            {
                var user = new IdentityUser()
                {
                    UserName = userName,
                    Email = email,
                };

                IdentityResult result = manager.Create(user, password);

                if (result.Succeeded)
                    result = user.AddToRole(role);

                return result;
            }
        }

        public static bool IsCurrentUserAuthenticated()
        {
            var authenticationManager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
            return authenticationManager.User != null;
        }

        public static bool UserExists(string userName)
        {
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);
            var user = manager.FindByName(userName);
            return (user != null);
        }
    }

    public static class TriggerOrangeRoles
    {
        public const string System = "System";
        public const string Admin = "Admin";
        public const string Customer = "Customer";
        public const string Developer = "Developer";
        public static string[] All { get { return new string[] { System, Admin, Customer, Developer }; } }
    }

    public static class IdentityUserExtension
    {
        public static IdentityResult AddToRole(this IdentityUser u, string roleName)
        {
            //Ensure the role exists in the store.
            var roleStore = new RoleStore<IdentityRole>();
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            if (roleManager.FindByName(roleName) == null)
                roleManager.Create(new IdentityRole() { Name = roleName });

            var userStore = new UserStore<IdentityUser>();
            var userManager = new UserManager<IdentityUser>(userStore);

            //Ensure the user exists in the store.
            var user = userManager.FindByName(u.UserName);
            if (user == null)
                return IdentityResult.Failed("User not found");

            userManager.AddToRole(user.Id, roleName);

            return IdentityResult.Success;
        }
    }
}
