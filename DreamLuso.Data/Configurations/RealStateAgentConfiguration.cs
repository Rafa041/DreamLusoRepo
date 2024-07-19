using DreamLuso.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DreamLuso.Data.Configurations;

internal class RealStateAgentConfiguration : IEntityTypeConfiguration<RealStateAgent>
{
    public void Configure(EntityTypeBuilder<RealStateAgent> builder)
    {
        builder.ToTable("RealStateAgent");
    }
}
