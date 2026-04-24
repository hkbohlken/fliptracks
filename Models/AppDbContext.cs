using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
namespace FlipTracks.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
    }
}
