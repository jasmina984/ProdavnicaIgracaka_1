using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProdavnicaIgracaka.Models; // dodato
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ProdavnicaIgracaka.Logic
{
    internal class RoleActions
    {
        internal void AddUserAndRole()
        {
            //Prvo se uspostavlja kontext baze podataka za bazu podataka o članstvu. 
            //Baza podataka o članstvu se takođe čuva kao .mdf datoteka u fascikli App_Data. 

            // Pristupanje aplikaciji context i kreiranje varijable result .
            Models.ApplicationDbContext context = new ApplicationDbContext();
            IdentityResult IdRoleResult;
            IdentityResult IdUserResult;

            //Kreiranje RoleStore objekta pomoću objekta ApplicationDbContext.
            // RoleStore objekat, koji pruža upravljanje ulogama, kreira se na osnovu konteksta baze podataka.
            // RoleStore-u je dozvoljeno samo da sadrzi IdentityRole objekte. 
            var roleStore = new RoleStore<IdentityRole>(context);

            // Kreiranje RoleManager objekta kojem je dozvoljeno da sadrzi IdentityRole objekte. 
            // Prilikom kreiranja RoleManager objekta, prosledjen (kao parametar) je RoleStore objekat. 
            var roleMgr = new RoleManager<IdentityRole>(roleStore);

            // Zatim kreiramo "admin" ulogu (role) ako vec ne postoji.
            //Metoda RoleExists se poziva da bi se odredilo da li je uloga „admin“ prisutna u bazi podataka o članstvu. 
            //Ako nije, onda kreirate ulogu.
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