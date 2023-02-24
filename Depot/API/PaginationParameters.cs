namespace Depot.API;

public record PaginationParameters
(
    int MaxPageSize = 50,
    int PageNumber = 1,
    int PageSize = 10,
    string SortBy = "id"
);