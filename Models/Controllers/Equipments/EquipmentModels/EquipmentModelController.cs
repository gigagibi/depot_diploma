using Depot.API.Entities.EntityModels.Requests;
using Depot.API.Entities.EntityModels.Responses;
using Depot.Services.Entities;
using Depot.Services.Entities.EntityModels;

namespace Depot.Controllers.Equipments.EquipmentModels;

public class EquipmentModelController : GenericDictionaryCrudController<IEquipmentModelsService, EntityModelGetResponse, EntityModelCreateRequest, EntityModelUpdateRequest, EntityModelCreateResponse, EntityModelUpdateResponse>, IEquipmentModelController
{
    public EquipmentModelController(IEquipmentModelsService service) : base(service)
    {
    }
}