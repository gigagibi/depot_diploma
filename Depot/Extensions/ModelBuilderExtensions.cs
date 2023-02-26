using Depot.Models.Departments;
using Depot.Models.Entities;
using Depot.Models.Requisitions;
using Depot.Models.Transactions;
using Depot.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Depot.Extensions;

public static class ModelBuilderExtensions
{
    public static void SeedRolesAndUsers(this ModelBuilder modelBuilder)
    {
        var roles = new List<Role>()
        {
            new Role { Id = -3, Name = RoleNames.Admin, NormalizedName = RoleNames.Admin.ToUpper() },
            new Role { Id = -2, Name = RoleNames.Operator, NormalizedName = RoleNames.Operator.ToUpper() },
            new Role { Id = -1, Name = RoleNames.User, NormalizedName = RoleNames.User.ToUpper() }
        };
        modelBuilder.Entity<Role>().HasData(roles);
        
        var employees = new List<Employee>()
        {
            new Employee()
            {
                Id = -3,
                Archived = false,
                FIO = "Admin",
                Phone = "88005553535",
                UserId = -3,
                Email = "admin@admin.ru",
                Link = "admin link",
            },
            new Employee()
            {
                Id = -2,
                Archived = false,
                FIO = "Operator",
                Phone = "88005553535",
                UserId = -2,
                Email = "operator@operator.ru",
                Link = "operator link",
            },
            new Employee()
            {
                Id = -1,
                Archived = false,
                FIO = "User",
                Phone = "88005553535",
                UserId = -1,
                Email = "user@user.ru",
                Link = "user link",
            }
        };
        modelBuilder.Entity<Employee>().HasData(employees);
        var users = new List<User>()
        {
            new User
            {
                Id = -3,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmployeeId = -3
            },
            new User
            {
                Id = -2,
                UserName = "operator",
                NormalizedUserName = "OPERATOR",
                Email = "operator@operator.com",
                NormalizedEmail = "OPERATOR@OPERATOR.COM",
                EmployeeId = -2
            },
            new User
            {
                Id = -1,
                UserName = "user",
                NormalizedUserName = "USER",
                Email = "user@user.com",
                NormalizedEmail = "USER@USER.COM",
                EmployeeId = -1
            }
        };
        
        modelBuilder.Entity<User>().HasData(users);

        var passwordHasher = new PasswordHasher<User>();
        users[0].PasswordHash = passwordHasher.HashPassword(users[0], "admin");
        users[1].PasswordHash = passwordHasher.HashPassword(users[1], "operator");
        users[2].PasswordHash = passwordHasher.HashPassword(users[1], "user");

        var userRoles = new List<IdentityUserRole<int>>();
        userRoles.Add(new IdentityUserRole<int>
        {
            UserId = users[0].Id,
            RoleId = roles.First(q => q.Name == RoleNames.Admin).Id
        });
        userRoles.Add(new IdentityUserRole<int>
        {
            UserId = users[1].Id,
            RoleId = roles.First(q => q.Name == RoleNames.Operator).Id
        });
        userRoles.Add(new IdentityUserRole<int>
        {
            UserId = users[2].Id,
            RoleId = roles.First(q => q.Name == RoleNames.User).Id
        });
        modelBuilder.Entity<IdentityUserRole<int>>().HasData(userRoles);
    }

    public static void SeedRequisitionsStatuses(this ModelBuilder modelBuilder)
    {
        var statuses = new List<RequisitionStatus>()
        {
            new RequisitionStatus()
            {
                Id = -3,
                Archived = false,
                Name = RequisitionStatusName.Created
            },
            new RequisitionStatus()
            {
                Id = -2,
                Archived = false,
                Name = RequisitionStatusName.Approved
            },
            new RequisitionStatus()
            {
                Id = -1,
                Archived = false,
                Name = RequisitionStatusName.Declined
            }
        };
        modelBuilder.Entity<RequisitionStatus>().HasData(statuses);
    }
    
    public static void Seed(this ModelBuilder modelBuilder)
    {
        var departments = new List<Department>()
        {
            new Department
            {
                Id = -3, Archived = false, Description = "IT Department", Geo = "Moscow", Phone = "88005553535",
                Name = "IT"
            },
            new Department
            {
                Id = -2, Archived = false, Description = "HR Department", Geo = "Moscow", Phone = "88005553535",
                Name = "HR"
            },
            new Department
            {
                Id = -1, Archived = false, Description = "Sales Department", Geo = "Moscow", Phone = "88005553535",
                Name = "Sales"
            }
        };
        modelBuilder.Entity<Department>().HasData(departments);

        var entityTypes = new List<EntityType>()
        {
            new EntityType()
            {
                Id = -1,
                Name = "PC",
                Description = "PC",
                Archived = false
            }
        };
        modelBuilder.Entity<EntityType>().HasData(entityTypes);
        
        var entityManufactures = new List<EntityManufacture>()
        {
            new EntityManufacture()
            {
                Id = -1,
                Name = "Dell",
                Description = "Dell",
                Country = "USA",
                Archived = false
            }
        };
        modelBuilder.Entity<EntityManufacture>().HasData(entityManufactures);
        
        var entityModels = new List<EntityModel>()
        {
            new EntityModel()
            {
                Id = -1,
                Name = "Dell",
                Description = "Dell",
                Archived = false,
                EntityManufactureId = -1,
                EntityTypeId = -1
            }
        };
        modelBuilder.Entity<EntityModel>().HasData(entityModels);
        
        // create transaction types reserve, unreserve, create, update
        var transactionTypes = new List<TransactionType>()
        {
            new TransactionType()
            {
                Id = -1,
                Type = "Reserve",
                Archived = false
            },
            new TransactionType()
            {
                Id = -2,
                Type = "Unreserve",
                Archived = false
            },
            new TransactionType()
            {
                Id = -3,
                Type = "Create",
                Archived = false
            },
            new TransactionType()
            {
                Id = -4,
                Type = "Update",
                Archived = false
            },
            new TransactionType()
            {
                Id = -5,
                Type = "Archive",
                Archived = false
            },
            new TransactionType()
            {
                Id = -6,
                Type = "Delete",
                Archived = false
            }
        };
        modelBuilder.Entity<TransactionType>().HasData(transactionTypes);
    }
}
