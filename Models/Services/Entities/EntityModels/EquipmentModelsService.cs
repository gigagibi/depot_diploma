using AutoMapper;
using Depot.API.Entities.EntityModels.Requests;
using Depot.API.Entities.EntityModels.Responses;
using Depot.Models.Equipments;
using Depot.Repositories.Entities.EntityModels;

namespace Depot.Services.Entities.EntityModels;

public class EquipmentModelsService : GenericDictionaryCrudService<EntityModel, IEquipmentModelsRepository, EntityModelGetResponse, EntityModelUpdateRequest, EntityModelCreateRequest, EntityModelUpdateResponse, EntityModelCreateResponse>, IEquipmentModelsService
{
    public EquipmentModelsService(IEquipmentModelsRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}