using Depot.API.Entities.EntityTypes.Requests;
using Depot.API.Entities.EntityTypes.Responses;
using Depot.Services.Entities;
using Depot.Services.Entities.EntitiesTypes;

namespace Depot.Controllers.Entities.EntityTypes;

public class EntityTypesController : GenericDictionaryCrudController<IEntityTypesService, EntityTypeGetResponse, EntityTypeCreateRequest, EntityTypeUpdateRequest, EntityTypeCreateResponse, EntityTypeUpdateResponse>, IEntityTypesController
{
    public EntityTypesController(IEntityTypesService service) : base(service)
    {
    }
}