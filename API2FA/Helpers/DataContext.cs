namespace API2FA.Helpers
{
    using API2FA.Models;
    using Microsoft.EntityFrameworkCore;

    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<User> Users { get; set; }
    }
}
