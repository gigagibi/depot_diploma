using Depot.Database;
using Depot.Models.Departments;

namespace Depot.Repositories.Departments;

public class DepartmentsRepository : GenericRepository<Department>, IDepartmentsRepository
{
    public DepartmentsRepository(DepotDbContext context) : base(context)
    {
    }
}