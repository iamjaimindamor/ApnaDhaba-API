using ApnaDhaba_API.Interfaces;
using ApnaDhaba_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApnaDhaba_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminServices adminServices;

        public AdminController(IAdminServices adminServices)
        {
            this.adminServices = adminServices;
        }

        [HttpPost("AddCategory")]
        public async Task<ActionResult<string>> AddCategory(Categories categories)
        {
            var data = await adminServices.AddCategory(categories);
            return Ok(data);
        }

        [HttpPost("AddProduct")]
        public async Task<ActionResult<string>> AddProduct(Product product)
        {
            var data = await adminServices.AddProduct(product);
            return Ok(data);
        }
    }
}