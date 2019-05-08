using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using ProdavnicaIgracaka.Logic;

namespace ProdavnicaIgracaka
{
    public partial class AddToCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string rawId = Request.QueryString["ProductID"];
            int productId;

            //provera da li je string prazan - ako nije i ako je productId int
            // TryParse pokusava od zadatog stringa da napravi int sto upisuje u productId  int.TryParse(rawId, out productId)
            if (!String.IsNullOrEmpty(rawId) && int.TryParse(rawId, out productId))
            {
                //instanciramo klasu ShoppingCartActions koja ima u sebi deklarisanu metodu AddToCart
                using (ShoppingCartActions userShoppingCart = new ShoppingCartActions())
                {
                    userShoppingCart.AddToCart(Convert.ToInt16(rawId));
                }
            }
            else
            {
                Debug.Fail("ERROR : We should never get to AddToCart.aspx without a ProductId.");
                throw new Exception("ERROR : It is illegal to load AddToCart.aspx without setting a ProductId.");
                //Nikada nećemo stići do AddToCart.aspk bez ProductId-a
                //Nije dopušteno učitavanje AddToCart.aspk bez postavljanja ProductId-a

            }
            Response.Redirect("ShoppingCart.aspx");
        }
    }
}