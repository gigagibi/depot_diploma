using Depot.API.Positions.Requests;
using Depot.API.Requisitions.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Depot.Controllers.Requisitions;

public interface IPositionsController : IGenericDictionaryCrudController<PositionCreateRequest, PositionUpdateRequest>
{

}