using Depot.API;
using Depot.API.Positions.Requests;
using Depot.API.Positions.Responses;
using Depot.API.Requisitions.Requests;
using Depot.API.Requisitions.Responses;
using Depot.API.Transactions.Transaction.Responses;

namespace Depot.Services.Positions;

public interface IPositionsService : IGenericDictionaryCrudService<PositionGetResponse, PositionUpdateRequest, PositionCreateRequest, PositionUpdateResponse, PositionCreateResponse>
{
}