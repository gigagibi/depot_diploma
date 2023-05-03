using Depot.API.Requisitions.RequisitionStatuses.Requests;
using Depot.API.Requisitions.RequisitionStatuses.Responses;
using Depot.Services.Requisitions.RequisitionStatuses;

namespace Depot.Controllers.Requisitions.RequisitionStatuses;

public class RequisitionStatusesController : GenericDictionaryCrudController<IRequisitionStatusesService, RequisitionStatusGetResponse, RequisitionStatusCreateRequest, RequisitionStatusUpdateRequest, RequisitionStatusCreateResponse, RequisitionStatusUpdateResponse>, IRequisitionStatusesController
{
    public RequisitionStatusesController(IRequisitionStatusesService service) : base(service)
    {
    }
}