using System.Transactions;
using AutoMapper;
using Depot.API.Requisitions.Requests;
using Depot.API.Requisitions.Responses;
using Depot.Models.Requisitions;
using Depot.Repositories;
using Depot.Repositories.Entities;
using Depot.Repositories.Requisitions;
using Depot.Repositories.Requisitions.RequisitionStatuses;

namespace Depot.Services.Requisitions;

public class RequisitionsService : GenericDictionaryCrudService<Requisition, IRequisitionsRepository, RequisitionGetResponse, RequisitionsUpdateRequest, RequisitionsCreateRequest, RequisitionUpdateResponse, RequisitionCreateResponse>, IRequisitionsService
{
    private IRequisitionStatusesRepository _statusesRepository;
    public RequisitionsService(IRequisitionsRepository repository, IMapper mapper, IRequisitionStatusesRepository statusesRepository) : base(repository, mapper)
    {
        _statusesRepository = statusesRepository;
    }

    public async Task<RequisitionUpdateResponse> SetStatus(int id, int statusId)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var req = await _repository.GetAsync(id);
        var status = await _statusesRepository.GetAsync(statusId);
        req.RequisitionStatus = status;
        var savedReq = await _repository.UpdateAsync(req);
        return _mapper.Map<RequisitionUpdateResponse>(savedReq);
    }
}