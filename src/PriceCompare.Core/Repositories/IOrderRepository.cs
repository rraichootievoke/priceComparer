using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using PriceCompare.Core.Contracts;

namespace PriceCompare.Core.Repositories
{
    public interface IOrderRepository
    {
        Task<DataTable> GetRecentOrderItemDetailsAsync(int? days = null);
        Task<DataTable> GetOrderItemDetailsByIdAsync(long orderId);
        Task<List<OrderResponseModel>> GetPriceFromIStoreAsync(DateTime fromDate, DateTime toDate, string accountNumber);
        Task InsertQuotesAsync(List<OrderResponseModel> quotes);
        Task<DataTable> GetQuoteDataByOrderNumAsync(long orderNum);
        Task<DataTable> SearchDealerOrdersAsync(string? status, long? orderId, string? oracleDealerId, string? dealerName, int? dealerId, DateTime? createdStart, DateTime? createdEnd);
    }
}
