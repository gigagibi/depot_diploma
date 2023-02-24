using Depot.API;
using Depot.Models.Entities;

namespace Depot.Repositories.Entities;

public interface IEntitiesRepository : IGenericRepository<Entity>
{
    Task<Entity> GetByInvNumberAsync(int invNumber, bool returnArchived = false);
    Task<int> GenerateInvNumberAsync();
    Task<bool> CheckIfInvNumberExistsAsync(int invNumber);
    Task<IEnumerable<Entity>> GetPartsAsync(int id, PaginationParameters? paginationParameters = null);
    Task<IEnumerable<Entity>> GetPartsByInvNumberAsync(int invNumber, PaginationParameters? paginationParameters = null);
    Task<Entity> GetFatherAsync(int id);
    Task<Entity> GetFatherByInvNumberAsync(int invNumber);
    Task DeleteByInvNumberAsync(int invNumber);
    Task ArchiveByInvNumberAsync(int invNumber);
}