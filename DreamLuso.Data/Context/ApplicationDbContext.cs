using DreamLuso.Data.Configurations;
using DreamLuso.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
namespace DreamLuso.Data.Context;

public class ApplicationDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Address> Address { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<Comments> Comments { get; set; }
    public DbSet<Favorites> Favorites{ get; set; }
    public DbSet<FinancialTransactions> FinancialTransactions { get; set; }
    public DbSet<Notifications> Notifications { get; set; }
    public DbSet<Property> Property { get; set; }
    public DbSet<PropertyImages> PropertyImages { get; set; }
    public DbSet<RealStateAgent> RealStateAgent { get; set; }
    public DbSet<User> Users { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
        base.OnConfiguring(optionsBuilder);
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new AccountConfiguration());
        modelBuilder.ApplyConfiguration(new AddressConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new CommentsConfiguration());
        modelBuilder.ApplyConfiguration(new FavoritesConfiguration());
        modelBuilder.ApplyConfiguration(new FinancialTransactionsConfiguration());
        modelBuilder.ApplyConfiguration(new NotificationsConfiguration());
        modelBuilder.ApplyConfiguration(new PropertyConfiguration());
        modelBuilder.ApplyConfiguration(new PropertyImagesConfiguration());
        modelBuilder.ApplyConfiguration(new RealStateAgentConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());

        modelBuilder.HasDefaultSchema("DreamLuso");
    }
}
