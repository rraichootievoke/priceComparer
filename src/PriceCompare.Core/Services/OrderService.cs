using PriceCompare.Core.Contracts;
using PriceCompare.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using PriceCompare.Core.Helpers;

namespace PriceCompare.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<DataTable> GetRecentOrdersAsync(int? days = null)
        {
            var dt = await _orderRepository.GetRecentOrderItemDetailsAsync(days);

            var distinctDt = dt.Clone();
            var seenOrderIds = new HashSet<object>();

            foreach (DataRow row in dt.Rows)
            {
                var orderId = row["OrderId"]; // assumes column exists from stored proc
                if (!seenOrderIds.Contains(orderId))
                {
                    distinctDt.ImportRow(row);
                    seenOrderIds.Add(orderId);
                }
            }

            return distinctDt;
        }

        public async Task<DataTable> GetOrderDetailsAsync(long orderId)
        {
            return await _orderRepository.GetOrderItemDetailsByIdAsync(orderId);
        }

        public async Task<DataTable> GetQuoteDataAsync(long orderNum)
        {
            return await _orderRepository.GetQuoteDataByOrderNumAsync(orderNum);
        }

        public async Task<List<OrderResponseModel>> FetchAndSaveOrderDataAsync(DateTime fromDate, DateTime toDate, string accountNumber)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                throw new ArgumentException("Account number cannot be empty", nameof(accountNumber));
            }

            var orderDataList = await _orderRepository.GetPriceFromIStoreAsync(fromDate, toDate, accountNumber);

            if (orderDataList.Count >0)
            {
                await _orderRepository.InsertQuotesAsync(orderDataList);
            }

            return orderDataList;
        }

        public async Task<List<DealerModel>> GetDealersAsync()
        {
            var dealers = new List<DealerModel>();
            using var conn = new SqlConnection(ConfigurationHelper.DealerPortalDbConnectionString);
            await conn.OpenAsync();
            using var cmd = new SqlCommand("Select ID, OracleDealerId, CompanyName from dbo.Dealer Where Active = 1", conn);
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                dealers.Add(new DealerModel
                {
                    Id = reader.GetInt32(reader.GetOrdinal("ID")),
                    OracleDealerId = reader.IsDBNull(reader.GetOrdinal("OracleDealerId")) ? null : reader.GetString(reader.GetOrdinal("OracleDealerId")),
                    CompanyName = reader.IsDBNull(reader.GetOrdinal("CompanyName")) ? null : reader.GetString(reader.GetOrdinal("CompanyName"))
                });
            }
            return dealers;
        }

        public async Task<DataTable> SearchDealerOrdersAsync(string? status, long? orderId, string? oracleDealerId, string? dealerName, int? dealerId, DateTime? createdStart, DateTime? createdEnd)
        {
            return await _orderRepository.SearchDealerOrdersAsync(status, orderId, oracleDealerId, dealerName, dealerId, createdStart, createdEnd);
        }
    }
}
