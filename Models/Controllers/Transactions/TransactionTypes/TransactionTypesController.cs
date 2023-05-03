using Depot.API.Transactions.TransactionTypes.Requests;
using Depot.API.Transactions.TransactionTypes.Responses;
using Depot.Services.Transactions;

namespace Depot.Controllers.Transactions.TransactionTypes;

public class TransactionTypesController : GenericDictionaryCrudController<ITransactionTypesService, TransactionTypeGetResponse, TransactionTypeCreateRequest, TransactionTypeUpdateRequest, TransactionTypeCreateResponse, TransactionTypeUpdateResponse>, ITransactionTypesController
{
    public TransactionTypesController(ITransactionTypesService service) : base(service)
    {
    }
}