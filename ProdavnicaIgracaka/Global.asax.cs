using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Data.Entity;
using ProdavnicaIgracaka.Models;
using ProdavnicaIgracaka.Logic;

namespace ProdavnicaIgracaka
{
    public class Global : HttpApplication
    {
        //Kada se pokrene aplikacija ona poziva Application_Start upravljača događaja
        void Application_Start(object sender, EventArgs e)
        {
            // Kod koji radi na pokretanju aplikacije
            //Poziv metode RegisterRoutes koristeći objekat RouteConfig na početku upravljača događaja Application_Start je poziv  
            //napravljen da primeni podrazumevano rutiranje.
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Inicijalizacija baze podataka proizvoda.
            Database.SetInitializer(new ProductDatabaseInitializer());

            // Kreiranje custom role i korisnika.
            RoleActions roleActions = new RoleActions();
            roleActions.AddUserAndRole();

            // Dodavanje Routes (rute)
            RegisterCustomRoutes(RouteTable.Routes);
        }
        // Metoda RegisterCustomRoutes dodaje svaku rutu pozivanjem MapPageRoute metode objekta RouteCollection.
        //Rute se definišu koristeći ime rute, URL rute i fizičku URL adresu.
        void RegisterCustomRoutes(RouteCollection routes)
        {
            //"ProductsByCategoryRoute" je ime rute i koristi se za pozivanje rute kada je to potrebno.
            //"Category/{categoryName}") definiše prijateljski zamenjeni URL koji može biti dinamičan na osnovu koda. 
            //CategoryName je promenljiva koja će se koristiti za određivanje ispravne putanje rute.
            //Ova ruta se koristi kada popunjavate kontrolu podataka vezama koje se generišu na osnovu podataka.
            routes.MapPageRoute(
                "ProductsByCategoryRoute",
                "Category/{categoryName}",
                "~/ProductList.aspx"
            );
            routes.MapPageRoute(
                "ProductByNameRoute",
                "Product/{prdId}",
                "~/ProductDetails.aspx"
            );

        }
        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs.

            // Get last error from the server
            Exception exc = Server.GetLastError();

            if (exc is HttpUnhandledException)
            {
                if (exc.InnerException != null)
                {
                    exc = new Exception(exc.InnerException.Message);
                    Server.Transfer("ErrorPage.aspx?handler=Application_Error%20-%20Global.asax",
                        true);
                }
            }
        }
    }
}