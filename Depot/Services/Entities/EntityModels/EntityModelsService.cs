using AutoMapper;
using Depot.API.Entities.EntityModels.Requests;
using Depot.API.Entities.EntityModels.Responses;
using Depot.Models.Entities;
using Depot.Repositories.Entities.EntityModels;

namespace Depot.Services.Entities.EntityModels;

public class EntityModelsService : GenericDictionaryCrudService<EntityModel, IEntityModelsRepository, EntityModelGetResponse, EntityModelUpdateRequest, EntityModelCreateRequest, EntityModelUpdateResponse, EntityModelCreateResponse>, IEntityModelsService
{
    public EntityModelsService(IEntityModelsRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}