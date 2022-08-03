using Microsoft.EntityFrameworkCore;
using SystemYNP.Models;

namespace SystemYNP.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<YNP> YNP { get; set; }
    }
}
