using Microsoft.EntityFrameworkCore;

namespace MVC_CRUD.Data
{
    public class MVCDbContext : DbContext
    {
        public MVCDbContext(DbContextOptions options): base(options) { }

        public DbSet<Models.Domain.User> Users { get; set; }
        public DbSet<Models.Domain.Product> Products { get; set; }

    }
}
