using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProdavnicaIgracaka.Models;
using System.Web.ModelBinding;
using System.Web.Routing; // dodato za route

namespace ProdavnicaIgracaka
{
    public partial class ProductList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //Ovde imamo samo deklarisanu metodu GetProducts koja predstavlja vrednost prethodno pomenutog svojstva SelectMethod 
        //i vraca tip IQueriable<Products>-obezbeđuje funkcionalnost za procenu upita prema određenom izvoru podataka 
        //gde je tip podataka(Proizvodi) poznat.
        public IQueryable<Product> GetProducts(
                            [QueryString("id")] int? categoryId,
                            [RouteData] string categoryName)
        {
            var _db = new ProdavnicaIgracaka.Models.ProductContext();
            IQueryable<Product> query = _db.Products;

            if (categoryId.HasValue && categoryId > 0)
            {
                //vrati sve proizvode cija kolona CategoryID je ista kao categoryId  koji je dosao iz query string-a jer koristimo IQueryable
                query = query.Where(p => p.CategoryID == categoryId);
            }
            //String.Compare vraca nulu ukoliko nema razlika izmedju naziva kategorije na proizvodu i categoryName prosledjenog kroz route url.
            if (!String.IsNullOrEmpty(categoryName))
            {
                query = query.Where(p =>
                    String.Compare(p.Category.CategoryName,
                    categoryName) == 0);
            }
            //Te ce se tako query vratiti samo za proizvode kojima je kategorija tog naziva.
            return query;
        }
        
    }
}