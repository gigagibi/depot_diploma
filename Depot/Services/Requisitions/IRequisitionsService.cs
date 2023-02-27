using Depot.API;
using Depot.API.Requisitions.Requests;
using Depot.API.Requisitions.Responses;
using Depot.API.Transactions.Transaction.Responses;

namespace Depot.Services.Requisitions;

public interface IRequisitionsService : IGenericDictionaryCrudService<RequisitionGetResponse, RequisitionUpdateRequest, RequisitionCreateRequest, RequisitionUpdateResponse, RequisitionCreateResponse>
{
    Task<RequisitionUpdateResponse> SetStatus(int id, int statusId);
}