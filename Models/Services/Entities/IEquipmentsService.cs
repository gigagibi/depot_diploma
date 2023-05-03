using Depot.API;
using Depot.API.Entities.Entity.Requests;
using Depot.API.Entities.Entity.Responses;

namespace Depot.Services.Entities;

public interface IEquipmentsService
{
    Task<EntityGetResponse> GetAsync(int? id, int? invNumber);
    Task<IEnumerable<EntityGetResponse>> GetAllAsync(PaginationParameters? paginationParameters = null);
    Task<IEnumerable<EntityGetResponse>> GetPartsAsync(int? id, int? invNumber, PaginationParameters? paginationParameters = null);
    Task<EntityGetResponse> GetFatherAsync(int? id, int? invNumber);
    Task<int> GenerateInvNumberAsync();
    Task<bool> CheckIfInvNumberExists(int invNumber);
    Task<EntityGetResponse> ReserveAsync(int? id, int? invNumber, string jwt);
    Task<EntityGetResponse> UnreserveAsync(int? id, int? invNumber, string jwt);
    Task<EntityUpdateResponse> UpdateAsync(int? id, int? invNumber, EntityUpdateRequest request, string jwt);
    Task<EntityCreateResponse> CreateAsync(EntityCreateRequest request, string jwt);
    Task<string> DeleteAsync(int? id, int? invNumber, string jwt);
    Task<string> ArchiveAsync(int? id, int? invNumber, string jwt);
}