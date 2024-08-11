using ApiBlog.Modelos;
using Microsoft.EntityFrameworkCore;

namespace ApiBlog.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        //Agragar los modelos
        public DbSet<Post> Post {  get; set; }
    }
}
