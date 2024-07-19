using DreamLuso.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DreamLuso.Data.Configurations;

internal class FinancialTransactionsConfiguration : IEntityTypeConfiguration<FinancialTransactions>
{
    public void Configure(EntityTypeBuilder<FinancialTransactions> builder)
    {
        builder.ToTable("FinancialTransactions");
    }
}
