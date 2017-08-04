using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestauranteEngine.Models
{
    public class RestauranteMapping
    {
        public RestauranteMapping(EntityTypeBuilder<Restaurante> builder) {

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(100);

        }
    }
}
