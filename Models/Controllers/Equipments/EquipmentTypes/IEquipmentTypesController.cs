using Depot.API.Entities.EntityTypes.Requests;

namespace Depot.Controllers.Equipments.EquipmentTypes;

public interface IEquipmentTypesController : IGenericDictionaryCrudController<EntityTypeCreateRequest, EntityTypeUpdateRequest>
{
    
}