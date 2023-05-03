using AutoMapper;
using Depot.API.Entities.EntityTypes.Requests;
using Depot.API.Entities.EntityTypes.Responses;
using Depot.Models.Equipments;
using Depot.Repositories.Entities.EntityTypes;

namespace Depot.Services.Entities.EntitiesTypes;

public class EquipmentTypesService : GenericDictionaryCrudService<EntityType, IEntityTypesRepository, EntityTypeGetResponse, EntityTypeUpdateRequest, EntityTypeCreateRequest, EntityTypeUpdateResponse, EntityTypeCreateResponse>, IEquipmentTypesService
{
    public EquipmentTypesService(IEntityTypesRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}