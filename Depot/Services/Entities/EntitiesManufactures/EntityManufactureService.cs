using AutoMapper;
using Depot.API.Entities.EntityManufactures.Requests;
using Depot.API.Entities.EntityManufactures.Responses;
using Depot.Models.Entities;
using Depot.Repositories.Entities.EntityManufactures;

namespace Depot.Services.Entities.EntitiesManufactures;

public class EntityManufactureService : GenericDictionaryCrudService<EntityManufacture, IEntityManufacturesRepository, EntityManufactureGetResponse, EntityManufactureUpdateRequest, EntityManufactureCreateRequest, EntityManufactureUpdateResponse, EntityManufactureCreateResponse>, IEntityManufactureService
{
    public EntityManufactureService(IEntityManufacturesRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}