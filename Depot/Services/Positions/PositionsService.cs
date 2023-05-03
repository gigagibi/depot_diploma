using AutoMapper;
using Depot.API.Positions.Requests;
using Depot.API.Positions.Responses;
using Depot.Models.Users;
using Depot.Repositories;
using Depot.Repositories.Positions;

namespace Depot.Services.Positions;

public class PositionsService : GenericDictionaryCrudService<Position, IPositionsRepository, PositionGetResponse, PositionUpdateRequest, PositionCreateRequest, PositionUpdateResponse, PositionCreateResponse>, IPositionsService
{
    public PositionsService(IGenericRepository<Position> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}