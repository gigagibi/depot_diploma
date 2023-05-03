using Depot.Database;
using Depot.Models.Equipments;

namespace Depot.Repositories.Entities.EntityModels;

public class EquipmentModelsRepository : GenericRepository<EntityModel>, IEquipmentModelsRepository
{
    public EquipmentModelsRepository(DepotDbContext context) : base(context)
    {
    }
}