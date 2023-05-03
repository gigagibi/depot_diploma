using Depot.Database;
using Depot.Models.Users;

namespace Depot.Repositories.Positions
{
    public class PositionsRepository : GenericRepository<Position>, IPositionsRepository
    {
        public PositionsRepository(DepotDbContext context) : base(context)
        {
        }
    }
}
