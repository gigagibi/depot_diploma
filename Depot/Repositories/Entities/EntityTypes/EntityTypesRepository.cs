using Depot.Database;
using Depot.Models.Entities;

namespace Depot.Repositories.Entities.EntityTypes;

public class EntityTypesRepository : GenericRepository<EntityType>, IEntityTypesRepository
{
    public EntityTypesRepository(DepotDbContext context) : base(context)
    {
    }
}