using Depot.Database;
using Depot.Models.Users;

namespace Depot.Repositories.Users;

public class UsersRepository : GenericRepository<User>, IUsersRepository
{
    public UsersRepository(DepotDbContext context) : base(context)
    {
    }
}