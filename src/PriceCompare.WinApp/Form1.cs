using Oracle.ManagedDataAccess.Client;
using PriceCompare.Core.Contracts;
using PriceCompare.Core.Helpers;
using PriceCompare.Core.Repositories;
using PriceCompare.Core.Services;
using System.Data;

namespace PriceCompareApp
{
    public partial class Form1 : Form
    {
        private readonly IOrderService _orderService;
        private List<DealerModel> _allDealers = new();
        private bool _suppressDealerFilter;
        private BindingSource _ordersBindingSource = new();
        private DataTable? _ordersTable;

        public Form1()
        {
            InitializeComponent();
            var orderRepository = new OrderRepository();
            _orderService = new OrderService(orderRepository);

            cbDealers.DropDownStyle = ComboBoxStyle.DropDown;
            cbDealers.TextUpdate += CbDealers_TextUpdate;
            cbDealers.KeyDown += CbDealers_KeyDown;
            richTextBox1.TextChanged += OrdersFilter_TextChanged;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await LoadDealersAsync();
        }

        private async Task LoadDealersAsync()
        {
            try
            {
                // Show loading placeholder
                _suppressDealerFilter = true;
                cbDealers.Enabled = false;
                cbDealers.DataSource = null;
                cbDealers.Items.Clear();
                cbDealers.Items.Add("Loading...");
                cbDealers.SelectedIndex = 0;
                cbDealers.Refresh();

                var dealers = await _orderService.GetDealersAsync();
                _allDealers = dealers;

                BindDealers(_allDealers);
                cbDealers.Enabled = true;
                cbDealers.SelectedIndex = -1; // nothing selected after load
                cbDealers.Text = string.Empty; // clear the Loading... text
            }
            catch (Exception ex)
            {
                cbDealers.DataSource = null;
                cbDealers.Items.Clear();
                cbDealers.Items.Add("Load failed");
                cbDealers.SelectedIndex = 0;
                MessageBox.Show($"Failed to load dealers: {ex.Message}");
            }
            finally
            {
                _suppressDealerFilter = false;
            }
        }

        private void BindDealers(List<DealerModel> source, string? typedText = null)
        {
            _suppressDealerFilter = true;
            cbDealers.DataSource = null; // reset binding
            // We project to a new list to avoid modifying original and allow custom display
            var displayList = source
                .Select(d => new DealerDisplay
                {
                    Id = d.Id,
                    OracleDealerId = d.OracleDealerId,
                    CompanyName = d.CompanyName,
                    Display = $"{d.CompanyName} | {d.OracleDealerId} | {d.Id}"
                })
                .OrderBy(d => d.CompanyName)
                .ToList();
            cbDealers.DisplayMember = nameof(DealerDisplay.Display);
            cbDealers.ValueMember = nameof(DealerDisplay.OracleDealerId);
            cbDealers.DataSource = displayList;
            if (!string.IsNullOrEmpty(typedText))
            {
                cbDealers.Text = typedText;
                cbDealers.SelectionStart = typedText.Length;
            }
            _suppressDealerFilter = false;
        }

        private void CbDealers_TextUpdate(object? sender, EventArgs e)
        {
            if (_suppressDealerFilter) return;
            var term = cbDealers.Text?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(term))
            {
                BindDealers(_allDealers);
                return;
            }
            var filtered = _allDealers.Where(d =>
                (d.CompanyName ?? string.Empty).Contains(term, StringComparison.OrdinalIgnoreCase) ||
                (d.OracleDealerId ?? string.Empty).Contains(term, StringComparison.OrdinalIgnoreCase) ||
                d.Id.ToString().Contains(term))
                .ToList();
            BindDealers(filtered, term);
            cbDealers.DroppedDown = true; // keep list open while typing
            // Prevent flicker
            Cursor.Current = Cursors.Default;
        }

        private void CbDealers_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                cbDealers.Text = string.Empty;
                BindDealers(_allDealers, string.Empty);
                cbDealers.DroppedDown = false;
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Enter)
            {
                // trigger search when user presses Enter inside combo
                _ = LoadDealerOrdersAsync();
                e.Handled = true;
            }
        }

        private record DealerDisplay
        {
            public int Id { get; init; }
            public string? OracleDealerId { get; init; }
            public string? CompanyName { get; init; }
            public string Display { get; init; } = string.Empty;
        }

        private async Task LoadDealerOrdersAsync()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                var selected = cbDealers.SelectedItem as DealerDisplay;
                string? oracleDealerId = selected?.OracleDealerId;
                string? dealerName = selected?.CompanyName;
                int? dealerId = selected?.Id;
                long? orderId = null;
                if (long.TryParse(txtOrderId.Text.Trim(), out var oid)) orderId = oid; // user may supply order id
                DateTime? start = dtpFrom.Value.Date;
                DateTime? end = dtpTo.Value.Date;
                var dt = await _orderService.SearchDealerOrdersAsync(status: null, orderId: orderId, oracleDealerId: oracleDealerId, dealerName: dealerName, dealerId: dealerId, createdStart: start, createdEnd: end);
                _ordersTable = dt;
                _ordersBindingSource.DataSource = _ordersTable;
                dgvOrders.DataSource = _ordersBindingSource;
                foreach (DataGridViewColumn col in dgvOrders.Columns) col.SortMode = DataGridViewColumnSortMode.Automatic; // enable sorting
                statusStrip1.Items.Clear();
                statusStrip1.Items.Add($"Records: {dt.Rows.Count}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading dealer orders: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void OrdersFilter_TextChanged(object? sender, EventArgs e)
        {
            if (_ordersTable == null) 
                return;
            var term = richTextBox1.Text.Trim().Replace("'", "''");
            var dv = _ordersTable.DefaultView;
            if (string.IsNullOrEmpty(term))
                dv.RowFilter = string.Empty;
            else 
                dv.RowFilter = $"Convert(OrderId,'System.String') LIKE '%{term}%' OR DealerName LIKE '%{term}%' OR OracleDealerId LIKE '%{term}%' OR FullName LIKE '%{term}%'";
            statusStrip1.Items.Clear();
            statusStrip1.Items.Add($"Filtered: {dv.Count}/{_ordersTable.Rows.Count}");
        }

        private async Task LoadRecentOrdersAsync(int? days = null)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                var distinctDt = await _orderService.GetRecentOrdersAsync(days);
                dgvOrdersList.DataSource = distinctDt;

                // Set FillWeight for columns
                if (dgvOrdersList.Columns["ComponentValue"] != null)
                    dgvOrdersList.Columns["ComponentValue"].FillWeight = 300;
                if (dgvOrdersList.Columns["OrderId"] != null)
                    dgvOrdersList.Columns["OrderId"].FillWeight = 50;
                if (dgvOrdersList.Columns["MyDoorItemListPrice"] != null)
                    dgvOrdersList.Columns["MyDoorItemListPrice"].FillWeight = 50;
                if (dgvOrdersList.Columns["MyDoorTotalPrice"] != null)
                    dgvOrdersList.Columns["MyDoorTotalPrice"].FillWeight = 50;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading order item details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private async Task LoadOrderDetailsAsync(long orderId)
        {
            try
            {
                var dtRaw = await _orderService.GetOrderDetailsAsync(orderId);
                var displayDt = new DataTable();
                displayDt.Columns.Add("OrderId", typeof(long));
                displayDt.Columns.Add("ComponentValue", typeof(string));
                displayDt.Columns.Add("MyDoorItemListPrice", typeof(string));
                displayDt.Columns.Add("MyDoorTotalPrice", typeof(string));

                foreach (DataRow row in dtRaw.Rows)
                {
                    string componentValue = row.Table.Columns.Contains("component_value") ? row["component_value"]?.ToString() : string.Empty;
                    string itemListPrice = row.Table.Columns.Contains("MYDOORITEMLISTPRICE") ? row["MYDOORITEMLISTPRICE"]?.ToString() : string.Empty;
                    string totalPrice = row.Table.Columns.Contains("MYDOORTOTAL_PRICE") ? row["MYDOORTOTAL_PRICE"]?.ToString() : string.Empty;

                    displayDt.Rows.Add(orderId, componentValue, itemListPrice, totalPrice);
                }

                dgvMyDoorData.DataSource = displayDt;

                var quoteData = await _orderService.GetQuoteDataAsync(orderId);
                dgviStoreData.DataSource = quoteData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading order details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                await LoadRecentOrdersAsync();

                btnSearch.Enabled = false;
                Cursor = Cursors.WaitCursor;

                var orderDataList = await _orderService.FetchAndSaveOrderDataAsync(dtpFrom.Value, dtpTo.Value, txtOrderId.Text.Trim());

                if (orderDataList.Count > 0)
                {
                    MessageBox.Show($"Successfully imported {orderDataList.Count} quote records.",
                     "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadRecentOrdersAsync();
                }
                else
                {
                    MessageBox.Show("No data found for the specified criteria.",
                      "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSearch.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtOrderId.Clear();
            dtpFrom.Value = DateTime.Now.AddDays(-30);
            dtpTo.Value = DateTime.Now;
            richTextBox1.Clear();
            _ordersBindingSource.RemoveFilter();
            statusStrip1.Items.Clear();
        }

        private async void dgvOrdersList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvOrdersList.Rows[e.RowIndex];
                var orderIdObj = row.Cells["OrderId"].Value;
                long orderId = 0;

                if (orderIdObj is long longValue)
                {
                    orderId = longValue;
                }
                else if (orderIdObj is int intValue)
                {
                    orderId = intValue;
                }
                else if (orderIdObj != null && long.TryParse(orderIdObj.ToString(), out long parsedId))
                {
                    orderId = parsedId;
                }

                if (orderId != 0)
                {
                    await LoadOrderDetailsAsync(orderId);
                }
            }
        }
    }
}