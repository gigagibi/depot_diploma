using Depot.API.Entities.EntityManufactures.Requests;
using Depot.API.Entities.EntityManufactures.Responses;

namespace Depot.Services.Entities.EntitiesManufactures;

public interface IEntityManufactureService : IGenericDictionaryCrudService<EntityManufactureGetResponse, EntityManufactureUpdateRequest, EntityManufactureCreateRequest, EntityManufactureUpdateResponse, EntityManufactureCreateResponse>
{

}