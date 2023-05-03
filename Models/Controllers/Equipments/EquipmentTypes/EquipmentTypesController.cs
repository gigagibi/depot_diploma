using Depot.API.Entities.EntityTypes.Requests;
using Depot.API.Entities.EntityTypes.Responses;
using Depot.Services.Entities;
using Depot.Services.Entities.EntitiesTypes;

namespace Depot.Controllers.Equipments.EquipmentTypes;

public class EquipmentTypesController : GenericDictionaryCrudController<IEquipmentTypesService, EntityTypeGetResponse, EntityTypeCreateRequest, EntityTypeUpdateRequest, EntityTypeCreateResponse, EntityTypeUpdateResponse>, IEquipmentTypesController
{
    public EquipmentTypesController(IEquipmentTypesService service) : base(service)
    {
    }
}