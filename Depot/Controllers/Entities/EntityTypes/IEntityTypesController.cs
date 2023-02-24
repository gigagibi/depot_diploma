using Depot.API.Entities.EntityTypes.Requests;

namespace Depot.Controllers.Entities.EntityTypes;

public interface IEntityTypesController : IGenericDictionaryCrudController<EntityTypeCreateRequest, EntityTypeUpdateRequest>
{
    
}