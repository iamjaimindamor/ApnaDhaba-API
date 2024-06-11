using ApnaDhaba_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApnaDhaba_API.Interfaces
{
    public interface IAdminServices
    {
        public Task<ActionResult<string>> AddCategory(Categories categories);

        public Task<ActionResult<string>> AddProduct(Product product);

        public ActionResult<prod_cat_Model> GetProduct();
    }
}