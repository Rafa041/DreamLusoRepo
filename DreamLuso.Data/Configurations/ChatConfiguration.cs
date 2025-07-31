using DreamLuso.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DreamLuso.Data.Configurations;

internal class ChatConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Property)
               .WithMany()
               .HasForeignKey(x => x.PropertyId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.User)
               .WithMany()
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.RealEstateAgent)
               .WithMany()
               .HasForeignKey(x => x.RealEstateAgentId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Messages)
               .WithOne(x => x.Chat)
               .HasForeignKey(x => x.ChatId);
    }
}
