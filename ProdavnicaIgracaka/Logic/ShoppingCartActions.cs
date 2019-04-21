using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProdavnicaIgracaka.Models;

namespace ProdavnicaIgracaka.Logic
{
    public class ShoppingCartActions : IDisposable
    {
        public string ShoppingCartId { get; set; }
        private ProductContext _db = new ProductContext();
        public const string CartSessionKey = "CartId";
        public void AddToCart(int id)
        {
            //  Preuzmi proizvod iz baze podataka
            ShoppingCartId = GetCartId();

            var cartItem = _db.ShoppingCartItems.SingleOrDefault(
                c => c.CartId == ShoppingCartId && c.ProductId == id);
            if (cartItem == null)
            {
                // Kreirajte novu stavku korpe ako ne postoji stavka u korpi
                cartItem = new CartItem
                {
                    ItemId = Guid.NewGuid().ToString(),
                    ProductId = id,
                    CartId = ShoppingCartId,
                    Product = _db.Products.SingleOrDefault(
                        p => p.ProductID == id),
                    Quantity = 1,
                    DateCreated = DateTime.Now
                };
                _db.ShoppingCartItems.Add(cartItem);
            }
            else
            {
                //Ako stavka postoji u korpi onda je dodajte u quantity.
                cartItem.Quantity++;
            }
            _db.SaveChanges();
        }
        public void Dispose()
        {
            if (_db != null)
            {
                _db.Dispose();
                _db = null;
            }
        }
        public string GetCartId()
        {
            if (HttpContext.Current.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))
                {
                    HttpContext.Current.Session[CartSessionKey] = HttpContext.Current.User.Identity.Name;
                }
                else
                {
                    // Generisite novi nasumicni GUID pomocu  System.Guid class.
                    Guid tempCartId = Guid.NewGuid();
                    HttpContext.Current.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return HttpContext.Current.Session[CartSessionKey].ToString();
        }
        public List<CartItem> GetCartItems()
        {
            ShoppingCartId = GetCartId();

            return _db.ShoppingCartItems.Where(
                c => c.CartId == ShoppingCartId).ToList();
        }
        public decimal GetTotal()
        {
            //GetTotal metoda dobija ID od ShoppingCart. Tada metoda dobija total korpe tako što množi cenu proizvoda sa 
            //količinom proizvoda za svaki proizvod naveden u korpi.
            ShoppingCartId = GetCartId();
            decimal? total = decimal.Zero;
            total = (decimal?)(from cartItems in _db.ShoppingCartItems where cartItems.CartId == ShoppingCartId select (int?)cartItems.Quantity * cartItems.Product.UnitPrice).Sum();
            return total ?? decimal.Zero;
        }
        public ShoppingCartActions GetCart(HttpContext context)
        {
            using (var cart = new ShoppingCartActions())
            {
                cart.ShoppingCartId = cart.GetCartId();
                return cart;
            }
        }
        //Metod UpdateShoppingCartDatabase, pozvan iz metode UpdateCartItems na stranici ShoppingCart.aspx.cs, 
        //sadrži logiku za ažuriranje ili uklanjanje stavki iz korpe za kupovinu. 
        //Metoda UpdateShoppingCartDatabase ponavlja sve redove u listi korpe za kupovinu.
        //Ako je stavka korpe za kupovinu označena da bude uklonjena ili je količina manja od jedne, poziva se metoda RemoveItem. 
        //U suprotnom, stavka korpe za kupovinu se proverava da li postoji ažuriranje kada se pozove metoda UpdateItem. 
        //Nakon što je stavka korpe za kupovinu uklonjena ili ažurirana, promene baze podataka su sacuvane.
        public void UpdateShoppingCartDatabase(String cartId, ShoppingCartUpdates[] CartItemUpdates)
        {
            using (var db = new ProdavnicaIgracaka.Models.ProductContext())
            {
                try
                {
                    int CartItemCount = CartItemUpdates.Count();
                    List<CartItem> myCart = GetCartItems();
                    foreach (var cartItem in myCart)
                    {
                        for (int i = 0; i < CartItemCount; i++)
                        {
                            if (cartItem.Product.ProductID == CartItemUpdates[i].ProductId)
                            {
                                if (CartItemUpdates[i].PurchaseQuantity < 1 || CartItemUpdates[i].RemoveItem == true)
                                {
                                    RemoveItem(cartId, cartItem.ProductId);
                                }
                                else
                                {
                                    UpdateItem(cartId, cartItem.ProductId, CartItemUpdates[i].PurchaseQuantity);
                                }
                            }
                        }
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Update Cart Database - " + exp.Message.ToString(), exp);
                }
            }
        }
        public void RemoveItem(string removeCartID, int removeProductID)
        {
            using (var _db = new ProdavnicaIgracaka.Models.ProductContext())
            {
                try
                {
                    var myItem = (from c in _db.ShoppingCartItems where c.CartId == removeCartID && c.Product.ProductID == removeProductID select c).FirstOrDefault();
                    if (myItem != null)
                    {
                        _db.ShoppingCartItems.Remove(myItem);
                        _db.SaveChanges();
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Remove Cart Item - " + exp.Message.ToString(), exp);
                }
            }
        }
        //Struktura ShoppingCartUpdates koristi se za držanje svih stavki korpe za kupovinu.
        //Metod UpdateShoppingCartDatabase koristi strukturu ShoppingCartUpdates da bi odredio da li je potrebno ažurirati 
        //ili ukloniti neku od stavki.
        public void UpdateItem(string updateCartID, int updateProductID, int quantity)
        {
            using (var _db = new ProdavnicaIgracaka.Models.ProductContext())
            {
                try
                {
                    var myItem = (from c in _db.ShoppingCartItems where c.CartId == updateCartID && c.Product.ProductID == updateProductID select c).FirstOrDefault();
                    if (myItem != null)
                    {
                        myItem.Quantity = quantity;
                        _db.SaveChanges();
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Update Cart Item - " + exp.Message.ToString(), exp);
                }
            }
        }
        public void EmptyCart()
        {
            ShoppingCartId = GetCartId();
            var cartItems = _db.ShoppingCartItems.Where(c => c.CartId == ShoppingCartId);
            foreach (var cartItem in cartItems)
            {
                _db.ShoppingCartItems.Remove(cartItem);
            }
            _db.SaveChanges();
        }

        // Metod GetCount odredjuje koliko je stavki u korpi za kupovinu
        public int GetCount()
        {
            ShoppingCartId = GetCartId();
            int? count = (from cartItems in _db.ShoppingCartItems where cartItems.CartId == ShoppingCartId select (int?)cartItems.Quantity).Sum();
            return count ?? 0;
        }
        public struct ShoppingCartUpdates
        {
            public int ProductId;
            public int PurchaseQuantity;
            public bool RemoveItem;

        }

        //Metod MigrateCart koristi postojeći cartId za pronalaženje korpe za kupovinu korisnika. 
        //Zatim, kod se provlači kroz sve stavke korpe za kupovinu i zamenjuje svojstvo CartId 
        //(kao što je određeno šemom CartItem) sa prijavljenim korisničkim imenom.//
        public void MigrateCart(string cartId, string userName)
        {
            var shoppingCart = _db.ShoppingCartItems.Where(c => c.CartId == cartId);
            foreach (CartItem item in shoppingCart)
            {
                item.CartId = userName;
            }
            HttpContext.Current.Session[CartSessionKey] = userName;
            _db.SaveChanges();
        }

    }
}