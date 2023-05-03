using Depot.API;
using Depot.API.Transactions.Transaction.Responses;

namespace Depot.Services.Transactions;

public interface ITransactionsService
{
    Task<IEnumerable<TransactionGetResponse>> GetAllAsync(PaginationParameters? paginationParameters = null);
    Task<TransactionGetResponse> GetAsync(int id);
    Task<IEnumerable<TransactionGetResponse>> GetByEntityIdAsync(int entityId, PaginationParameters? paginationParameters = null);
    Task<IEnumerable<TransactionGetResponse>> GetByEntityInvNumberAsync(int invNumber, PaginationParameters? paginationParameters = null);
    
}