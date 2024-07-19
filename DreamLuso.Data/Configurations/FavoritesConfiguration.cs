using DreamLuso.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DreamLuso.Data.Configurations;

internal class FavoritesConfiguration : IEntityTypeConfiguration<Favorites>
{
    public void Configure(EntityTypeBuilder<Favorites> builder)
    {
        builder.ToTable("Favorites");
    }
}
