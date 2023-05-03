using Depot.API;
using Depot.API.Entities.Entity.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Depot.Controllers.Equipments;

public interface IEquipmentController
{
    Task<IActionResult> GetAsync(int? id, int? invNumber, PaginationParameters paginationParameters, bool all = false);
    Task<IActionResult> GetPartsAsync(int? id, int? invNumber, PaginationParameters paginationParameters, bool all = false);
    Task<IActionResult> GetFatherAsync(int? id, int? invNumber);
    Task<IActionResult> GenerateInvNumberAsync();
    
    Task<IActionResult> CheckIfInvNumberExists(int invNumber);
    Task<IActionResult> ReserveAsync(int? id, int? invNumber);
    Task<IActionResult> UnreserveAsync(int? id, int? invNumber);
    Task<IActionResult> UpdateAsync(int? id, int? invNumber, EntityUpdateRequest request);
    Task<IActionResult> CreateAsync(EntityCreateRequest request);
    Task<IActionResult> DeleteAsync(int? id, int? invNumber);
    Task<IActionResult> ArchiveAsync(int? id, int? invNumber);
}