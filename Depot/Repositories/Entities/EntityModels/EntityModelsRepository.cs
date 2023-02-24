using Depot.Database;
using Depot.Models.Entities;

namespace Depot.Repositories.Entities.EntityModels;

public class EntityModelsRepository : GenericRepository<EntityModel>, IEntityModelsRepository
{
    public EntityModelsRepository(DepotDbContext context) : base(context)
    {
    }
}