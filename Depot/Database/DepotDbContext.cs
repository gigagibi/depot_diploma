using Depot.Extensions;
using Depot.Models.Departments;
using Depot.Models.Entities;
using Depot.Models.Transactions;
using Depot.Models.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Depot.Database;

public class DepotDbContext : IdentityDbContext<User, Role, int>
{
    public DbSet<Department> Departments { get; set; }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<Entity> Entities { get; set; }

    public DbSet<EntityManufacture> EntityManufactures { get; set; }

    public DbSet<EntityModel> EntityModels { get; set; }

    public DbSet<EntityType> EntityTypes { get; set; }

    public DbSet<Transaction> Transactions { get; set; }
        
    public DbSet<TransactionType> TransactionTypes { get; set; }

    public DepotDbContext(DbContextOptions<DepotDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // if is development environment, use the seed data
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
        {
            builder.Seed();
        }
        builder.SeedRolesAndUsers();
        builder.Entity<Employee>()
            .HasOne(a => a.User)
            .WithOne(b => b.Employee)
            .HasForeignKey<User>(b => b.EmployeeId);

        builder.Entity<User>()
            .HasOne(a => a.Employee)
            .WithOne(b => b.User)
            .HasForeignKey<Employee>(b => b.UserId);
        
        builder.Entity<Employee>().Navigation(e => e.User).AutoInclude();
        
        builder.Entity<User>().Navigation(e => e.Employee).AutoInclude();
        
        builder.Entity<Entity>().Navigation(e => e.Employee).AutoInclude();
        builder.Entity<Entity>().Navigation(e => e.EntityModel).AutoInclude().AutoInclude();
        builder.Entity<Entity>().Navigation(e => e.Department).AutoInclude();
        builder.Entity<Entity>()
            .HasIndex(e => e.InvNumber)
            .IsUnique();

        builder.Entity<EntityModel>().Navigation(e => e.EntityType).AutoInclude();
        builder.Entity<EntityModel>().Navigation(e => e.EntityManufacture).AutoInclude();
        
        builder.Entity<Transaction>().Navigation(e => e.TransactionType).AutoInclude();
        builder.Entity<Transaction>().Navigation(e => e.Entity).AutoInclude();
        builder.Entity<Transaction>().Navigation(e => e.Author).AutoInclude();
        builder.Entity<Transaction>().Navigation(e => e.Employee).AutoInclude();
        builder.Entity<Transaction>().Navigation(e => e.Department).AutoInclude();
        builder.Entity<Transaction>().Navigation(e => e.EntityModel).AutoInclude();
        
        
        builder.Entity<Department>()
            .HasIndex(e => e.Name)
            .IsUnique();
        builder.Entity<Employee>()
            .HasIndex(e => e.FIO)
            .IsUnique();
        
        // unique names for ent types, ent manufactures, transaction types, ent models
        builder.Entity<EntityType>()
            .HasIndex(e => e.Name)
            .IsUnique();
        builder.Entity<EntityManufacture>()
            .HasIndex(e => e.Name)
            .IsUnique();
        builder.Entity<EntityModel>()
            .HasIndex(e => e.Name)
            .IsUnique();
        builder.Entity<TransactionType>()
            .HasIndex(e => e.Type)
            .IsUnique();
    }
}