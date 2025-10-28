using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCompareApp
{
    //using Microsoft.Data.SqlClient;
    using Oracle.ManagedDataAccess.Client;
    using System.Data.SqlClient;
    using static System.Runtime.InteropServices.JavaScript.JSType;

    public class DataTransferService
    {
        private readonly string _oracleConnStr;
        private readonly string _sqlConnStr;

        public DataTransferService(string oracleConnStr, string sqlConnStr)
        {
            _oracleConnStr = oracleConnStr;
            _sqlConnStr = sqlConnStr;
        }

        public void TransferData()
        {
            //using var conn = new OracleConnection(_oracleConnStr);
            //using var cmd = new OracleCommand("XCBPC_MYDOOR_PKG.CREATE_ORDER", conn)
            //{
            //    CommandType = System.Data.CommandType.StoredProcedure
            //};

            //// Input parameters
            //cmd.Parameters.Add("P_FROM_DATE", OracleDbType.Date).Value = fromDate;
            //cmd.Parameters.Add("P_TO_DATE", OracleDbType.Date).Value = toDate;
            //cmd.Parameters.Add("P_DEALER_ACCOUNT_NUM", OracleDbType.Varchar2).Value = dealerAccountNum;

            //// Optional: Output parameter
            //cmd.Parameters.Add("P_STATUS", OracleDbType.Varchar2, 100).Direction = System.Data.ParameterDirection.Output;

            //conn.Open();
            //cmd.ExecuteNonQuery();

            //string status = cmd.Parameters["P_STATUS"].Value?.ToString();
            //Console.WriteLine($"Order creation status: {status}");
        
        }

        private void InsertIntoSqlServer(QuoteData quote)
        {
            using var sqlConn = new SqlConnection(_sqlConnStr);
            using var sqlCmd = new SqlCommand("usp_InsertQuoteData", sqlConn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            sqlCmd.Parameters.AddWithValue("@MyDoorOrderNum", quote.MyDoorOrderNum);
            sqlCmd.Parameters.AddWithValue("@DealerAccountNum", quote.DealerAccountNum);
            sqlCmd.Parameters.AddWithValue("@QuoteName", quote.QuoteName);
            sqlCmd.Parameters.AddWithValue("@QuoteNumber", quote.QuoteNumber);
            sqlCmd.Parameters.AddWithValue("@LineNo", quote.LineNo);
            sqlCmd.Parameters.AddWithValue("@LineTotalPrice", quote.LineTotalPrice);
            sqlCmd.Parameters.AddWithValue("@LineItemPriceDescription", quote.LineItemPriceDescription);
            sqlCmd.Parameters.AddWithValue("@LineItemConfigDescription", quote.LineItemConfigDescription);
            sqlCmd.Parameters.AddWithValue("@QuoteTotalPrice", quote.QuoteTotalPrice);
            sqlCmd.Parameters.AddWithValue("@IsConfigurationChanged", quote.IsConfigurationChanged);
            sqlCmd.Parameters.AddWithValue("@IsLinesAddedDeleted", quote.IsLinesAddedDeleted);

            sqlConn.Open();
            sqlCmd.ExecuteNonQuery();
        }
    }

}
