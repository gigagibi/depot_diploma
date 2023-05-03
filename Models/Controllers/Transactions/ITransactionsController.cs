using Depot.API;
using Microsoft.AspNetCore.Mvc;

namespace Depot.Controllers.Transactions;

public interface ITransactionsController
{
    Task<IActionResult> GetAsync(int? id, int? invNumber, int? entityId, PaginationParameters? paginationParameters = null, bool all = false);
}