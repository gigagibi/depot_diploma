using Depot.API;
using Depot.API.Entities.EntityManufactures.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Depot.Controllers.Entities.EntityManufactures;

public interface IEntityManufacturesController : IGenericDictionaryCrudController<EntityManufactureCreateRequest, EntityManufactureUpdateRequest>
{
    
}