using ApnaDhaba_API.Interfaces;
using ApnaDhaba_API.Models;
using ApnaDhaba_API.Models.Database_Context;
using ApnaDhaba_API.Models.Other;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ApnaDhaba_API.Services
{
    public class AdminServices : IAdminServices
    {
        private readonly apnaDbContext dbContext;

        public AdminServices(apnaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        async Task<ActionResult<string>> IAdminServices.AddCategory(Categories categories)
        {
            await dbContext.AddAsync(categories);
            await dbContext.SaveChangesAsync();
            //return categories;
            return "Category Added";
        }

        async Task<ActionResult<string>> IAdminServices.AddProduct(Product product)
        {
            var doesExist = await dbContext.productTable.FindAsync(product.ProductName);
            if (doesExist == null)
            {
                await dbContext.productTable.AddAsync(product);
                await dbContext.SaveChangesAsync();
                return ("Product Added");
            }
            else
            {
                return ("Product Already Exists");
            }
        }

        ActionResult<prod_cat_Model> IAdminServices.GetProduct()
        {
            prod_cat_Model prod_Cat = new prod_cat_Model();
            prod_Cat.catSelectList = CategoryList();

            return prod_Cat;
        }

        private cat_dropdownList CategoryList()
        {
            //create instance of the model that contains the List Of SelectListType
            cat_dropdownList dList = new cat_dropdownList();

            //access that field and create new object
            dList.categoryDropDown = new List<SelectListItem>();

            //get data from catrgories table
            var categoryData = dbContext.categoryTable.ToList();

            //populate the dropdown list
            if (categoryData != null)
            {
                dList.categoryDropDown.Add(new SelectListItem
                {
                    Value = "",
                    Text = "Select Category"
                });

                foreach (var item in categoryData)
                {
                    dList.categoryDropDown.Add(new SelectListItem
                    {
                        Value = Convert.ToString(item.CategoryId),
                        Text = item.CategoryName
                    });
                }
            }

            return dList;
        }
    }
}