using Depot.Database;
using Depot.Models.Entities;

namespace Depot.Repositories.Entities.EntityManufactures;

public class EntityManufacturesRepository : GenericRepository<EntityManufacture>, IEntityManufacturesRepository
{
    public EntityManufacturesRepository(DepotDbContext context) : base(context)
    {
    }
}