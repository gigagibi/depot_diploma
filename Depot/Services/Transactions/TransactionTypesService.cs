using AutoMapper;
using Depot.API.Transactions.TransactionTypes.Requests;
using Depot.API.Transactions.TransactionTypes.Responses;
using Depot.Models.Transactions;
using Depot.Repositories;
using Depot.Repositories.Transactions;

namespace Depot.Services.Transactions;

public class TransactionTypesService : GenericDictionaryCrudService<TransactionType, ITransactionTypesRepository, TransactionTypeGetResponse, TransactionTypeUpdateRequest, TransactionTypeCreateRequest, TransactionTypeUpdateResponse, TransactionTypeCreateResponse>, ITransactionTypesService
{
    public TransactionTypesService(ITransactionTypesRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}