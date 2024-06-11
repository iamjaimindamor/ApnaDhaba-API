using Microsoft.EntityFrameworkCore;

namespace ApnaDhaba_API.Models.Database_Context
{
    public class apnaDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<UserModel> userModels { get; set; }
        public DbSet<roleModel> roleModels { get; set; }
        public DbSet<AssignedRole> assignedRoles { get; set; }
        public DbSet<Product> productTable { get; set; }
        public DbSet<Categories> categoryTable { get; set; }
    }
}

/*

 */