using Depot.API;
using Microsoft.AspNetCore.Mvc;

namespace Depot.Controllers;

public interface IGenericDictionaryCrudController<in TCreateRequest, in TUpdateRequest>
{
    Task<IActionResult> GetAsync (int? id = null,
        PaginationParameters? paginationParameters = null, bool all = false);
    Task<IActionResult> DeleteAsync (int id);
    Task<IActionResult> ArchiveAsync (int id);
    Task<IActionResult> CreateAsync (TCreateRequest request);
    Task<IActionResult> UpdateAsync (int id, TUpdateRequest request);
}