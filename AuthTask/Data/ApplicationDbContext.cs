using AuthTask.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthTask.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=AuthTask;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        public DbSet<User> users{  get; set; }
    }
}
