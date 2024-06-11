using ApnaDhaba_API.Models;
using ApnaDhaba_API.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ApnaDhaba_API.Interfaces
{
    public interface IAuthService
    {
        public string ConfirmUser(string username);

        public string ResetPassword(string username, string password);

        public Task<ActionResult<string>> Register(UserModel userRegisteration);

        public UserModel Fetch(string username);

        public Task<string> Authenticate(LoginDTO loginRequest);

        public Task<bool> SeedRole();

        public Task<bool> AddRole(roleModel role);

        public Task<bool> AssignOwner(string Username);

        public Task<bool> AssignAdmin(string Username);

        public Task<bool> AssignUser(string Username);

        public bool UpdateUser(UserModel user);
    }
}