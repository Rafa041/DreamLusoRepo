using DreamLuso.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace DreamLuso.Data.Configurations;

internal class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        builder.ToTable("Property");
        
        builder.HasOne(p => p.Address)
        .WithOne()
        .HasForeignKey<Property>(p => p.AddressId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Images)
        .WithOne(p => p.Property)
        .HasForeignKey(p => p.PropertyId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.Property(p => p.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.HasOne(p => p.RealEstateAgent)
              .WithMany(r => r.Properties)
              .HasForeignKey(p => p.RealEstateAgentId)
              .OnDelete(DeleteBehavior.NoAction);
    }
}
