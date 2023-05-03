using Depot.API.Entities.EntityTypes.Requests;
using Depot.API.Entities.EntityTypes.Responses;

namespace Depot.Services.Entities.EntitiesTypes;

public interface IEquipmentTypesService : IGenericDictionaryCrudService<EntityTypeGetResponse, EntityTypeUpdateRequest, EntityTypeCreateRequest, EntityTypeUpdateResponse, EntityTypeCreateResponse>
{

}