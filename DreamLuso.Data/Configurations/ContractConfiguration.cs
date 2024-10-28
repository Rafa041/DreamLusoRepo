using DreamLuso.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DreamLuso.Data.Configurations;

internal class ContractConfiguration : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.ToTable("Contracts");


        builder.HasKey(c => c.Id);

        builder.HasOne(c => c.Property)
               .WithMany()
               .HasForeignKey(c => c.PropertyId)
               .OnDelete(DeleteBehavior.Restrict);


        builder.HasOne(c => c.Client)
               .WithMany()
               .HasForeignKey(c => c.ClientId)
               .OnDelete(DeleteBehavior.Restrict);


        builder.HasOne(c => c.RealStateAgent)
               .WithMany()
               .HasForeignKey(c => c.RealStateAgentId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(c => c.Value).HasColumnType("decimal(18,2)");
        builder.Property(c => c.AdditionalFees).HasColumnType("decimal(18,2)");
        builder.Property(c => c.SecurityDeposit).HasColumnType("decimal(18,2)");
        builder.Property(c => c.DocumentPath).HasMaxLength(255);

        builder.HasOne(c => c.Property)
               .WithMany()
               .HasForeignKey(c => c.PropertyId)
               .OnDelete(DeleteBehavior.Restrict);


        builder.HasMany(c => c.Invoices)
               .WithOne(i => i.Contract)
               .HasForeignKey(i => i.ContractId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Transactions)
               .WithOne(t => t.Contract )
               .HasForeignKey(t => t.ContractId)
               .OnDelete(DeleteBehavior.Cascade);

    }
}
