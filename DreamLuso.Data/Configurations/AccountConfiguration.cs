using DreamLuso.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DreamLuso.Data.Configurations;

internal class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts");

        builder.Property(x => x.PasswordHash)
            .HasColumnName("PasswordHash");

        builder.Property(x => x.PasswordSalt)
            .HasColumnName("PasswordSalt");
    }
}
