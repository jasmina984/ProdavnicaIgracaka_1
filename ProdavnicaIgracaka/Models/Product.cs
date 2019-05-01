using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProdavnicaIgracaka.Models
{
    public class Product
    {
        [ScaffoldColumn(false)]
        public int ProductID { get; set; }

        [Required, StringLength(100), Display(Name = "Ime")]
        public string ProductName { get; set; }
        [Required, StringLength(10000), Display(Name = "Opis proizvoda"), 
        DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string ImagePath { get; set; }

        [Display(Name = "Cena")]
        public double? UnitPrice { get; set; }

        public int? CategoryID { get; set; }
        public virtual Category Category { get; set; }

       
    }
}