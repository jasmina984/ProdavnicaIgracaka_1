using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProdavnicaIgracaka.Models;
using ProdavnicaIgracaka.Logic;

namespace ProdavnicaIgracaka.Admin
{
    public partial class AdminPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string productAction = Request.QueryString["ProductAction"];
            if (productAction == "add")
            {
                LabelAddStatus.Text = "Proizvod je dodat!";
            }

            if (productAction == "remove")
            {
                LabelRemoveStatus.Text = "Proizvod je uklonjen!";
            }
        }
        // Kada se klikne na AddProductButton, kod proverava HasFile svojstvo kontrole FileUpload. 
        //Ako kontrola ima datoteku i ako je dozvoljen tip datoteke (na osnovu ekstenzije datoteke), slika se čuva u \
        //folderu Slike i folderu Images / Thumbs aplikacije.
        protected void AddProductButton_Click(object sender, EventArgs e)
        {
            Boolean fileOK = false;
            String path = Server.MapPath("~/Catalog/Images/");
            if (ProductImage.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(ProductImage.FileName).ToLower();
                String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOK = true;
                    }
                }
            }

            if (fileOK)
            {
                try
                {
                    // Save to Images folder.
                    ProductImage.PostedFile.SaveAs(path + ProductImage.FileName);
                    // Save to Images/Thumbs folder.
                    ProductImage.PostedFile.SaveAs(path + "Thumbs/" + ProductImage.FileName);
                }
                catch (Exception ex)
                {
                    LabelAddStatus.Text = ex.Message;
                }

                // Add product data to DB.
                AddProducts products = new AddProducts();
                bool addSuccess = products.AddProduct(AddProductName.Text, AddProductDescription.Text,
                    AddProductPrice.Text, DropDownAddCategory.SelectedValue, ProductImage.FileName);
                if (addSuccess)
                {
                    // Ponovo ucitajte stranicu.
                    string pageUrl = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.Count() - Request.Url.Query.Count());
                    Response.Redirect(pageUrl + "?ProductAction=add");
                }
                else
                {
                    LabelAddStatus.Text = "Nije moguce dodati novi proizvod u bazu podataka";
                }
            }
            else
            {
                LabelAddStatus.Text = "Nije moguce prihvatiti tip datoteke.";
            }
        }
        public IQueryable GetCategories()
        {
            //metoda GetCategories mora da instancira bazu i onda iz baze uzima samo sadrzaj tabele Categories
            var _db = new ProdavnicaIgracaka.Models.ProductContext();
            IQueryable query = _db.Categories;
            return query;
        }

        public IQueryable GetProducts()
        {
            var _db = new ProdavnicaIgracaka.Models.ProductContext();
            IQueryable query = _db.Products;
            return query;
        }

        protected void RemoveProductButton_Click(object sender, EventArgs e)
        {
            //u ovoj metodi uzimamo selectovni proizvod iz liste na osnovu DataValueField = "ProductID"
            //onda pokusamo da "dovucemo" taj proizvod iz tabele Products.
            //imamo proveru da li je null jer FirstOrDefault vraca null ukoliko ne pronadje taj proizvod.
            //ako nije NULL brisemo ga iz tabele
            //i potrebno je pozvati metodu na kontextu
            //kako bi se izmene sacuvale.
            using (var _db = new ProdavnicaIgracaka.Models.ProductContext())
            {
                int productId = Convert.ToInt16(DropDownRemoveProduct.SelectedValue);
                var myItem = (from c in _db.Products where c.ProductID == productId select c).FirstOrDefault();
                if (myItem != null)
                {
                    _db.Products.Remove(myItem);
                    _db.SaveChanges();

                    // Reload the page.
                    string pageUrl = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.Count() - Request.Url.Query.Count());
                    Response.Redirect(pageUrl + "?ProductAction=remove");
                }
                else
                {
                    LabelRemoveStatus.Text = "Nije moguce pronaci proizvod.";

                }
            }
        }
    }
}