using Dapper;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Data.SqlClient;
using PriceCompare.Core.Helpers;
using PriceCompare.Core.Contracts;
using System.Linq; // for parameter enumeration
using System.Diagnostics; // for Debug logging

namespace PriceCompare.Core.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _sqlConnectionString;
        private readonly string _oracleConnectionString;
        private readonly string _dealerPortalConnectionString;

        public OrderRepository()
        {
            _sqlConnectionString = ConfigurationHelper.SqlServerConnectionString;
            _oracleConnectionString = ConfigurationHelper.OracleConnectionString;
            _dealerPortalConnectionString = ConfigurationHelper.DealerPortalDbConnectionString;
        }

        public async Task<DataTable> GetRecentOrderItemDetailsAsync(int? days = null)
        {
            using var connection = new SqlConnection(_sqlConnectionString);
            var parameters = new DynamicParameters();

            if (days.HasValue)
            {
                parameters.Add("@Days", days.Value);
            }

            var reader = await connection.ExecuteReaderAsync(ConfigurationHelper.GetStoredProcedure("GetRecentOrderItemDetails"), parameters, commandType: CommandType.StoredProcedure);

            var dt = new DataTable();
            dt.Load(reader);
            return dt;
        }

        public async Task<DataTable> GetOrderItemDetailsByIdAsync(long orderId)
        {
            using var connection = new SqlConnection(_sqlConnectionString);
            var parameters = new DynamicParameters();
            parameters.Add("@OrderId", orderId);

            var reader = await connection.ExecuteReaderAsync(ConfigurationHelper.GetStoredProcedure("GetOrderItemDetails"), parameters, commandType: CommandType.StoredProcedure);

            var dt = new DataTable();
            dt.Load(reader);
            return dt;
        }

        public async Task<DataTable> GetQuoteDataByOrderNumAsync(long orderNum)
        {
            using var connection = new SqlConnection(_sqlConnectionString);
            var parameters = new DynamicParameters();
            parameters.Add("@OrderNum", orderNum);

            var reader = await connection.ExecuteReaderAsync(ConfigurationHelper.GetSqlQuery("GetQuoteDataByOrderNum"), parameters, commandType: CommandType.Text);

            var dt = new DataTable();
            dt.Load(reader);
            return dt;
        }

        public async Task<DataTable> SearchDealerOrdersAsync(string? status, long? orderId, string? oracleDealerId, string? dealerName, int? dealerId, DateTime? createdStart, DateTime? createdEnd)
        {
            using var connection = new SqlConnection(_sqlConnectionString);
            var parameters = new DynamicParameters();
            parameters.Add("@Status", status);
            parameters.Add("@OrderId", orderId);
            parameters.Add("@OracleDealerId", oracleDealerId);
            parameters.Add("@DealerName", dealerName);
            parameters.Add("@DealerId", dealerId);
            parameters.Add("@CreatedOnStart", createdStart);
            parameters.Add("@CreatedOnEnd", createdEnd);

            var sql = ConfigurationHelper.GetSqlQuery("DealerOrdersSearch");

#if DEBUG
            Debug.WriteLine("[DealerOrdersSearch] Executing SQL:\n" + sql);
            Debug.WriteLine("[DealerOrdersSearch] Parameters:");
            foreach (var name in parameters.ParameterNames)
            {
                object? val = null;
                try { val = parameters.Get<object>(name); } catch { }
                Debug.WriteLine($" {name} = {(val ?? "<null>")}");
            }
#endif
            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new InvalidOperationException("DealerOrdersSearch SQL not found or empty. Ensure queries.json is copied to output and loaded.");
            }

            var reader = await connection.ExecuteReaderAsync(sql, parameters, commandType: CommandType.Text);

            var dt = new DataTable();
            dt.Load(reader);
            return dt;
        }

        public async Task<List<OrderResponseModel>> GetPriceFromIStoreAsync(DateTime fromDate, DateTime toDate, string accountNumber)
        {
            var orderDataList = new List<OrderResponseModel>();

            try
            {
                using var connection = new OracleConnection(_oracleConnectionString);
                await connection.OpenAsync();

                using var command = new OracleCommand(ConfigurationHelper.GetStoredProcedure("OracleGetIStoreQuoteDetails1"), connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add("p_dealer_account_num", OracleDbType.Varchar2).Value = accountNumber;
                command.Parameters.Add("p_start_date", OracleDbType.Varchar2).Value = fromDate.ToString("dd-MMM-yyyy");
                command.Parameters.Add("p_end_date", OracleDbType.Varchar2).Value = toDate.ToString("dd-MMM-yyyy");
                command.Parameters.Add("X_ERROR_MESSAGE", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.Output;
                command.Parameters.Add("x_result_data", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var orderData = new OrderResponseModel
                    {
                        MyDoorOrderNum = reader["MYDOOR_ORDER_NUM"].ToString(),
                        DealerAccountNum = accountNumber,
                        QuoteName = reader["QUOTE_NAME"].ToString(),
                        QuoteNumber = reader["QUOTE_NUMBER"].ToString(),
                        LineNo = reader["LINE_NO"].ToString(),
                        LineTotalPrice = reader["LINE_TOTAL_PRICE"].ToString(),
                        LineItemPriceDescription = reader["LINE_ITEM_PRICE_DESCRIPTION"].ToString(),
                        LineItemConfigDescription = reader["LINE_ITEM_CONFIG_DESCRIPTION"].ToString(),
                        QuoteTotalPrice = reader["QUOTE_TOTAL_PRICE"].ToString(),
                        IsConfigurationChanged = reader["IS_CONFIGURATION_CHANGED"].ToString() == "Y" ? "1" : "0",
                        IsLinesAddedDeleted = reader["IS_LINES_ADDED_DELETED"].ToString() == "Y" ? "1" : "0"
                    };

                    orderDataList.Add(orderData);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error fetching price from iStore: {ex.Message}", ex);
            }

            return orderDataList;
        }

        public async Task InsertQuotesAsync(List<OrderResponseModel> quotes)
        {
            using var connection = new SqlConnection(_sqlConnectionString);
            await connection.OpenAsync();

            foreach (var quote in quotes)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@MyDoorOrderNum", quote.MyDoorOrderNum);
                parameters.Add("@DealerAccountNum", quote.DealerAccountNum);
                parameters.Add("@QuoteName", quote.QuoteName);
                parameters.Add("@QuoteNumber", quote.QuoteNumber);
                parameters.Add("@LineNo", quote.LineNo);
                parameters.Add("@LineTotalPrice", quote.LineTotalPrice);
                parameters.Add("@LineItemPriceDescription", quote.LineItemPriceDescription);
                parameters.Add("@LineItemConfigDescription", quote.LineItemConfigDescription);
                parameters.Add("@QuoteTotalPrice", quote.QuoteTotalPrice);
                parameters.Add("@IsConfigurationChanged", quote.IsConfigurationChanged);
                parameters.Add("@IsLinesAddedDeleted", quote.IsLinesAddedDeleted);

                await connection.ExecuteAsync(ConfigurationHelper.GetStoredProcedure("InsertQuoteData"), parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
