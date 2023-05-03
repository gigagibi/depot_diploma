using Depot.API.Transactions.TransactionTypes.Requests;

namespace Depot.Controllers.Transactions.TransactionTypes;

public interface ITransactionTypesController : IGenericDictionaryCrudController<TransactionTypeCreateRequest, TransactionTypeUpdateRequest>
{
    
}