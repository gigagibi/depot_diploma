using Depot.API.Entities.EntityModels.Requests;
using Depot.API.Entities.EntityModels.Responses;
using Depot.Services.Entities;
using Depot.Services.Entities.EntityModels;

namespace Depot.Controllers.Entities.EntityModels;

public class EntityModelController : GenericDictionaryCrudController<IEntityModelsService, EntityModelGetResponse, EntityModelCreateRequest, EntityModelUpdateRequest, EntityModelCreateResponse, EntityModelUpdateResponse>, IEntityModelController
{
    public EntityModelController(IEntityModelsService service) : base(service)
    {
    }
}