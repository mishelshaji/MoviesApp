using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MoviesApp.Data
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Movie> Movies { get; set; }
    }
}
