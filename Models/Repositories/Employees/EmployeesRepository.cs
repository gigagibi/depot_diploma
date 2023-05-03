using Depot.Database;
using Depot.Models.Users;

namespace Depot.Repositories.Employees;

public class EmployeesRepository : GenericRepository<Employee>, IEmployeesRepository
{
    public EmployeesRepository(DepotDbContext context) : base(context)
    {
    }
}