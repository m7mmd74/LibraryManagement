using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Models
{
    public class LibraryDbContext:IdentityDbContext<ApplicationUser>
    {public LibraryDbContext(DbContextOptions<LibraryDbContext> options):base(options)
        {
                
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        }
}
