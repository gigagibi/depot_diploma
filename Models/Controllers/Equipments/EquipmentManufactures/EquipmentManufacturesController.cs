using Depot.API.Entities.EntityManufactures.Requests;
using Depot.API.Entities.EntityManufactures.Responses;
using Depot.Services.Entities;
using Depot.Services.Entities.EntitiesManufactures;

namespace Depot.Controllers.Equipments.EquipmentManufactures;

public class EquipmentManufacturesController : GenericDictionaryCrudController<IEquipmentManufactureService, EntityManufactureGetResponse, EntityManufactureCreateRequest, EntityManufactureUpdateRequest, EntityManufactureCreateResponse, EntityManufactureUpdateResponse>, IEquipmentManufacturesController
{
    public EquipmentManufacturesController(IEquipmentManufactureService service) : base(service)
    {
    }
}