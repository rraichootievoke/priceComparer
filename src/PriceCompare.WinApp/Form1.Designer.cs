namespace PriceCompareApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvMyDoorData;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, true.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvMyDoorData = new DataGridView();
            dgvOrdersList = new DataGridView();
            dgviStoreData = new DataGridView();
            lblDealers = new Label();
            btnSearch = new Button();
            lblFrom = new Label();
            lblTo = new Label();
            cbDealers = new ComboBox();
            dtpFrom = new DateTimePicker();
            dtpTo = new DateTimePicker();
            lblOrderId = new Label();
            txtOrderId = new TextBox();
            btnClear = new Button();
            gbSearch = new GroupBox();
            lblDaysRange = new Label();
            dgvOrders = new DataGridView();
            richTextBox1 = new RichTextBox();
            lblOrders = new Label();
            splitContainer1 = new SplitContainer();
            label1 = new Label();
            label2 = new Label();
            statusStrip = new StatusStrip();
            ((System.ComponentModel.ISupportInitialize)dgvMyDoorData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvOrdersList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgviStoreData).BeginInit();
            gbSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOrders).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // dgvMyDoorData
            // 
            dgvMyDoorData.AllowUserToOrderColumns = true;
            dgvMyDoorData.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvMyDoorData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvMyDoorData.ColumnHeadersHeight = 34;
            dgvMyDoorData.Location = new Point(0, 27);
            dgvMyDoorData.Margin = new Padding(2);
            dgvMyDoorData.Name = "dgvMyDoorData";
            dgvMyDoorData.RowHeadersWidth = 62;
            dgvMyDoorData.Size = new Size(641, 162);
            dgvMyDoorData.TabIndex = 1;
            dgvMyDoorData.CellClick += dgvOrdersList_CellClick;
            // 
            // dgvOrdersList
            // 
            dgvOrdersList.AllowUserToOrderColumns = true;
            dgvOrdersList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvOrdersList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOrdersList.Location = new Point(9, 163);
            dgvOrdersList.Margin = new Padding(2);
            dgvOrdersList.Name = "dgvOrdersList";
            dgvOrdersList.RowHeadersWidth = 62;
            dgvOrdersList.Size = new Size(488, 389);
            dgvOrdersList.TabIndex = 2;
            dgvOrdersList.CellClick += dgvOrdersList_CellClick;
            // 
            // dgviStoreData
            // 
            dgviStoreData.AllowUserToOrderColumns = true;
            dgviStoreData.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgviStoreData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgviStoreData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgviStoreData.Location = new Point(0, 27);
            dgviStoreData.Margin = new Padding(2);
            dgviStoreData.Name = "dgviStoreData";
            dgviStoreData.RowHeadersWidth = 62;
            dgviStoreData.Size = new Size(641, 165);
            dgviStoreData.TabIndex = 3;
            // 
            // lblDealers
            // 
            lblDealers.AutoSize = true;
            lblDealers.Location = new Point(25, 25);
            lblDealers.Margin = new Padding(2, 0, 2, 0);
            lblDealers.Name = "lblDealers";
            lblDealers.Size = new Size(45, 15);
            lblDealers.TabIndex = 4;
            lblDealers.Text = "Dealers";
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(409, 22);
            btnSearch.Margin = new Padding(2);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(113, 52);
            btnSearch.TabIndex = 7;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // lblFrom
            // 
            lblFrom.AutoSize = true;
            lblFrom.Location = new Point(3, 54);
            lblFrom.Margin = new Padding(2, 0, 2, 0);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new Size(67, 15);
            lblFrom.TabIndex = 8;
            lblFrom.Text = "Date Range";
            // 
            // lblTo
            // 
            lblTo.AutoSize = true;
            lblTo.Location = new Point(195, 55);
            lblTo.Margin = new Padding(2, 0, 2, 0);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(20, 15);
            lblTo.TabIndex = 9;
            lblTo.Text = "To";
            // 
            // cbDealers
            // 
            cbDealers.FormattingEnabled = true;
            cbDealers.Location = new Point(75, 22);
            cbDealers.Name = "cbDealers";
            cbDealers.Size = new Size(314, 23);
            cbDealers.TabIndex = 10;
            // 
            // dtpFrom
            // 
            dtpFrom.Location = new Point(75, 51);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.Size = new Size(120, 23);
            dtpFrom.TabIndex = 11;
            dtpFrom.Value = new DateTime(2025, 9, 29, 2, 17, 42, 0);
            dtpFrom.ValueChanged += dtpFrom_ValueChanged;
            // 
            // dtpTo
            // 
            dtpTo.Location = new Point(215, 51);
            dtpTo.Name = "dtpTo";
            dtpTo.Size = new Size(120, 23);
            dtpTo.TabIndex = 11;
            dtpTo.ValueChanged += dtpTo_ValueChanged;
            // 
            // lblOrderId
            // 
            lblOrderId.AutoSize = true;
            lblOrderId.Location = new Point(23, 83);
            lblOrderId.Margin = new Padding(2, 0, 2, 0);
            lblOrderId.Name = "lblOrderId";
            lblOrderId.Size = new Size(47, 15);
            lblOrderId.TabIndex = 9;
            lblOrderId.Text = "OrderId";
            // 
            // txtOrderId
            // 
            txtOrderId.Location = new Point(75, 80);
            txtOrderId.Name = "txtOrderId";
            txtOrderId.Size = new Size(148, 23);
            txtOrderId.TabIndex = 12;
            txtOrderId.Text = "24269";
            // 
            // btnClear
            // 
            btnClear.Location = new Point(526, 22);
            btnClear.Margin = new Padding(2);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(117, 50);
            btnClear.TabIndex = 13;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // gbSearch
            // 
            gbSearch.Controls.Add(lblDaysRange);
            gbSearch.Controls.Add(txtOrderId);
            gbSearch.Controls.Add(btnClear);
            gbSearch.Controls.Add(lblDealers);
            gbSearch.Controls.Add(btnSearch);
            gbSearch.Controls.Add(lblFrom);
            gbSearch.Controls.Add(dtpTo);
            gbSearch.Controls.Add(dtpFrom);
            gbSearch.Controls.Add(lblOrderId);
            gbSearch.Controls.Add(cbDealers);
            gbSearch.Controls.Add(lblTo);
            gbSearch.Location = new Point(12, 12);
            gbSearch.Name = "gbSearch";
            gbSearch.Size = new Size(648, 115);
            gbSearch.TabIndex = 14;
            gbSearch.TabStop = false;
            // 
            // lblDaysRange
            // 
            lblDaysRange.AutoSize = true;
            lblDaysRange.Location = new Point(340, 55);
            lblDaysRange.Margin = new Padding(2, 0, 2, 0);
            lblDaysRange.Name = "lblDaysRange";
            lblDaysRange.Size = new Size(52, 15);
            lblDaysRange.TabIndex = 14;
            lblDaysRange.Text = "360 days";
            // 
            // dgvOrders
            // 
            dgvOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOrders.Location = new Point(1032, 105);
            dgvOrders.Name = "dgvOrders";
            dgvOrders.Size = new Size(69, 34);
            dgvOrders.TabIndex = 18;
            // 
            // richTextBox1
            // 
            richTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            richTextBox1.Location = new Point(666, 21);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(477, 135);
            richTextBox1.TabIndex = 15;
            richTextBox1.Text = "";
            // 
            // lblOrders
            // 
            lblOrders.AutoSize = true;
            lblOrders.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblOrders.Location = new Point(9, 132);
            lblOrders.Margin = new Padding(2, 0, 2, 0);
            lblOrders.Name = "lblOrders";
            lblOrders.Size = new Size(75, 30);
            lblOrders.TabIndex = 14;
            lblOrders.Text = "Orders";
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.Location = new Point(502, 163);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(dgvMyDoorData);
            splitContainer1.Panel1.Controls.Add(label1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(dgviStoreData);
            splitContainer1.Panel2.Controls.Add(label2);
            splitContainer1.Size = new Size(641, 389);
            splitContainer1.SplitterDistance = 191;
            splitContainer1.TabIndex = 16;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(0, 4);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(103, 21);
            label1.TabIndex = 17;
            label1.Text = "MyDoor Data";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(3, 4);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(86, 21);
            label2.TabIndex = 18;
            label2.Text = "iStore Data";
            // 
            // statusStrip
            // 
            statusStrip.Location = new Point(0, 570);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(1155, 22);
            statusStrip.TabIndex = 17;
            statusStrip.Text = "statusStrip1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1155, 592);
            Controls.Add(statusStrip);
            Controls.Add(splitContainer1);
            Controls.Add(lblOrders);
            Controls.Add(richTextBox1);
            Controls.Add(gbSearch);
            Controls.Add(dgvOrdersList);
            Controls.Add(dgvOrders);
            Margin = new Padding(2);
            Name = "Form1";
            Text = "Clopay :|: Price Validator Tool";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvMyDoorData).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvOrdersList).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgviStoreData).EndInit();
            gbSearch.ResumeLayout(false);
            gbSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOrders).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvOrdersList;
        private DataGridView dgviStoreData;
        private Label lblDealers;
        private Button btnSearch;
        private Label lblFrom;
        private Label lblTo;
        private ComboBox cbDealers;
        private DateTimePicker dtpFrom;
        private DateTimePicker dtpTo;
        private Label lblOrderId;
        private TextBox txtOrderId;
        private Button btnClear;
        private GroupBox gbSearch;
        private RichTextBox richTextBox1;
        private Label lblOrders;
        private SplitContainer splitContainer1;
        private Label label1;
        private Label label2;
        private StatusStrip statusStrip;
        private DataGridView dgvOrders;
        private Label lblDaysRange;
    }
}
