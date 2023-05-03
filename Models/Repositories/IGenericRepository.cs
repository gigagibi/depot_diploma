using Depot.API;
using Depot.Models;

namespace Depot.Repositories;

public interface IGenericRepository<T> where T : IGenericModel
{
    Task<T> CreateAsync(T t);
    Task<T> UpdateAsync(T t);
    Task DeleteAsync(int id);
    Task ArchiveAsync(int id);
    Task<T> GetAsync(int id, bool returnArchived = false);
    Task<IEnumerable<T>> GetAllAsync(PaginationParameters? paginationParameters = null, bool returnArchived = false);
    IEnumerable<T> GetAllByPredicate(Func<T, bool> predicate, PaginationParameters? paginationParameters = null, bool returnArchived = false);
        
}