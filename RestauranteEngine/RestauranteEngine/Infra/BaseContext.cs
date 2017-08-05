using Microsoft.EntityFrameworkCore;
using RestauranteEngine.Models;

namespace RestauranteEngine.Infra
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new RestauranteMapping(modelBuilder.Entity<Restaurante>());
            new PratoMapping(modelBuilder.Entity<Prato>());
        }
    }
}
