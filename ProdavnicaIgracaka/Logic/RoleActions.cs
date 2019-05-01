using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProdavnicaIgracaka.Models; // dodata su tri using-a
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ProdavnicaIgracaka.Logic
{
    internal class RoleActions
    {
        internal void AddUserAndRole()
        {
            // Pristupanje aplikaciji context and kreiranje varijable result .
            Models.ApplicationDbContext context = new ApplicationDbContext();
            IdentityResult IdRoleResult;
            IdentityResult IdUserResult;

            //Kreiranje RoleStore objekta pomoću objekta ApplicationDbContext.
            // RoleStore-u je dozvoljeno samo da sadrzi IdentityRole objekte. 
            var roleStore = new RoleStore<IdentityRole>(context);

            // Kreiranje RoleManager objekta kojem je dozvoljeno da sadrzi IdentityRole objekte. 
            // Prilikom kreiranja RoleManager objekta, prosledjen (kao parametar) je RoleStore object. 
            var roleMgr = new RoleManager<IdentityRole>(roleStore);

            // Zatim kreiramo "admin" role ako vec ne postoji.
            if (!roleMgr.RoleExists("admin"))
            {
                IdRoleResult = roleMgr.Create(new IdentityRole { Name = "admin" });
            }

            // Kreiranje objekta UserManager  na osnovu objekta UserStore i objekta ApplicationDbContext.
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var appUser = new ApplicationUser
            {
                UserName = "administrator@prodavnicaigracaka.com",
                Email = "administrator@prodavnicaigracaka.com"
            };
            IdUserResult = userMgr.Create(appUser, "Pa$$word1");

            // Ako je novi korisnik "admin" uspesno kreiran, dodajte korisniku "admin" (role) ulogu admina
            if (!userMgr.IsInRole(userMgr.FindByEmail("administrator@prodavnicaigracaka.com").Id, "admin"))
            {
                IdUserResult = userMgr.AddToRole(userMgr.FindByEmail("administrator@prodavnicaigracaka.com").Id, "admin");
            }
        }
    }
}