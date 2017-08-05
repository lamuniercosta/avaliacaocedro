using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestauranteEngine.Models
{
    public class PratoMapping
    {
        public PratoMapping(EntityTypeBuilder<Prato> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.RestauranteId);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Preco).IsRequired();
            builder.HasOne(x => x.restaurante).WithMany(x => x.pratos).HasForeignKey(x => x.RestauranteId);
        }
    }
}
