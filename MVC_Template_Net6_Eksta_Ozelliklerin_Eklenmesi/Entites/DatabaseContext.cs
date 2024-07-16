using Microsoft.EntityFrameworkCore;

namespace MVC_Template_Net6_Eksta_Ozelliklerin_Eklenmesi.Entites
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }

}
