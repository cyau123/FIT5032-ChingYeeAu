using FIT5032_Assignment_v1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FIT5032_Assignment_v1.Startup))]
namespace FIT5032_Assignment_v1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesAndUsers();
            app.MapSignalR();
        }

        private void createRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Receptionist"))
            {
                //create the role Receptionist
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Receptionist";
                roleManager.Create(role);

                //create a Receptionist
                var user = new ApplicationUser();
                user.UserName = "rcp001@hq.com";
                user.Email = "rcp001@hq.com";
                user.FirstName = "Megan";
                user.LastName = "Cheung";

                string userPwd = "Potato1!";
                var newUser = userManager.Create(user, userPwd);
                
                if (newUser.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, "Receptionist");
                }
            }

            //create the role Patient
            if (!roleManager.RoleExists("Patient"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Patient";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Admin"))
            {
                //create the role Admin
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //create an Admin
                var user = new ApplicationUser();
                user.UserName = "admin@hq.com";
                user.Email = "admin@hq.com";
                user.FirstName = "Admin";
                user.LastName = "One";

                string userPwd = "Potato1!";
                var newUser = userManager.Create(user, userPwd);

                if (newUser.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, "Admin");
                }
            }
        }
    }
}
