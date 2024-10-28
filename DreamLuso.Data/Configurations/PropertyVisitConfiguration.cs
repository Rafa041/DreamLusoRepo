using DreamLuso.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DreamLuso.Data.Configurations;

internal class PropertyVisitConfiguration : IEntityTypeConfiguration<PropertyVisit>
{
    public void Configure(EntityTypeBuilder<PropertyVisit> builder)
    {
        builder.ToTable("PropertyVisits");
        builder.HasKey(r => r.Id);

        builder.HasOne(pv => pv.Property)
               .WithMany()
               .HasForeignKey(pv => pv.PropertyId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(pv => pv.Client)
               .WithMany()
               .HasForeignKey(pv => pv.ClientId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(pv => pv.RealStateAgent)
               .WithMany()
               .HasForeignKey(pv => pv.RealStateAgentId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
