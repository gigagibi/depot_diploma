using Depot.API;
using Depot.Repositories;

namespace Depot.Services;

public interface IGenericDictionaryCrudService<TGetResponse, in TUpdateRequest, in TCreateRequest, TUpdateResponse, TCreateResponse> 
    where TGetResponse : class where TUpdateRequest : class where TCreateRequest : class 
    where TUpdateResponse : class where TCreateResponse : class
{
    Task<IEnumerable<TGetResponse>> GetAllAsync (PaginationParameters? paginationParameters = null);
    Task<TGetResponse> GetAsync (int id);
    Task<TCreateResponse> CreateAsync (TCreateRequest request);
    Task<TUpdateResponse> UpdateAsync (int id, TUpdateRequest request);
    Task DeleteAsync (int id);
    Task ArchiveAsync (int id);
}