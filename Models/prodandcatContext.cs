using Microsoft.EntityFrameworkCore;

namespace prodandcat.Models
{
    public class prodandcatContext : DbContext
    {
        public prodandcatContext(DbContextOptions<prodandcatContext> options) : base(options) {}
        public  DbSet<Product> Products { get; set;}
        public  DbSet<Category> Categories { get; set;}
        public  DbSet<Associations> Associations { get; set;}
    }
}