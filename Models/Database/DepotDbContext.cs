using Depot.Extensions;
using Depot.Models.Departments;
using Depot.Models.Equipments;
using Depot.Models.Requisitions;
using Depot.Models.Transactions;
using Depot.Models.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Depot.Database;

public class DepotDbContext : IdentityDbContext<User, Role, int>
{
    public DbSet<Department> Departments { get; set; }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<Equipment> Entities { get; set; }

    public DbSet<EquipmentManufacture> EntityManufactures { get; set; }

    public DbSet<EntityModel> EntityModels { get; set; }

    public DbSet<EquipmentType> EntityTypes { get; set; }

    public DbSet<Transaction> Transactions { get; set; }
        
    public DbSet<TransactionType> TransactionTypes { get; set; }

    public DbSet<Requisition> Requisitions { get; set; }

    public DbSet<RequisitionStatus> RequisitionsStatuses { get; set; }

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
        builder.SeedRequisitionsStatuses();
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
        
        builder.Entity<Equipment>().Navigation(e => e.Employee).AutoInclude();
        builder.Entity<Equipment>().Navigation(e => e.EntityModel).AutoInclude().AutoInclude();
        builder.Entity<Equipment>().Navigation(e => e.Department).AutoInclude();
        builder.Entity<Equipment>()
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
        
        builder.Entity<Requisition>().Navigation(e => e.Employee).AutoInclude();
        builder.Entity<Requisition>().Navigation(e => e.Entity).AutoInclude();
        builder.Entity<Requisition>().Navigation(e => e.Operator).AutoInclude();
        builder.Entity<Requisition>().Navigation(e => e.RequisitionStatus).AutoInclude();

        builder.Entity<Department>()
            .HasIndex(e => e.Name)
            .IsUnique();
        builder.Entity<Employee>()
            .HasIndex(e => e.FIO)
            .IsUnique();
        
        builder.Entity<EquipmentType>()
            .HasIndex(e => e.Name)
            .IsUnique();
        builder.Entity<EquipmentManufacture>()
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