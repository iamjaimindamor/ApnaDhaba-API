using ApnaDhaba.Models.Other;
using ApnaDhaba_API.Interfaces;
using ApnaDhaba_API.Models;
using ApnaDhaba_API.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApnaDhaba_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<string>> Register(UserModel user)
        {
            var data = await authService.Register(user);
            if (data != null)
            {
                await authService.AssignUser(user.Username);
                return Ok(data);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginDTO login)
        {
            var data = await authService.Authenticate(login);
            if (data != null)
            {
                return Ok(data);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("SeedRole")]
        public async Task<bool> SeedRole()
        {
            return await authService.SeedRole();
        }

        [HttpPost("AssignAdmin")]
        public async Task<bool> AdminRole(string u)
        {
            return await authService.AssignAdmin(u);
        }

        [HttpPost("AssignOwner")]
        public async Task<bool> OwnerRole(string u)
        {
            return await authService.AssignOwner(u);
        }

        [HttpPost("AssignUser")]
        public async Task<bool> UserRole(string u)
        {
            return await authService.AssignUser(u);
        }

        [HttpPost("Confirm")]
        public IActionResult ConfirmUser(UserModel user)
        {
            if (user.Username != null)
            {
                string isConfirmed = authService.ConfirmUser(user.Username);
                if (isConfirmed != null)
                {
                    return Ok(isConfirmed);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("Reset")]
        [Authorize(Roles = "USER")]
        public IActionResult ResetPassword(Reset newPass)
        {
            if (newPass.Password != null && newPass.username != null)
            {
                string isUpdate = authService.ResetPassword(newPass.username, newPass.Password);

                if (isUpdate != null)
                {
                    return Ok(isUpdate);
                }
                else
                {
                    return BadRequest(string.Empty);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("GetDetails")]
        public IActionResult GetDetails(UserModel user)
        {
            return Ok(authService.Fetch(user.Username));
        }

        [HttpPut("UpdateUser")]
        public IActionResult updateuser(UserModel user)
        {
            if (user != null)
            {
                return Ok(authService.UpdateUser(user));
            }
            else
            {
                return BadRequest();
            }
        }
    }
}