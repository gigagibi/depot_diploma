using Depot.API.Entities.EntityModels.Requests;
using Depot.API.Entities.EntityModels.Responses;

namespace Depot.Services.Entities.EntityModels;

public interface IEquipmentModelsService : IGenericDictionaryCrudService<EntityModelGetResponse, EntityModelUpdateRequest, EntityModelCreateRequest, EntityModelUpdateResponse, EntityModelCreateResponse>
{
    
}