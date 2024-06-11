using ApnaDhaba_API.Models.Other;
using System.ComponentModel.DataAnnotations;

namespace ApnaDhaba_API.Models
{
    public class prod_cat_Model
    {
        [Key]
        public int serailID { get; set; }

        public Product? product { get; set; }
        public List<Product>? productsList { get; set; }
        public Categories? categories { get; set; }
        public List<Categories>? categoriesList { get; set; }
        public cat_dropdownList? catSelectList { get; set; }
    }
}