using Depot.Database;
using Depot.Models.Equipments;

namespace Depot.Repositories.Entities.EntityTypes;

public class EquipmentTypesRepository : GenericRepository<EquipmentType>, IEquipmentTypesRepository
{
    public EquipmentTypesRepository(DepotDbContext context) : base(context)
    {
    }
}