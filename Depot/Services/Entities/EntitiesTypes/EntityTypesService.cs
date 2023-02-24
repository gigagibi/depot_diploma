using AutoMapper;
using Depot.API.Entities.EntityTypes.Requests;
using Depot.API.Entities.EntityTypes.Responses;
using Depot.Models.Entities;
using Depot.Repositories.Entities.EntityTypes;

namespace Depot.Services.Entities.EntitiesTypes;

public class EntityTypesService : GenericDictionaryCrudService<EntityType, IEntityTypesRepository, EntityTypeGetResponse, EntityTypeUpdateRequest, EntityTypeCreateRequest, EntityTypeUpdateResponse, EntityTypeCreateResponse>, IEntityTypesService
{
    public EntityTypesService(IEntityTypesRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}