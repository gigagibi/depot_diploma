using Depot.Database;
using Depot.Models.Transactions;

namespace Depot.Repositories.Transactions;

public class TransactionTypesRepository : GenericRepository<TransactionType>, ITransactionTypesRepository
{
    public TransactionTypesRepository(DepotDbContext context) : base(context)
    {
    }
}