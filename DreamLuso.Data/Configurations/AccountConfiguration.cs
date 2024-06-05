using DreamLuso.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DreamLuso.Data.Configurations;

internal class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts");

        builder.Property(x => x.Password)
            .HasColumnName("PasswordHash");
    }
}
internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.OwnsOne<Name>(x => x.Name, n =>
        {
            n.Property(x => x.FirstName).HasColumnName("FirstName");
            n.Property(x => x.LastName).HasColumnName("LastName");
        });
    }
}
