using System.Reflection;
using Depot.Database;
using Depot.Models.Equipments;
using Depot.Models.Transactions;
using Depot.Models.Users;

namespace Depot.Repositories.Transactions;

public class TransactionsRepository : GenericRepository<Transaction>, ITransactionsRepository
{
    public TransactionsRepository(DepotDbContext context) : base(context)
    {
    }
    
    public async Task<Transaction> CreateTransactionWithEntityAsync(Equipment entity, TransactionType type, Employee author)
    {
        var transaction = new Transaction
        {
            Entity = entity,
            TransactionType = type,
            Date = DateTime.Now,
            Author = author
        };
        var entityType = entity.GetType();
        var transactionType = transaction.GetType();

        foreach (var property in entityType.GetProperties())
        {
            var propertyValue = property.GetValue(entity);
            if(property.Name == "Id" || property.Name == "Archived")
                continue;
            var targetProperty = transactionType.GetProperty(property.Name);
            targetProperty?.SetValue(transaction, propertyValue);
        }
        transaction.EntityArchived = entity.Archived;
        var entry = _dbSet.Add(transaction);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }
}
