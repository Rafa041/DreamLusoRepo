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

    }
}
