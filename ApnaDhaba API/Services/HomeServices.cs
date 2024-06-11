using ApnaDhaba_API.Interfaces;
using ApnaDhaba_API.Models.Database_Context;

namespace ApnaDhaba_API.Services
{
    public class HomeServices : IHomeServices
    {
        private readonly apnaDbContext dbContext;

        public HomeServices(apnaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        string IHomeServices.FetchAddress(string user)
        {
            try
            {
                if (user != null)
                {
                    var add_user = dbContext.userModels.FirstOrDefault(x => x.Username == user);
                    if (add_user != null)
                    {
                        if (add_user.Address != null)
                        {
                            return add_user.Address;
                        }
                        else
                        {
                            throw new Exception("Address Not Provided");
                        }
                    }
                    else
                    {
                        throw new Exception("User Does Not Exists");
                    }
                }
                else
                {
                    throw new Exception("User Not Provided");
                }
            }
            catch
            {
                return "Error";
            }
        }
    }
}