using Depot.API.Entities.EntityModels.Requests;

namespace Depot.Controllers.Equipments.EquipmentModels;

public interface IEquipmentModelController : IGenericDictionaryCrudController<EntityModelCreateRequest, EntityModelUpdateRequest>
{
    
}