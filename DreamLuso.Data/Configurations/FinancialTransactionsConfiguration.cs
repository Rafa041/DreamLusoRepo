using DreamLuso.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DreamLuso.Data.Configurations;

internal class FinancialTransactionsConfiguration : IEntityTypeConfiguration<FinancialTransactions>
{
    public void Configure(EntityTypeBuilder<FinancialTransactions> builder)
    {
        builder.ToTable("FinancialTransactions");
        builder.HasKey(t => t.Id);

        builder.HasOne(t => t.Contract)
               .WithMany(c => c.Transactions)
               .HasForeignKey(t => t.ContractId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Invoice)
               .WithOne(i => i.Transaction)
               .HasForeignKey<FinancialTransactions>(t => t.InvoiceId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
