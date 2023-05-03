using Depot.Database;
using Depot.Models.Equipments;

namespace Depot.Repositories.Entities.EntityManufactures;

public class EquipmentManufacturesRepository : GenericRepository<EquipmentManufacture>, IEquipmentManufacturesRepository
{
    public EquipmentManufacturesRepository(DepotDbContext context) : base(context)
    {
    }
}