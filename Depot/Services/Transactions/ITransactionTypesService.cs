using AutoMapper;
using Depot.API.Transactions.TransactionTypes.Requests;
using Depot.API.Transactions.TransactionTypes.Responses;
using Depot.Models.Transactions;
using Depot.Repositories;
using Depot.Repositories.Transactions;

namespace Depot.Services.Transactions;

public interface ITransactionTypesService : IGenericDictionaryCrudService<TransactionTypeGetResponse, TransactionTypeUpdateRequest, TransactionTypeCreateRequest, TransactionTypeUpdateResponse, TransactionTypeCreateResponse>
{

}