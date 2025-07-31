using DreamLuso.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DreamLuso.Data.Configurations;

internal class RealEstateAgentConfiguration : IEntityTypeConfiguration<RealEstateAgent>
{
    public void Configure(EntityTypeBuilder<RealEstateAgent> builder)
    {
        builder.ToTable("RealEstateAgent");
        builder.Property(r => r.UserId)
               .IsRequired();

        builder.Property(r => r.OfficeEmail)
               .IsRequired()
               .HasMaxLength(255);


        builder.HasMany(r => r.Properties)
               .WithOne(p => p.RealEstateAgent)
               .HasForeignKey(p => p.RealEstateAgentId);
    }
}
