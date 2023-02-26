using Depot.API.Requisitions.RequisitionStatuses.Requests;
using Depot.API.Transactions.TransactionTypes.Requests;

namespace Depot.Controllers.Requisitions.RequisitionStatuses;

public interface IRequisitionStatusesController : IGenericDictionaryCrudController<RequisitionStatusCreateRequest, RequisitionStatusUpdateRequest>
{
    
}