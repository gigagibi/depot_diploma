using Depot.Models.Entities;
using Depot.Models.Transactions;
using Depot.Models.Users;

namespace Depot.Repositories.Transactions;

public interface ITransactionsRepository : IGenericRepository<Transaction>
{
    Task<Transaction> CreateTransactionWithEntityAsync(Entity entity, TransactionType type, Employee author);
}