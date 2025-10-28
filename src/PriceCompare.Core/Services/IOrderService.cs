using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using PriceCompare.Core.Contracts;
using PriceCompare.Core.Repositories;

namespace PriceCompare.Core.Services
{
    public interface IOrderService
    {
        Task<DataTable> GetRecentOrdersAsync(int? days = null);
        Task<DataTable> GetOrderDetailsAsync(long orderId);
        Task<DataTable> GetQuoteDataAsync(long orderNum);
        Task<List<OrderResponseModel>> FetchAndSaveOrderDataAsync(DateTime fromDate, DateTime toDate, string accountNumber);
        Task<List<DealerModel>> GetDealersAsync();
        Task<DataTable> SearchDealerOrdersAsync(string? status, long? orderId, string? oracleDealerId, string? dealerName, int? dealerId, DateTime? createdStart, DateTime? createdEnd);
    }
}
