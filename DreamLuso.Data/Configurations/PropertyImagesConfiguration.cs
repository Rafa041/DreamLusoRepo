using DreamLuso.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DreamLuso.Data.Configurations;

internal class PropertyImagesConfiguration : IEntityTypeConfiguration<PropertyImages>
{
    public void Configure(EntityTypeBuilder<PropertyImages> builder)
    {
        builder.ToTable("PropertyImages");
    }
}
