using ApnaDhaba_API.Interfaces;
using ApnaDhaba_API.Models;
using ApnaDhaba_API.Models.Database_Context;
using ApnaDhaba_API.Models.DTOs;
using ApnaDhaba_API.Models.Other;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ApnaDhaba_API.Services
{
    public class AuthService : IAuthService
    {
        private readonly apnaDbContext authContext;
        private readonly IPasswordServices passwordServices;
        private readonly IConfiguration configuration;

        public AuthService(apnaDbContext authContext, IPasswordServices passwordServices, IConfiguration configuration)
        {
            this.authContext = authContext;
            this.passwordServices = passwordServices;
            this.configuration = configuration;
        }

        async Task<string> IAuthService.Authenticate(LoginDTO loginRequest)
        {
            var isUser = await authContext.userModels.AnyAsync(x => x.Username == loginRequest.Username);

            if (isUser == true)
            {
                var user = await authContext.userModels.FirstOrDefaultAsync(x => x.Username == loginRequest.Username);
                if (user != null)
                {
                    if (user.Password != null && loginRequest.Password != null)
                    {
                        //validate the user
                        bool valid = passwordServices.VerifyPassword(loginRequest.Password, user.Password);

                        if (valid == true)
                        {
                            //JWT Token

                            //encoded key as SecurityKey in this case a Symmetric Key
                            var encodedkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

                            //set the jwt claims
                            var claims = new List<Claim>()
                            {
                                new Claim(ClaimTypes.Name,"ApnaToken"),

                                new Claim(JwtRegisteredClaimNames.Sub,configuration["Jwt:Subject"])
                            };

                            var rolecheck = await authContext.assignedRoles.Where(x => x.userId == user.userId).ToListAsync();

                            if (rolecheck != null)
                            {
                                foreach (var role in rolecheck)
                                {
                                    var rolename = await authContext.roleModels.FirstOrDefaultAsync(x => x.roleId == role.roleId);
                                    claims.Add(new Claim(ClaimTypes.Role, rolename.roleName));
                                }
                            }

                            //mention the signing credentials aka encryption to be used
                            var signIn = new SigningCredentials(encodedkey, SecurityAlgorithms.HmacSha512);

                            //create the token parameters
                            var token = new JwtSecurityToken(
                                configuration["Jwt:Issuer"],
                                configuration["Jwt:Audience"],
                                claims,
                                expires: DateTime.Now.AddMinutes(30),
                                signingCredentials: signIn);
                            //finally wrtie the token
                            var access_token = new JwtSecurityTokenHandler().WriteToken(token);
                            return access_token;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        async Task<ActionResult<string>> IAuthService.Register(UserModel userRegisteration)
        {
            var doesExist = await authContext.userModels.AnyAsync(x => x.Username == userRegisteration.Username);

            if (doesExist == false)
            {
                if (userRegisteration.Password != null)
                {
                    string hashed = passwordServices.HashPassword(userRegisteration.Password);

                    UserModel user = new UserModel()
                    {
                        Firstname = userRegisteration.Firstname,
                        Lastname = userRegisteration.Lastname,
                        Username = userRegisteration.Username,
                        Password = hashed,
                        ImageURL = userRegisteration.ImageURL,
                        CreateDate = userRegisteration.CreateDate
                    };

                    await authContext.userModels.AddAsync(user);
                    await authContext.SaveChangesAsync();

                    return "Registered Successfully";
                }
                else return null;
            }
            else
            {
                return null;
            }
        }

        async Task<bool> IAuthService.SeedRole()
        {
            roleModel r1 = new roleModel()
            {
                roleName = StaticUserRole.OWNER
            };
            roleModel r2 = new roleModel()
            {
                roleName = StaticUserRole.ADMIN
            };
            roleModel r3 = new roleModel()
            {
                roleName = StaticUserRole.USER
            };
            roleModel r4 = new roleModel()
            {
                roleName = StaticUserRole.GUEST
            };
            await authContext.AddRangeAsync(r1, r2, r3, r4);
            await authContext.SaveChangesAsync();

            return true;
        }

        async Task<bool> IAuthService.AddRole(roleModel role)
        {
            if (role != null)
            {
                await authContext.roleModels.AddAsync(role);
                await authContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        async Task<bool> IAuthService.AssignAdmin(string inputUsername)
        {
            var userData = await authContext.userModels.FirstOrDefaultAsync(x => x.Username == inputUsername);
            //var roleData = await authContext.roleModels.ToListAsync();

            //int assignID = 0;

            //foreach (var item in roleData)
            //{
            //    if (item.roleName == "ADMIN")
            //    {
            //        assignID = item.roleId;
            //    }
            //}

            //var getRole = await authContext.roleModels.FindAsync(assignID);

            AssignedRole assigned = new()
            {
                roleId = 2,
                userId = userData.userId
            };

            await authContext.AddAsync(assigned);
            await authContext.SaveChangesAsync();
            return true;
        }

        async Task<bool> IAuthService.AssignOwner(string inputUsername)
        {
            var userData = await authContext.userModels.FirstOrDefaultAsync(x => x.Username == inputUsername);
            var roleData = await authContext.roleModels.ToListAsync();

            int assignID = 0;

            foreach (var item in roleData)
            {
                if (item.roleName == "OWNER")
                {
                    assignID = item.roleId;
                }
            }

            var getRole = await authContext.roleModels.FindAsync(assignID);

            AssignedRole assigned = new()
            {
                roleId = getRole.roleId,
                userId = userData.userId
            };

            await authContext.AddAsync(assigned);
            await authContext.SaveChangesAsync();
            return true;
        }

        async Task<bool> IAuthService.AssignUser(string inputUsername)
        {
            var userData = await authContext.userModels.FirstOrDefaultAsync(x => x.Username == inputUsername);
            var roleData = await authContext.roleModels.ToListAsync();

            int assignID = 0;

            foreach (var item in roleData)
            {
                if (item.roleName == "USER")
                {
                    assignID = item.roleId;
                }
            }

            var getRole = await authContext.roleModels.FindAsync(assignID);

            AssignedRole assigned = new()
            {
                roleId = getRole.roleId,
                userId = userData.userId
            };

            await authContext.AddAsync(assigned);
            await authContext.SaveChangesAsync();
            return true;
        }

        string IAuthService.ConfirmUser(string username)
        {
            var data = authContext.userModels.FirstOrDefault(x => x.Username == username);

            if (data != null)
            {
                return "User Found";
            }
            return null;
        }

        string IAuthService.ResetPassword(string username, string NewPassword)
        {
            var restUserPass = authContext.userModels.FirstOrDefault(x => x.Username == username);

            if (NewPassword != null && restUserPass != null)
            {
                //encrypt the password
                var hmac = new HMACSHA512();

                //salting
                hmac.Key = RandomNumberGenerator.GetBytes(64);

                //assign the password salt

                byte[] passwordSalt = hmac.Key;
                byte[] NewPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(NewPassword));

                //store password in encoded foramt along with password salt also in encoded format
                var newHash = string.Join('.', Convert.ToBase64String(passwordSalt), Convert.ToBase64String(NewPasswordHash));

                restUserPass.Password = newHash;
                restUserPass.LastModifiedDate = DateTime.Now;
                authContext.userModels.Update(restUserPass);
                authContext.SaveChanges();

                return "User Password Updated";
            }
            return null;
        }

        UserModel IAuthService.Fetch(string username)
        {
            var data = authContext.userModels.FirstOrDefault(x => x.Username == username);

            if (data != null)
            {
                data.Password = null;
                return data;
            }
            return null;
        }

        bool IAuthService.UpdateUser(UserModel user)
        {
            if (user != null)
            {
                var data = authContext.userModels.FirstOrDefault(x => x.Username == user.Username);

                if (data != null)
                {
                    data.Firstname = user.Firstname;
                    data.Lastname = user.Lastname;
                    data.Address = user.Address;
                    data.Email = user.Email;
                    data.LastModifiedDate = user.LastModifiedDate;

                    authContext.userModels.Update(data);
                    authContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else { return false; }
        }
    }
}