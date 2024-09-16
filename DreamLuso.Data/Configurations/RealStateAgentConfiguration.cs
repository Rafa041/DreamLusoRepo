using DreamLuso.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DreamLuso.Data.Configurations;

internal class RealStateAgentConfiguration : IEntityTypeConfiguration<RealStateAgent>
{
    public void Configure(EntityTypeBuilder<RealStateAgent> builder)
    {
        builder.ToTable("RealStateAgent");
        builder.Property(r => r.UserId)
               .IsRequired();

        builder.Property(r => r.OfficeEmail)
               .IsRequired()
               .HasMaxLength(255);


        builder.HasMany(r => r.Properties)
               .WithOne(p => p.RealStateAgent)
               .HasForeignKey(p => p.RealStateAgentId);
    }
}
