using Microsoft.EntityFrameworkCore;

namespace aspPro2.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Students> student { get; set; }
        public DbSet<Games> gameInfo { get; set; }
        public DbSet<Users> users { get; set; }
    }
}
