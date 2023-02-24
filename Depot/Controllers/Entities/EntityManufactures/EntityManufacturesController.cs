using Depot.API.Entities.EntityManufactures.Requests;
using Depot.API.Entities.EntityManufactures.Responses;
using Depot.Services.Entities;
using Depot.Services.Entities.EntitiesManufactures;

namespace Depot.Controllers.Entities.EntityManufactures;

public class EntityManufacturesController : GenericDictionaryCrudController<IEntityManufactureService, EntityManufactureGetResponse, EntityManufactureCreateRequest, EntityManufactureUpdateRequest, EntityManufactureCreateResponse, EntityManufactureUpdateResponse>, IEntityManufacturesController
{
    public EntityManufacturesController(IEntityManufactureService service) : base(service)
    {
    }
}