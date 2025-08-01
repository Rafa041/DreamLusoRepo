﻿using DreamLuso.Data.Configurations;
using DreamLuso.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace DreamLuso.Data.Context;

public class ApplicationDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<Favorites> Favorites{ get; set; }
    public DbSet<FinancialTransactions> FinancialTransactions { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Property> Properties { get; set; }
    public DbSet<PropertyImages> PropertyImages { get; set; }
    public DbSet<PropertyVisit> PropertyVisits { get; set; }
    public DbSet<RealEstateAgent> RealEstateAgent { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }

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
        modelBuilder.ApplyConfiguration(new ClientConfiguration());
        modelBuilder.ApplyConfiguration(new CommentsConfiguration());
        modelBuilder.ApplyConfiguration(new ContractConfiguration());
        modelBuilder.ApplyConfiguration(new FavoritesConfiguration());
        modelBuilder.ApplyConfiguration(new FinancialTransactionsConfiguration());
        modelBuilder.ApplyConfiguration(new InvoiceConfiguration());
        modelBuilder.ApplyConfiguration(new NotificationsConfiguration());
        modelBuilder.ApplyConfiguration(new PropertyConfiguration());
        modelBuilder.ApplyConfiguration(new PropertyImagesConfiguration());
        modelBuilder.ApplyConfiguration(new PropertyImagesConfiguration());
        modelBuilder.ApplyConfiguration(new RealEstateAgentConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());

        modelBuilder.HasDefaultSchema("DreamLuso");
    }
}
