using ApnaDhaba_API.Interfaces;
using ApnaDhaba_API.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ApnaDhaba_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHomeServices homeServices;

        public HomeController(IHomeServices homeServices)
        {
            this.homeServices = homeServices;
        }

        [HttpPost("GetAddress")]
        public ActionResult<string> getAddress(fetchAddClass user)
        {
            try
            {
                string user2 = user.username;
                return homeServices.FetchAddress(user2);
            }
            catch (Exception ex)
            {
                return "Address Fetch Failed " + ex;
            }
        }
    }
}