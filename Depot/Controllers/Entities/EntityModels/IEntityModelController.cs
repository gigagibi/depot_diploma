using Depot.API.Entities.EntityModels.Requests;

namespace Depot.Controllers.Entities.EntityModels;

public interface IEntityModelController : IGenericDictionaryCrudController<EntityModelCreateRequest, EntityModelUpdateRequest>
{
    
}