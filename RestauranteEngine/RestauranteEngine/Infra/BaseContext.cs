using Microsoft.EntityFrameworkCore;
using RestauranteEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestauranteEngine.Infra
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new RestauranteMapping(modelBuilder.Entity<Restaurante>());
        }
    }
}
