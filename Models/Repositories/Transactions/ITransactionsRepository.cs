using Depot.Models.Equipments;
using Depot.Models.Transactions;
using Depot.Models.Users;

namespace Depot.Repositories.Transactions;

public interface ITransactionsRepository : IGenericRepository<Transaction>
{
    Task<Transaction> CreateTransactionWithEntityAsync(Equipment entity, TransactionType type, Employee author);
}