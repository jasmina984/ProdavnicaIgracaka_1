using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ProdavnicaIgracaka.Models
{
    public class ProductDatabaseInitializer :
    DropCreateDatabaseAlways<ProductContext>
    {
        protected override void Seed(ProductContext context)
        {
            GetCategories().ForEach(c => context.Categories.Add(c));
            GetProducts().ForEach(p => context.Products.Add(p));
        }

        private static List<Category> GetCategories()
        {
            var categories = new List<Category>
            {
                new Category
                { CategoryID = 1,
                  CategoryName = "Automobili"
                },
                new Category
                {
                    CategoryID = 2,
                    CategoryName = "Avioni"
                },
                new Category
                {
                    CategoryID = 3,
                    CategoryName = "Kamioni"
                },
                new Category
                {
                    CategoryID = 4,
                    CategoryName = "Brodovi"
                },
                new Category
                {
                    CategoryID = 5,
                    CategoryName = "Rakete"
                },

            };
            return categories;
        }

        private static List<Product> GetProducts()
        {
            var products = new List<Product> {
                new Product
                {
                    ProductID = 1,
                    ProductName = "Kabriolet Bentley Continental",
                    Description = "Ovaj kabriolet je brz! Burago Bentley Continental Supers je u razmeri 1/18." +
                                  "Motor pokrece baterija bazirana na neutrinu (nije ukljucena).Napunite ga i pustite! ",
                    ImagePath="bentley.jpg",
                    UnitPrice = 4000,
                    CategoryID = 1
               },
                new Product
                {
                    ProductID = 2,
                    ProductName = "Oldtajmer",
                    Description = "Ovaj autic radi na navijanje. Vrlo je zanimljiv deci od 1-3 god.",
                    ImagePath="autic-na-navijanje.jpg",
                    UnitPrice = 800,
                    CategoryID = 1
               },
                new Product
                {
                    ProductID = 3,
                    ProductName = "Burago Bugatti EB 110",
                    Description = "Ova maketa je u razmeri 1/24." +
                                  "Burago je cuvena italijanska kompanija nudi sirok spektar autenticnih maketa automobila," +
                                  "razlicitih razmera i marki, koje su narocito popularne medju kolekcionarima.",
                    ImagePath="bugati.jpg",
                    UnitPrice = 2000,
                    CategoryID = 1
                },
                new Product
                {
                    ProductID = 4,
                    ProductName = "RC Francuski policijski auto",
                    Description = "Ovaj auto je na daljinski u razmeri 1/28.Moze se kretati u svim smerovima." +
                                  "Vrata se otvaraju a svetla se pale.",
                    ImagePath="policijski-auto.jpg",
                    UnitPrice = 1500,
                    CategoryID = 1
                },
                new Product
                {
                    ProductID = 5,
                    ProductName = "Rastar Porsche 918 Spider Weissach R70710",
                    Description = "Rastar automobil na daljinsko upravljanje, R/C Porsche 918 Spider Weissach je u razmeri 1/14." +
                                  "Ovaj auto je izuzetno brz i deca vole da se trkaju.",
                    ImagePath="porsche.jpg",
                    UnitPrice = 3000,
                    CategoryID = 1
                },
                new Product
                {
                    ProductID = 6,
                    ProductName = "Avion od drveta",
                    Description = "Ova igracka aviona je vrlo zanimljiva deci jer je od drveta. Sadrzi realisticne boje i detalje.",
                    ImagePath="drveni-avion.jpg",
                    UnitPrice = 800,
                    CategoryID = 2
                },
                new Product
                {
                    ProductID = 7,
                    ProductName = "Helikopter 3.5CH Game Star",
                    Description = "Ova igracka helikoptera sa daljinskim je od kvalitetne plastike i metala." +
                                  "Vreme leta je 6-7 min i radi na udaljenosti od 30m od daljinskog upravljaca",
                    ImagePath="helikopter.jpg",
                    UnitPrice = 4500,
                    CategoryID = 2
                },
                new Product
                {
                    ProductID = 8,
                    ProductName = "Jedrilica",
                    Description = "Model električnog aviona Spy Hawk Hubsan RtF RC Jedrilica 1000 mm je izuzetnog dizajna." +
                                  "Radi sa daljinskim i duzina je 76cm, autentican je sa originalom i ima svetla koja se pale.",
                    ImagePath="jedrilica.jpeg",
                    UnitPrice = 8000,
                    CategoryID = 2
                },
                new Product
                {
                    ProductID = 9,
                    ProductName = "Avion Maketa Hawk T.1 Red Arrows",
                    Description = "Ova igracka nije namenjena deci mladjoj od 10 godina. Raspon krila je 13cm a duzina 16cm." +
                                  "Ovaj avion je autentican sa originalom i u razmeri 1/72",
                    ImagePath="hawk.jpg",
                    UnitPrice = 1500,
                    CategoryID = 2
                },
                new Product
                {
                    ProductID = 10,
                    ProductName = "Kamion dizalica Lena",
                    Description = "Lena kamion dizalica sa 3 faze podizanja do 105cm. Kapacitet do 100kg, 6mm pocinkovane osovine. " +
                                  "270 ° rotirajuci kran, prosiriv sa 4 kolone i 2 klina za stabilizaciju.",
                    ImagePath="Kamion-dizalica.jpg",
                    UnitPrice = 4000,
                    CategoryID = 3
                },
                new Product
                {
                    ProductID = 11,
                    ProductName = "Disney Miki Vatrogasni Kamion",
                    Description = "Jedan od omiljenih Diznijevih junaka krece u avanturu gasenja pozara svojim Mickey vatrogasnim kamionom." +
                    "Zajedno sa svojom druzinom Mickey Mouse uz pomoc creva gasi pozar dok svetlo i sirena na krovu kamiona signaliziraju vatrogasnu uzbunu.",
                    ImagePath="miki-vatrogasni-kamion.jpg",
                    UnitPrice = 3500,
                    CategoryID = 3
                },
                new Product
                {
                    ProductID = 12,
                    ProductName = "Kamion Lena djubretarac",
                    Description = "Kamion za smeCe opremljen dodacima za vecu zabavu vasim najmladjih." +
                    "Sadrzi 2 osovine vozila sa kapacitetom opterecenja do 100kg(50kg u pokretu), 6mm pocinkovane osovine." +
                    "Veliki i mali kontejner za smeće mogu se nagnuti pomoću mehanizma za podizanje.Veliki kontejneri mogu biti fiksirani." +
                    "Pored toga, klapna se može otvoriti na kraju vozila.",
                    ImagePath="Kamion-za-smece.jpg",
                    UnitPrice = 4000,
                    CategoryID = 3
                },
                new Product
                {
                    ProductID = 13,
                    ProductName = "Redbox Piratski brod",
                    Description = "Zamislite da plovite morima u potrazi za osvajnajem novog kraljevstva." +
                    "Set sadrzi 40 delova kao napr kapije, pokretni most, pa čak i tesku artiljeriju za gadjanje kao što je top i katapult. ",
                    ImagePath="piratski-brod.jpg",
                    UnitPrice = 4000,
                    CategoryID = 4
                },
                new Product
                {
                    ProductID = 14,
                    ProductName = "Siku Brod Mein Schiff",
                    Description = "Brod Mein Schiff 1, detaljno uradjen model broda iz Siku Super serije, izradjen u razmeri 1:1400." +
                    "Velika paznja je posvecena kvalitetu i zavrsnoj obradi, pa model predstavlja lep primerak i za kolekcionare.",
                    ImagePath="siku-brod.jpg",
                    UnitPrice = 1500,
                    CategoryID = 4
                },
                new Product
                {
                    ProductID = 15,
                    ProductName = "Jahta brod na daljinsko upravljanje ATLANTIC YACHT",
                    Description = "Veoma startan model sa odlicnim hidrodinamickim osobinama koje olaksavaju upravljivost." +
                    "Daljinski poseduje dva kanala: napred i levo, desno. Uz model se isporucuje postolje i dve rezervne elise." +
                    "Brzina broda se krece i do 15 km/h. Poseduje dva motora.",
                    ImagePath="jahta-brod.jpg",
                    UnitPrice = 8000,
                    CategoryID = 4
                },
                new Product
                {
                    ProductID = 16,
                    ProductName = "Wow Svemirska Raketa Roni",
                    Description = "Roni je super-sonicna svemirska raketa spremna za avanture!" +
                    "Povucite kanap na Ronijevom robotu Zigiju i odbrojavanje pocinje: 5...4...3...2...1... " +
                    "Roni pocinje da vibrira dok se priprema da poleti i svaki broj se pojavljuje na njegovom specijalnom ekranu....iiiii POLECEMO!!!" +
                    "Roni ima i tajno dugme za otvaranje kabine u kojoj sedi njegov pilot, major Tom.",
                    ImagePath="svemirska-raketa.jpg",
                    UnitPrice = 2800,
                    CategoryID = 5
                }

            };
            return products;
        }

       
    }

}