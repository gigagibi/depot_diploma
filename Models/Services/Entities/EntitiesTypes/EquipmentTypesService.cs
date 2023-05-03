using AutoMapper;
using Depot.API.Entities.EntityTypes.Requests;
using Depot.API.Entities.EntityTypes.Responses;
using Depot.Models.Equipments;
using Depot.Repositories.Entities.EntityTypes;

namespace Depot.Services.Entities.EntitiesTypes;

public class EquipmentTypesService : GenericDictionaryCrudService<EquipmentType, IEquipmentTypesRepository, EntityTypeGetResponse, EntityTypeUpdateRequest, EntityTypeCreateRequest, EntityTypeUpdateResponse, EntityTypeCreateResponse>, IEquipmentTypesService
{
    public EquipmentTypesService(IEquipmentTypesRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}