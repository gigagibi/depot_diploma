using AutoMapper;
using Depot.API;
using Depot.Models;
using Depot.Repositories;

namespace Depot.Services;

public abstract class GenericDictionaryCrudService<TModel, TRepository, TGetResponse, TUpdateRequest, TCreateRequest, TUpdateResponse, TCreateResponse> : IGenericDictionaryCrudService<TGetResponse, TUpdateRequest, TCreateRequest, TUpdateResponse, TCreateResponse>
    where TGetResponse : class where TUpdateRequest : class where TCreateRequest : class 
    where TUpdateResponse : class where TCreateResponse : class where TModel : class, IGenericModel
    where TRepository : IGenericRepository<TModel>
{
    private readonly IGenericRepository<TModel> _repository;
    private readonly IMapper _mapper;

    protected GenericDictionaryCrudService(IGenericRepository<TModel> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TGetResponse>> GetAllAsync(PaginationParameters? paginationParameters = null)
    {
        var entity = await _repository.GetAllAsync(paginationParameters);
        return _mapper.Map<IEnumerable<TGetResponse>>(entity);
    }

    public async Task<TGetResponse> GetAsync(int id)
    {
        var entity = await _repository.GetAsync(id);
        return _mapper.Map<TGetResponse>(entity);
    }

    public async Task<TCreateResponse> CreateAsync(TCreateRequest request)
    {
        var entity = _mapper.Map<TModel>(request);
        var savedEntity = await _repository.CreateAsync(entity);
        return _mapper.Map<TCreateResponse>(savedEntity);
    }

    public async Task<TUpdateResponse> UpdateAsync(int id, TUpdateRequest request)
    {
        var oldEntity = await _repository.GetAsync(id);
        var mappedEntity = _mapper.Map(request, oldEntity);
        var savedEntity = await _repository.UpdateAsync(mappedEntity);
        return _mapper.Map<TUpdateResponse>(savedEntity);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task ArchiveAsync(int id)
    {
        await _repository.ArchiveAsync(id);
    }
}