using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProdavnicaIgracaka.Models;
using System.Web.ModelBinding;


namespace ProdavnicaIgracaka
{
    public partial class ProductDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //izmenjeno i dodato za route
        public IQueryable<Product> GetProduct(
                   [QueryString("ProductID")] int? productId,
                   [RouteData] int prdId)
        {
            var _db = new ProdavnicaIgracaka.Models.ProductContext();
            IQueryable<Product> query = _db.Products;

            if (prdId > 0)
            {
                query = query.Where(p => p.ProductID == prdId);
            }
            //else if (!String.IsNullOrEmpty(productName))
            //{
            //    query = query.Where(p =>
            //            String.Compare(p.ProductName, productName) == 0);
            //}
            else
            {
                query = null;
            }           

            return query;
        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        //public IQueryable<ProdavnicaIgracaka.Models.ProductImage> productImages_GetData()
        //{
        //    return null;
        //}
        //public IQueryable<Product> GetProduct([QueryString("productID")]int? productId)
        //{
        //    var _db = new ProdavnicaIgracaka.Models.ProductContext();
        //    IQueryable<Product> query = _db.Products;
        //    if (productId.HasValue && productId > 0)
        //    {
        //        query = query.Where(p => p.ProductID == productId);
        //    }
        //    else
        //    {
        //        query = null;
        //    }
        //    return query;
        //}
    }
}