namespace MediSight_Project.Migrations
{
    using MediSight_Project.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MediSight_Project.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MediSight_Project.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            // add the roles
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!RoleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                RoleManager.Create(role);
            }

            if (!RoleManager.RoleExists("Practitioner"))
            {
                var role = new IdentityRole();
                role.Name = "Practitioner";
                RoleManager.Create(role);
            }

            if (!RoleManager.RoleExists("Patient"))
            {
                var role = new IdentityRole();
                role.Name = "Patient";
                RoleManager.Create(role);
            }

            // add users
            if (UserManager.FindByName("admin@example.com") == null)
            {
                var user = new ApplicationUser();
                user.UserName = "admin@test.com";
                user.Email = "admin@test.com";
                string pword = "Test123!";
                var createUser = UserManager.Create(user, pword);

                // Assign role to user
                if (createUser.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Admin");
                }
            }

            if (UserManager.FindByName("practitioner@example.com") == null)
            {
                var user = new ApplicationUser();
                user.UserName = "practitioner@test.com";
                user.Email = "practitioner@test.com";
                string pword = "Test123!";
                var createUser = UserManager.Create(user, pword);

                // Assign role to user
                if (createUser.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Practitioner");
                }
            }

            if (UserManager.FindByName("patient@example.com") == null)
            {
                var user = new ApplicationUser();
                user.UserName = "patient@test.com";
                user.Email = "patient@test.com";
                string pword = "Test123!";
                var createUser = UserManager.Create(user, pword);

                // Assign role to user
                if (createUser.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Patient");
                }
            }
        }
}
}
