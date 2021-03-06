﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProyectoIHER.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ProyectoIHER
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //migracion automatica
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<
                Models.ProyectoIHERContext, Migrations.Configuration>());
            //establece conexion con la BD
            ApplicationDbContext db = new ApplicationDbContext();
            CreateRoles(db);
            CreateSuperUser(db);
            AddPermisionToSuperUser(db);
            db.Dispose();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void AddPermisionToSuperUser(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            var user = userManager.FindByName("admin@iher.hn");

            if (!userManager.IsInRole(user.Id, "Ver"))
            {
                userManager.AddToRole(user.Id, "Ver");
            }

            if (!userManager.IsInRole(user.Id, "Editar"))
            {
                userManager.AddToRole(user.Id, "Editar");
            }

            if (!userManager.IsInRole(user.Id, "Crear"))
            {
                userManager.AddToRole(user.Id, "Crear");
            }

            if (!userManager.IsInRole(user.Id, "Eliminar"))
            {
                userManager.AddToRole(user.Id, "Eliminar");
            }
        }

        //crear super usuario automaticamente
        private void CreateSuperUser(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var user = userManager.FindByName("admin@iher.hn");
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "admin@iher.hn",
                    Email = "admin@iher.hn"
                };
                userManager.Create(user, "KaThonic2793!");
            }
        }

        private void CreateRoles(ApplicationDbContext db)
        {
            //permite manipular los roles
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            if (!roleManager.RoleExists("Ver"))
            {
                roleManager.Create(new IdentityRole("Ver"));
            }

            if (!roleManager.RoleExists("Editar"))
            {
                roleManager.Create(new IdentityRole("Editar"));
            }

            if (!roleManager.RoleExists("Crear"))
            {
                roleManager.Create(new IdentityRole("Crear"));
            }

            if (!roleManager.RoleExists("Eliminar"))
            {
                roleManager.Create(new IdentityRole("Eliminar"));
            }
        }

    }
}
