using System.Transactions;
using AutoMapper;
using Depot.API;
using Depot.API.Entities.Entity.Requests;
using Depot.API.Entities.Entity.Responses;
using Depot.Exceptions;
using Depot.Helpers;
using Depot.Models.Entities;
using Depot.Models.Transactions;
using Depot.Models.Users;
using Depot.Repositories.Entities;
using Depot.Repositories.Transactions;
using Transaction = Depot.Models.Transactions.Transaction;

namespace Depot.Services.Entities;

// TODO везде логирование
// TODO Продумать что делать с транзакцяим при удалении
public class EntitiesService : IEntitiesService
{
    private readonly IEntitiesRepository _entitiesRepository;
    private readonly ITransactionsRepository _transactionsRepository;
    private readonly ITransactionTypesRepository _transactionTypesRepository;
    private readonly IMapper _mapper;
    private readonly JwtHelper _jwtHelper;
    private readonly ILogger<EntitiesService> _logger;

    public EntitiesService(IEntitiesRepository entitiesRepository, IMapper mapper, JwtHelper jwtHelper, ITransactionsRepository transactionsRepository, ITransactionTypesRepository transactionTypesRepository, ILogger<EntitiesService> logger)
    {
        _entitiesRepository = entitiesRepository;
        _mapper = mapper;
        _jwtHelper = jwtHelper;
        _transactionsRepository = transactionsRepository;
        _transactionTypesRepository = transactionTypesRepository;
        _logger = logger;
    }

    public async Task<EntityGetResponse> GetAsync(int? id, int? invNumber)
    {
        if (id is not null)
        {
            var entity = await _entitiesRepository.GetAsync(id.Value);
            return _mapper.Map<EntityGetResponse>(entity);
        }
        if (invNumber is not null)
        {
            var entity = _entitiesRepository.GetAllByPredicate(e => e.InvNumber == invNumber).FirstOrDefault() ?? throw new ModelNotFoundException("Entity with given invNumber not found");
            return _mapper.Map<EntityGetResponse>(entity);
        }
        throw new ArgumentException("Given arguments are null");
    }

    public async Task<IEnumerable<EntityGetResponse>> GetAllAsync(PaginationParameters? paginationParameters = null)
    {
        var entities = await _entitiesRepository.GetAllAsync(paginationParameters);
        return _mapper.Map<IEnumerable<EntityGetResponse>>(entities);
    }

    public async Task<IEnumerable<EntityGetResponse>> GetPartsAsync(int? id, int? invNumber, PaginationParameters? paginationParameters = null)
    {
        if (id is not null)
        {
            var parts = await _entitiesRepository.GetPartsAsync(id.Value, paginationParameters);
            return _mapper.Map<IEnumerable<EntityGetResponse>>(parts);            
        }
        if (invNumber is not null)
        {
            var parts = await _entitiesRepository.GetPartsByInvNumberAsync(invNumber.Value, paginationParameters);
            return _mapper.Map<IEnumerable<EntityGetResponse>>(parts);
        }
        throw new ArgumentException("Given arguments are null");
    }

    public async Task<EntityGetResponse> GetFatherAsync(int? id, int? invNumber)
    {
        if (id is not null)
        {
            var father = await _entitiesRepository.GetFatherAsync(id.Value);
            return _mapper.Map<EntityGetResponse>(father);
        }
        if (invNumber is not null)
        {
            var father = await _entitiesRepository.GetFatherByInvNumberAsync(invNumber.Value);
            return _mapper.Map<EntityGetResponse>(father);
        }
        throw new ArgumentException("Given arguments are null");
    }

    public async Task<int> GenerateInvNumberAsync()
    {
        return await _entitiesRepository.GenerateInvNumberAsync();
    }

    public async Task<bool> CheckIfInvNumberExists(int invNumber)
    {
        return await _entitiesRepository.CheckIfInvNumberExistsAsync(invNumber);
    }

    public async Task<EntityGetResponse> ReserveAsync(int? id, int? invNumber, string jwt)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var trType = _transactionTypesRepository.GetAllByPredicate(t => t.Type.ToLower() == "reserve").First();
        Entity entity;
        if (id is not null)
        {
            entity = await _entitiesRepository.GetAsync(id.Value);
        }
        else if (invNumber is not null)
        {
            entity = await _entitiesRepository.GetByInvNumberAsync(invNumber.Value);
        }
        else
        {
            throw new ArgumentException("Given arguments are null");
        }
        if (entity.EmployeeId != null)
        {
            _logger.LogWarning("{nameof(Entity)} with id={id} is already reserved", nameof(Entity), id);
            throw new EntityReservationException("Entity is already reserved");
        }
        entity.Employee = await _jwtHelper.GetEmployeeFromJwt(jwt);
        var savedEntity = await _entitiesRepository.UpdateAsync(entity);
        await _transactionsRepository.CreateTransactionWithEntityAsync(savedEntity, trType, entity.Employee);
        scope.Complete();
        return _mapper.Map<EntityGetResponse>(savedEntity);
    }

    public async Task<EntityGetResponse> UnreserveAsync(int? id, int? invNumber, string jwt)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var trType = _transactionTypesRepository.GetAllByPredicate(t => t.Type.ToLower() == "unreserve").First();
        Entity entity;
        if (id is not null)
        {
            entity = await _entitiesRepository.GetAsync(id.Value);
        }
        else if (invNumber is not null)
        {
            entity = await _entitiesRepository.GetByInvNumberAsync(invNumber.Value);
        }
        else
        {
            throw new ArgumentException("Given arguments are null");
        }
        var employee = await _jwtHelper.GetEmployeeFromJwt(jwt);
        if (entity.Employee != employee)
        {
            _logger.LogWarning("Can't unreserve {nameof(Entity)} with id={id}, because it's reserved by another employee or not reserved at all", nameof(Entity), id);
            throw new EntityReservationException("Can't unreserve entity, because it's reserved by another employee or not reserved at all");
        }
        entity.Employee = null;
        var savedEntity = await _entitiesRepository.UpdateAsync(entity);
        await _transactionsRepository.CreateTransactionWithEntityAsync(savedEntity, trType, employee);
        scope.Complete();
        return _mapper.Map<EntityGetResponse>(savedEntity);
    }
    
    public async Task<EntityUpdateResponse> UpdateAsync(int? id, int? invNumber, EntityUpdateRequest request, string jwt)
    { 
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        Entity entity;
        if (id is not null)
        {
            entity = await _entitiesRepository.GetAsync(id.Value);
        }
        else if (invNumber is not null)
        {
            entity = await _entitiesRepository.GetByInvNumberAsync(invNumber.Value);
        }
        else
        {
            throw new ArgumentException("Given arguments are null");
        }
        if (_entitiesRepository.CheckIfInvNumberExistsAsync(request.InvNumber).Result && entity.InvNumber != request.InvNumber)
        {
            _logger.LogWarning("Can't update {nameof(Entity)} with id={id}, because {nameof(Entity)} with invNumber={invNumber} already exists", nameof(Entity), id, nameof(Entity), request.InvNumber);
            throw new InvNumberAlreadyExistsException("InvNumber already exists");
        }
        var prevPartOfField = entity.PartOfField;
        var prevPartOf = entity.PartOf;
        var trType = _transactionTypesRepository.GetAllByPredicate(t => t.Type.ToLower() == "update").First();
        var mappedEntity = _mapper.Map(request, entity);
        var savedEntity = await _entitiesRepository.UpdateAsync(mappedEntity);
        var author = await _jwtHelper.GetEmployeeFromJwt(jwt);
        await _transactionsRepository.CreateTransactionWithEntityAsync(savedEntity, trType, author);
        await UpdateFather(savedEntity.PartOfField, savedEntity.PartOf, author, prevPartOfField, prevPartOf);
        scope.Complete();
        return _mapper.Map<EntityUpdateResponse>(savedEntity);
    }

    public async Task<EntityCreateResponse> CreateAsync(EntityCreateRequest request, string jwt)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        request.InvNumber ??= await _entitiesRepository.GenerateInvNumberAsync();
        if (_entitiesRepository.CheckIfInvNumberExistsAsync(request.InvNumber.Value).Result)
        {
            _logger.LogWarning("Can't create {nameof(Entity)}, because invNumber={invNumber} already exists", nameof(Entity), request.InvNumber);
            throw new InvNumberAlreadyExistsException("InvNumber already exists");
        }
        var trType = _transactionTypesRepository.GetAllByPredicate(t => t.Type.ToLower() == "create").First();
        var mappedEntity = _mapper.Map<Entity>(request);
        var savedEntity = await _entitiesRepository.CreateAsync(mappedEntity);
        var author = await _jwtHelper.GetEmployeeFromJwt(jwt);
        await _transactionsRepository.CreateTransactionWithEntityAsync(savedEntity, trType, author);
        await UpdateFather(savedEntity.PartOfField, savedEntity.PartOf, author);
        scope.Complete();
        return _mapper.Map<EntityCreateResponse>(savedEntity);
    }

    public async Task<string> DeleteAsync(int? id, int? invNumber, string jwt)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        Entity entity;
        if (id is not null)
        {
            entity = await _entitiesRepository.GetAsync(id.Value);
        }
        else if (invNumber is not null)
        {
            entity = await _entitiesRepository.GetByInvNumberAsync(invNumber.Value);
        }
        else
        {
            throw new ArgumentException("Given arguments are null");
        }
        // TODO продумать действие над родительским или дочерними элементами
        // TODO продумать действие над транзакциями, пока что реализовано обнуление ссылки на УЕ
        var transactions = _transactionsRepository.GetAllByPredicate(t => t.EntityId == entity.Id);
        foreach (var transaction in transactions)
        {
            transaction.EntityId = null;
            transaction.Entity = null;
            transaction.Employee = null;
            transaction.EmployeeId = null;
            transaction.Author = null;
            transaction.AuthorId = null;
            transaction.TransactionType = null;
            transaction.Area = null;
            transaction.Comment = null;
            transaction.Department = null;
            transaction.BinvNumber = null;
            transaction.Status = null;
            transaction.MOT = null;
            transaction.GlType = null;
            transaction.PartOfField = null;
            await _transactionsRepository.UpdateAsync(transaction);
        }
        await _entitiesRepository.DeleteAsync(entity.Id);
        scope.Complete();
        return "Entity deleted";
    }

    // TODO что делать с parts при архивации или удалении father
    public async Task<string> ArchiveAsync(int? id, int? invNumber, string jwt)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        Entity entity;
        if (id is not null)
        {
            await _entitiesRepository.ArchiveAsync(id.Value);       
            entity = await _entitiesRepository.GetAsync(id.Value, true);
        }
        else if (invNumber is not null)
        {
            await _entitiesRepository.ArchiveByInvNumberAsync(invNumber.Value);
            entity = await _entitiesRepository.GetByInvNumberAsync(invNumber.Value, true);
        }
        else
        {
            throw new ArgumentException("Given arguments are null");
        }
        var trType = _transactionTypesRepository.GetAllByPredicate(t => t.Type.ToLower() == "archive").First();
        var author = await _jwtHelper.GetEmployeeFromJwt(jwt);
        await _transactionsRepository.CreateTransactionWithEntityAsync(entity, trType, author);
        scope.Complete();
        return "Entity archived";
    }
    
    private async Task UpdateFather(int? curPartOfField, bool curPartOf, Employee author, int? prevPartOfField = null, bool? prevPartOf = null)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        if (prevPartOfField is not null && prevPartOf is not null && prevPartOf.Value && !curPartOf)
        {
            var parts = await _entitiesRepository.GetPartsAsync(prevPartOfField.Value);
            if(!parts.Any())
            {
                await SetFatherOf(prevPartOfField.Value, author, false);
            }
        }
        else if(curPartOf && curPartOfField is not null)
        {
            await SetFatherOf(curPartOfField.Value, author, true);
        }
        scope.Complete();
    }

    private async Task SetFatherOf(int partOfField, Employee author, bool fatherOfValue)
    {
        var father = await _entitiesRepository.GetAsync(partOfField);
        var trType = _transactionTypesRepository.GetAllByPredicate(t => t.Type.ToLower() == "update").First();
        father.FatherOf = fatherOfValue;
        await _entitiesRepository.UpdateAsync(father);
        await _transactionsRepository.CreateTransactionWithEntityAsync(father, trType, author);
    }
    
}