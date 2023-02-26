using AutoMapper;
using Depot.API.Requisitions.RequisitionStatuses.Requests;
using Depot.API.Requisitions.RequisitionStatuses.Responses;
using Depot.Models.Entities;
using Depot.Models.Requisitions;
using Depot.Repositories;
using Depot.Repositories.Requisitions.RequisitionStatuses;

namespace Depot.Services.Requisitions.RequisitionStatuses;

public class RequisitionStatusesService : GenericDictionaryCrudService<RequisitionStatus, IRequisitionStatusesRepository, RequisitionStatusGetResponse, RequisitionStatusUpdateRequest, RequisitionStatusCreateRequest, RequisitionStatusUpdateResponse, RequisitionStatusCreateResponse>, IRequisitionStatusesService
{
    public RequisitionStatusesService(IRequisitionStatusesRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}