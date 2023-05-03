using Depot.API.Positions.Requests;
using Depot.API.Positions.Responses;
using Depot.API.Requisitions.Requests;
using Depot.API.Requisitions.Responses;
using Depot.Exceptions;
using Depot.Services.Positions;
using Depot.Services.Requisitions;
using Microsoft.AspNetCore.Mvc;

namespace Depot.Controllers.Requisitions;

public class PositionsController : GenericDictionaryCrudController<IPositionsService, PositionGetResponse, PositionCreateRequest, PositionUpdateRequest, PositionCreateResponse, PositionUpdateResponse>, IPositionsController
{
    public PositionsController(IPositionsService service) : base(service)
    {
    }
}