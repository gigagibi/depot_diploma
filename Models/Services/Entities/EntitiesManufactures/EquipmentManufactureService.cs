using AutoMapper;
using Depot.API.Entities.EntityManufactures.Requests;
using Depot.API.Entities.EntityManufactures.Responses;
using Depot.Models.Equipments;
using Depot.Repositories.Entities.EntityManufactures;

namespace Depot.Services.Entities.EntitiesManufactures;

public class EquipmentManufactureService : GenericDictionaryCrudService<EquipmentManufacture, IEquipmentManufacturesRepository, EntityManufactureGetResponse, EntityManufactureUpdateRequest, EntityManufactureCreateRequest, EntityManufactureUpdateResponse, EntityManufactureCreateResponse>, IEquipmentManufactureService
{
    public EquipmentManufactureService(IEquipmentManufacturesRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}