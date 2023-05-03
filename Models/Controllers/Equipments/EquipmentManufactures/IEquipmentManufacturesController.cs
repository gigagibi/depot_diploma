using Depot.API;
using Depot.API.Entities.EntityManufactures.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Depot.Controllers.Equipments.EquipmentManufactures;

public interface IEquipmentManufacturesController : IGenericDictionaryCrudController<EntityManufactureCreateRequest, EntityManufactureUpdateRequest>
{
    
}