using Lost.Models;
using Microsoft.EntityFrameworkCore;

namespace Lost.Data
{
    public class LostContext : DbContext
    {
        public LostContext(DbContextOptions<LostContext> options) : base(options)
        {
        }

        public DbSet<Zaginiony> Zaginieni { get; set; }
        public DbSet<Uzytkownik> Uzytkownicy { get; set; }
    }
}
