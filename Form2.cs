using Dapper;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Net;
using System.Text.Json;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PriceCompareApp
{
    public partial class Form2 : Form
    {
        private DateTimePicker dateTimePickerFrom;
        private DateTimePicker dateTimePickerTo;
        private TextBox textBoxInput;
        private Button buttonSubmit;
        private Label labelFrom;
        private Label labelTo;
        private Label labelInput;

        public Form2()
        {
            // Removed InitializeComponent() as it is not required for manually added controls.

            // From Date
            labelFrom = new Label { Text = "From Date:", Left = 20, Top = 20, Width = 80 };
            dateTimePickerFrom = new DateTimePicker { Left = 110, Top = 20, Width = 500 };

            // To Date
            labelTo = new Label { Text = "To Date:", Left = 20, Top = 60, Width = 80 };
            dateTimePickerTo = new DateTimePicker { Left = 110, Top = 60, Width = 500 };

            // Input TextBox
            labelInput = new Label { Text = "Input:", Left = 20, Top = 100, Width = 80 };
            textBoxInput = new TextBox { Left = 110, Top = 100, Width = 500 };

            // Submit Button
            buttonSubmit = new Button { Text = "Submit", Left = 110, Top = 140, Width = 100 };
            buttonSubmit.Click += ButtonSubmit_Click;

            // Add controls to form
            Controls.Add(labelFrom);
            Controls.Add(dateTimePickerFrom);
            Controls.Add(labelTo);
            Controls.Add(dateTimePickerTo);
            Controls.Add(labelInput);
            Controls.Add(textBoxInput);
            Controls.Add(buttonSubmit);

            // Set form size (increased)
            this.Size = new System.Drawing.Size(800, 400);
            this.Text = "Date Range Input";
        }

        private void ButtonSubmit_Click(object sender, EventArgs e)
        {
            //GetDate();
            // Example: handle input
            DateTime inputDate = new DateTime(2025, 10, 28, 15, 59, 38);
            string formatted = inputDate.ToString("dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture).ToUpper();
            
            DateTime fromDate = dateTimePickerFrom.Value;
            DateTime toDate = dateTimePickerTo.Value;

            //string fromDate = dateTimePickerFrom.Value.ToString("dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture).ToUpper();
            //string toDate = dateTimePickerTo.Value.ToString("dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture).ToUpper();

            string inputText = textBoxInput.Text;
            var quotes = GetPricefromIStore(fromDate, toDate, inputText);

            // Fix: Convert IList<QuoteData> to List<QuoteData> before passing to InsertQuotesIntoSql
            if (quotes != null)
            {
                InsertQuotesIntoSql(quotes.ToList());
            }

            //MessageBox.Show($"From: {fromDate:yyyy-MM-dd}\nTo: {toDate:yyyy-MM-dd}\nInput: {inputText}");
        }

        public IList<QuoteData> GetPricefromIStore(DateTime fromdate, DateTime todate, string accountNumber)
        {
            var quotes = new List<QuoteData>();
            try
            {

                


                using var connection = new OracleConnection("Data Source = (DESCRIPTION = (ADDRESS = (PROTOCOL = tcp)(HOST = 129.147.140.210)(PORT = 1521))(ADDRESS = (PROTOCOL = tcp)(HOST = 129.147.140.212)(PORT = 1521))(ADDRESS = (PROTOCOL = tcp)(HOST = 129.147.140.219)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = ebs_TCLO1I)(INSTANCE_NAME = EBSTS11))); User ID = XXBPC_INTF; Password = W#JE2sasp*9+;");
                using var command = new OracleCommand("XCBPC_ISTORE_TRK_DWNLDS_PKG.get_iStore_quote_details1", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Input parameters
                command.Parameters.Add("p_dealer_account_num", OracleDbType.Varchar2).Value = accountNumber;
                command.Parameters.Add("p_start_date", OracleDbType.Varchar2).Value = fromdate.ToString("dd-MMM-yyyy");
                command.Parameters.Add("p_end_date", OracleDbType.Varchar2).Value = todate.ToString("dd-MMM-yyyy");

                // Output parameters
                command.Parameters.Add("X_ERROR_MESSAGE", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.Output;
                command.Parameters.Add("x_result_data", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                connection.Open();

                using var reader = command.ExecuteReader();
                

                while (reader.Read())
                {
                    var quote = new QuoteData
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
                        //IsConfigurationChanged = reader["IS_CONFIGURATION_CHANGED"].ToString() == "Y" ? 1 : 0,
                        // Fix for CS0029: Convert the integer value to a string explicitly
                        IsConfigurationChanged = reader["IS_CONFIGURATION_CHANGED"].ToString() == "Y" ? "1" : "0",
                        IsLinesAddedDeleted = reader["IS_LINES_ADDED_DELETED"].ToString() == "Y" ? "1" : "0"
                    };

                    quotes.Add(quote);
                }

            }
            catch (Exception ex)
            {
                //cdqlogger.Log("GetPAMultipliers - " + customerNumber + " - " + System.DateTime.Now.ToString() + ex.Message, LogMessageTypes.ErrorType);
            }
            return quotes;
        }

        public void InsertQuotesIntoSql(List<QuoteData> quotes)
        {
            using var conn = new SqlConnection("Data Source=10.105.3.4;Initial Catalog=MyDoorTest;Persist Security Info=True;User ID=PortalAdmin;Password=_EDBTestServer160923;Connect Timeout=3600; pooling='true'; Max Pool Size=2000;MultipleActiveResultSets=True;");
            conn.Open();

            foreach (var quote in quotes)
            {
                using var cmd = new SqlCommand("usp_InsertQuoteData", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@MyDoorOrderNum", quote.MyDoorOrderNum);
                cmd.Parameters.AddWithValue("@DealerAccountNum", quote.DealerAccountNum);
                cmd.Parameters.AddWithValue("@QuoteName", quote.QuoteName);
                cmd.Parameters.AddWithValue("@QuoteNumber", quote.QuoteNumber);
                cmd.Parameters.AddWithValue("@LineNo", quote.LineNo);
                cmd.Parameters.AddWithValue("@LineTotalPrice", quote.LineTotalPrice);
                cmd.Parameters.AddWithValue("@LineItemPriceDescription", quote.LineItemPriceDescription);
                cmd.Parameters.AddWithValue("@LineItemConfigDescription", quote.LineItemConfigDescription);
                cmd.Parameters.AddWithValue("@QuoteTotalPrice", quote.QuoteTotalPrice);
                cmd.Parameters.AddWithValue("@IsConfigurationChanged", quote.IsConfigurationChanged);
                cmd.Parameters.AddWithValue("@IsLinesAddedDeleted", quote.IsLinesAddedDeleted);

                cmd.ExecuteNonQuery();
            }
        }

        //public string getdate()
        //{
        //    try
        //    {
        //        using (var connection = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=129.147.140.210)(PORT=1521))(ADDRESS=(PROTOCOL=tcp)(HOST=129.147.140.212)(PORT=1521))(ADDRESS=(PROTOCOL=tcp)(HOST=129.147.140.219)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ebs_TCLO1I)(INSTANCE_NAME=EBSTS11)));User ID=XXBPC_INTF;Password=W#JE2sasp*9+;"))
        //        {
        //            using (var command = new OracleCommand("GetDate()", connection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;

        //                // Input parameters
        //                command.Parameters.Add("v_dealer_num", OracleDbType.Varchar2).Value = accountNumber;
        //                command.Parameters.Add("v_start_date", OracleDbType.Date).Value = fromdate;
        //                command.Parameters.Add("v_end_date", OracleDbType.Date).Value = todate;

        //                // Output parameter: assuming it's a SYS_REFCURSOR
        //                command.Parameters.Add("x_result_data", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

        //                connection.Open();

        //                using (var reader = command.ExecuteReader())
        //                {
        //                    var iStoreQuoteList = new List<QuoteData>();

        //                    while (reader.Read())
        //                    {
        //                        var quote = new QuoteData
        //                        {
        //                            MyDoorOrderNum = reader["MYDOOR_ORDER_NUM"].ToString(),
        //                            DealerAccountNum = reader["DEALER_ACCOUNT_NUM"].ToString(),
        //                            QuoteName = reader["QUOTE_NAME"].ToString(),
        //                            QuoteNumber = reader["QUOTE_NUMBER"].ToString(),
        //                            LineNo = Convert.ToInt32(reader["LINE_NO"]),
        //                            LineTotalPrice = Convert.ToDecimal(reader["LINE_TOTAL_PRICE"]),
        //                            LineItemPriceDescription = reader["LINE_ITEM_PRICE_DESCRIPTION"].ToString(),
        //                            LineItemConfigDescription = reader["LINE_ITEM_CONFIG_DESCRIPTION"].ToString(),
        //                            QuoteTotalPrice = Convert.ToDecimal(reader["QUOTE_TOTAL_PRICE"]),
        //                            IsConfigurationChanged = reader["IS_CONFIGURATION_CHANGED"].ToString() == "Y",
        //                            IsLinesAddedDeleted = reader["IS_LINES_ADDED_DELETED"].ToString() == "Y"
        //                        };

        //                        iStoreQuoteList.Add(quote);
        //                    }

        //                    // You can now pass iStoreQuoteList to your SQL bulk insert method
        //                    InsertQuotesIntoSql(iStoreQuoteList);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error: {ex.Message}");
        //    }
        //}
        // Renamed method to follow PascalCase naming convention and fixed return type issue
        public string GetDate()
        {
            try
            {
                using (var connection = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=129.147.140.210)(PORT=1521))(ADDRESS=(PROTOCOL=tcp)(HOST=129.147.140.212)(PORT=1521))(ADDRESS=(PROTOCOL=tcp)(HOST=129.147.140.219)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ebs_TCLO1I)(INSTANCE_NAME=EBSTS11)));User ID=XXBPC_INTF;Password=W#JE2sasp*9+;"))
                {
                    using (var command = new OracleCommand("XCBPC_ISTORE_TRK_DWNLDS_PKG.get_iStore_quote_details", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Input parameters
                        command.Parameters.Add("v_dealer_num", OracleDbType.Varchar2).Value = "24269"; // Replace with actual value
                        command.Parameters.Add("v_start_date", OracleDbType.Date).Value = "2025-10-01"; // Replace with actual value
                        command.Parameters.Add("v_end_date", OracleDbType.Date).Value = "2025-10-01"; // Replace with actual value

                        // Output parameter: assuming it's a SYS_REFCURSOR
                        command.Parameters.Add("x_result_data", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Assuming the first column contains the date as a string
                                return reader.GetString(0);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            // Return a default value or null if no data is retrieved
            return string.Empty;
        }
    }
}