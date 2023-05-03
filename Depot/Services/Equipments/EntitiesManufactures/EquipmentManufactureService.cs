using AutoMapper;
using Depot.API.Entities.EntityManufactures.Requests;
using Depot.API.Entities.EntityManufactures.Responses;
using Depot.Models.Equipments;
using Depot.Repositories.Entities.EntityManufactures;

namespace Depot.Services.Entities.EntitiesManufactures;

public class EquipmentManufactureService : GenericDictionaryCrudService<EntityManufacture, IEntityManufacturesRepository, EntityManufactureGetResponse, EntityManufactureUpdateRequest, EntityManufactureCreateRequest, EntityManufactureUpdateResponse, EntityManufactureCreateResponse>, IEquipmentManufactureService
{
    public EquipmentManufactureService(IEntityManufacturesRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}