using Microsoft.AspNetCore.Mvc;
using WePromoLink.DTO;

namespace WePromoLink.Services;

public interface ITransactionService
{
    Task<PaginationList<Transaction>> Get(int? page, int? cant);
    Task<TransactionDetail> GetDetails(string id);

}