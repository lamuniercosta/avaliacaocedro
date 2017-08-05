using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RestauranteEngine.Models
{
    public class RestauranteMapping
    {
        public RestauranteMapping(EntityTypeBuilder<Restaurante> builder) {

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(100);
            builder.HasMany(x => x.pratos).WithOne();
        }
    }
}
