using Depot.API;
using Depot.Models.Equipments;

namespace Depot.Repositories.Entities;

public interface IEquipmentsRepository : IGenericRepository<Equipment>
{
    Task<Equipment> GetByInvNumberAsync(int invNumber, bool returnArchived = false);
    Task<int> GenerateInvNumberAsync();
    Task<bool> CheckIfInvNumberExistsAsync(int invNumber);
    Task<IEnumerable<Equipment>> GetPartsAsync(int id, PaginationParameters? paginationParameters = null);
    Task<IEnumerable<Equipment>> GetPartsByInvNumberAsync(int invNumber, PaginationParameters? paginationParameters = null);
    Task<Equipment> GetFatherAsync(int id);
    Task<Equipment> GetFatherByInvNumberAsync(int invNumber);
    Task DeleteByInvNumberAsync(int invNumber);
    Task ArchiveByInvNumberAsync(int invNumber);
}