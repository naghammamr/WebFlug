using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using WebFlug.Models;

[assembly: OwinStartupAttribute(typeof(WebFlug.Startup))]
namespace WebFlug
{
    public partial class Startup
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
          //  CreateRoles();
            //CreateAdmin();
        }

        /*public void CreateAdmin()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = new ApplicationUser();
            user.Email = "admin@flug.com";
            user.UserName = "Admin";
            var check = userManager.Create(user, "@El7el7el7");
            if (check.Succeeded)
            {
                userManager.AddToRole(user.Id, "Admin");
            }
        }
        
        public void CreateRoles()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            IdentityRole role;
            if(!roleManager.RoleExists("Admin"))
            {
                role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("User"))
            {
                role = new IdentityRole();
                role.Name = "User";
                roleManager.Create(role);
            }
        }*/
    }
}
