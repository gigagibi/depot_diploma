using AutoMapper;
using Depot.API;
using Depot.API.Transactions.Transaction.Responses;
using Depot.Repositories.Transactions;

namespace Depot.Services.Transactions;

public class TransactionsService : ITransactionsService
{
    private readonly ITransactionsRepository _transactionsRepository;
    private readonly IMapper _mapper;

    public TransactionsService(ITransactionsRepository transactionsRepository, IMapper mapper)
    {
        _transactionsRepository = transactionsRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TransactionGetResponse>> GetAllAsync(PaginationParameters? paginationParameters = null)
    {
        var transactions = await _transactionsRepository.GetAllAsync(paginationParameters);
        return _mapper.Map<IEnumerable<TransactionGetResponse>>(transactions);
    }

    public async Task<TransactionGetResponse> GetAsync(int id)
    {
        var transaction = await _transactionsRepository.GetAsync(id);
        return _mapper.Map<TransactionGetResponse>(transaction);
    }

    public async Task<IEnumerable<TransactionGetResponse>> GetByEntityIdAsync(int entityId, PaginationParameters? paginationParameters = null)
    {
        var transactions = _transactionsRepository.GetAllByPredicate(t => t.EntityId == entityId, paginationParameters);
        return _mapper.Map<IEnumerable<TransactionGetResponse>>(transactions);
    }

    public async Task<IEnumerable<TransactionGetResponse>> GetByEntityInvNumberAsync(int invNumber, PaginationParameters? paginationParameters = null)
    {
        var transactions = _transactionsRepository.GetAllByPredicate(t => t.Entity.InvNumber == invNumber, paginationParameters);
        return _mapper.Map<IEnumerable<TransactionGetResponse>>(transactions);
    }
}