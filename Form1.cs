using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PriceCompareApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(1200, 800);

            // Change label1 text to "Day"
            label1.Text = "Day";

            // Wire up the CellClick event
            dataGridView2.CellClick += dataGridView2_CellClick;

            // Make columns fill the grid space
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Make both grids use the full width of the form
            dataGridView2.Dock = DockStyle.Top;
            dataGridView3.Dock = DockStyle.Bottom;
            dataGridView2.Width = this.ClientSize.Width;
            dataGridView3.Width = this.ClientSize.Width;
            dataGridView1.Width = this.ClientSize.Width;
            dataGridView1.Height = 200;
            // Set FillWeight after data binding
            dataGridView2.DataBindingComplete += DataGridView2_DataBindingComplete;
        }

        private void DataGridView2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dataGridView2.Columns["ComponentValue"] != null)
                dataGridView2.Columns["ComponentValue"].FillWeight = 300; // Large value for wider column
            if (dataGridView2.Columns["OrderId"] != null)
                dataGridView2.Columns["OrderId"].FillWeight = 50;
            if (dataGridView2.Columns["MyDoorItemListPrice"] != null)
                dataGridView2.Columns["MyDoorItemListPrice"].FillWeight = 50;
            if (dataGridView2.Columns["MyDoorTotalPrice"] != null)
                dataGridView2.Columns["MyDoorTotalPrice"].FillWeight = 50;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            string connectionString = "Data Source=10.105.3.4;Initial Catalog=MyDoorTest;Persist Security Info=True;User ID=PortalAdmin;Password=_EDBTestServer160923;Connect Timeout=3600; pooling='true'; Max Pool Size=2000;MultipleActiveResultSets=True;";
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("usp_GetRecentOrderItemDetails", connection);
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                connection.Open();
                using var reader = command.ExecuteReader();
                var dt = new DataTable();
                dt.Load(reader);
                // Filter for distinct OrderId and first price
                var distinctDt = dt.Clone();
                var seenOrderIds = new HashSet<object>();
                foreach (DataRow row in dt.Rows)
                {
                    var orderId = row["OrderId"];
                    if (!seenOrderIds.Contains(orderId))
                    {
                        distinctDt.ImportRow(row);
                        seenOrderIds.Add(orderId);
                    }
                }
                dataGridView2.DataSource = distinctDt;

                // Set FillWeight for columns
                if (dataGridView2.Columns["ComponentValue"] != null)
                    dataGridView2.Columns["ComponentValue"].FillWeight = 300; // Larger value for wider column
                if (dataGridView2.Columns["OrderId"] != null)
                    dataGridView2.Columns["OrderId"].FillWeight = 50;
                if (dataGridView2.Columns["MyDoorItemListPrice"] != null)
                    dataGridView2.Columns["MyDoorItemListPrice"].FillWeight = 50;
                if (dataGridView2.Columns["MyDoorTotalPrice"] != null)
                    dataGridView2.Columns["MyDoorTotalPrice"].FillWeight = 50;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading order item details: {ex.Message}");
            }
        }

        private DataTable FilterOrderItemDetailsColumns(DataTable source)
        {
            var dt = new DataTable();
            dt.Columns.Add("OrderId", typeof(long));
            dt.Columns.Add("ComponentValue", typeof(string));
            dt.Columns.Add("MyDoorItemListPrice", typeof(string));
            dt.Columns.Add("MyDoorTotalPrice", typeof(string));
            foreach (DataRow row in source.Rows)
            {
                dt.Rows.Add(
                    row.Table.Columns.Contains("OrderId") ? row["OrderId"] : null,
                    row.Table.Columns.Contains("ComponentValue") ? row["ComponentValue"] : (row.Table.Columns.Contains("component_value") ? row["component_value"] : null),
                    row.Table.Columns.Contains("MyDoorItemListPrice") ? row["MyDoorItemListPrice"] : (row.Table.Columns.Contains("MYDOORITEMLISTPRICE") ? row["MYDOORITEMLISTPRICE"] : null),
                    row.Table.Columns.Contains("MyDoorTotalPrice") ? row["MyDoorTotalPrice"] : (row.Table.Columns.Contains("MYDOORTOTAL_PRICE") ? row["MYDOORTOTAL_PRICE"] : null)
                );
            }
            return dt;
        }

        private string SafeGetString(SqlDataReader reader, string columnName)
        {
            int ordinal;
            try { ordinal = reader.GetOrdinal(columnName); }
            catch { return string.Empty; }
            if (reader.IsDBNull(ordinal))
                return string.Empty;

            var value = reader.GetValue(ordinal);
            return value?.ToString() ?? string.Empty;
        }



        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dataGridView2.Rows[e.RowIndex];
                var orderIdObj = row.Cells["OrderId"].Value;
                long orderId = 0;
                if (orderIdObj is long)
                {
                    orderId = (long)orderIdObj;
                }
                else if (orderIdObj is int)
                {
                    orderId = (int)orderIdObj;
                }
                else if (orderIdObj != null && long.TryParse(orderIdObj.ToString(), out long parsedId))
                {
                    orderId = parsedId;
                }
                if (orderId != 0)
                {
                    string connectionString = "Data Source=10.105.3.4;Initial Catalog=MyDoorTest;Persist Security Info=True;User ID=PortalAdmin;Password=_EDBTestServer160923;Connect Timeout=3600; pooling='true'; Max Pool Size=2000;MultipleActiveResultSets=True;";
                    using var connection = new SqlConnection(connectionString);
                    using var command = new SqlCommand("usp_GetOrderItemDetails", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrderId", orderId);
                    try
                    {
                        connection.Open();
                        using var reader = command.ExecuteReader();
                        var dtRaw = new DataTable();
                        dtRaw.Columns.Add("OrderId", typeof(long));
                        dtRaw.Columns.Add("ComponentValue", typeof(string));
                        dtRaw.Columns.Add("MyDoorItemListPrice", typeof(string));
                        dtRaw.Columns.Add("MyDoorTotalPrice", typeof(string));
                        while (reader.Read())
                        {
                            string component_value = SafeGetString(reader, "component_value");
                            string MYDOORITEMLISTPRICE = SafeGetString(reader, "MYDOORITEMLISTPRICE");
                            string MYDOORTOTAL_PRICE = SafeGetString(reader, "MYDOORTOTAL_PRICE");
                            dtRaw.Rows.Add(orderId, component_value, MYDOORITEMLISTPRICE, MYDOORTOTAL_PRICE);
                        }
                        reader.Close();
                        dataGridView1.DataSource = dtRaw;




                        string connectionString1 = "Data Source=10.105.3.4;Initial Catalog=MyDoorTest;Persist Security Info=True;User ID=PortalAdmin;Password=_EDBTestServer160923;Connect Timeout=3600; pooling='true'; Max Pool Size=2000;MultipleActiveResultSets=True;";
                        string query = @"
        SELECT 
            MyDoorOrderNum, 
            LineItemPriceDescription, 
            LineTotalPrice, 
            QuoteTotalPrice 
        FROM dbo.QuoteData 
        WHERE MyDoorOrderNum = @OrderNum";

                        using var connection1 = new SqlConnection(connectionString);
                        using var command1 = new SqlCommand(query, connection1);
                        command1.Parameters.AddWithValue("@OrderNum", orderId);

                        using var adapter = new SqlDataAdapter(command1);
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView3.DataSource = dataTable;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading order item details: {ex.Message}");
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=10.105.3.4;Initial Catalog=MyDoorTest;Persist Security Info=True;User ID=PortalAdmin;Password=_EDBTestServer160923;Connect Timeout=3600; pooling='true'; Max Pool Size=2000;MultipleActiveResultSets=True;";
            string storedProcedureName = "usp_GetRecentOrderItemDetails"; // Or your relevant SP

            // Read number of days from textBoxDays
            int days = 1; // Default to 1 if not valid
            if (!int.TryParse(textBox1.Text, out days) || days < 1)
                days = 1;

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(storedProcedureName, connection);
            command.CommandType = CommandType.StoredProcedure;

            // Add a parameter for days if your SP supports it
            command.Parameters.AddWithValue("@Days", days);

            try
            {
                connection.Open();
                using var reader = command.ExecuteReader();
                var dt = new DataTable();
                dt.Load(reader);

                // Filter for distinct OrderId and first price
                var distinctDt = dt.Clone();
                var seenOrderIds = new HashSet<object>();
                foreach (DataRow row in dt.Rows)
                {
                    var orderId = row["OrderId"];
                    if (!seenOrderIds.Contains(orderId))
                    {
                        distinctDt.ImportRow(row);
                        seenOrderIds.Add(orderId);
                    }
                }
                dataGridView2.DataSource = distinctDt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading order item details: {ex.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var form2 = new Form2();
            form2.ShowDialog(); // or form2.Show();
        }
    }
}