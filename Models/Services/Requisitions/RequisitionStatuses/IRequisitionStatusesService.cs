using Depot.API.Requisitions.RequisitionStatuses.Requests;
using Depot.API.Requisitions.RequisitionStatuses.Responses;

namespace Depot.Services.Requisitions.RequisitionStatuses;

public interface IRequisitionStatusesService : IGenericDictionaryCrudService<RequisitionStatusGetResponse, RequisitionStatusUpdateRequest, RequisitionStatusCreateRequest, RequisitionStatusUpdateResponse, RequisitionStatusCreateResponse>
{
    
}