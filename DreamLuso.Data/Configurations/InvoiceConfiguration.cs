using DreamLuso.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DreamLuso.Data.Configurations;

internal class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("Invoice");
        builder.HasKey(i => i.Id);

        builder.Property(i => i.InvoiceNumber).HasMaxLength(50);
        builder.Property(i => i.Amount).HasColumnType("decimal(18,2)");

        builder.HasOne(i => i.Contract)
               .WithMany(c => c.Invoices)
               .HasForeignKey(i => i.ContractId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(i => i.Transaction)
               .WithOne(t => t.Invoice)
               .HasForeignKey<Invoice>(i => i.TransactionId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}