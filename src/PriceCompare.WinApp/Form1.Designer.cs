namespace PriceCompareApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridView1;

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
            dataGridView1 = new DataGridView();
            dgvOrdersList = new DataGridView();
            dataGridView3 = new DataGridView();
            lblDealers = new Label();
            textBox1 = new TextBox();
            button1 = new Button();
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
            richTextBox1 = new RichTextBox();
            lblOrders = new Label();
            splitContainer1 = new SplitContainer();
            label1 = new Label();
            label2 = new Label();
            statusStrip1 = new StatusStrip();
            dgvOrders = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvOrdersList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView3).BeginInit();
            gbSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOrders).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeight = 34;
            dataGridView1.Dock = DockStyle.Bottom;
            dataGridView1.Location = new Point(0, 36);
            dataGridView1.Margin = new Padding(2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(581, 231);
            dataGridView1.TabIndex = 1;
            // 
            // dgvOrdersList
            // 
            dgvOrdersList.AllowUserToOrderColumns = true;
            dgvOrdersList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            dgvOrdersList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOrdersList.Location = new Point(830, 206);
            dgvOrdersList.Margin = new Padding(2);
            dgvOrdersList.Name = "dgvOrdersList";
            dgvOrdersList.RowHeadersWidth = 62;
            dgvOrdersList.Size = new Size(315, 296);
            dgvOrdersList.TabIndex = 2;
            //dgvOrdersList.CellContentClick += dgvOrdersList_CellContentClick;
            // 
            // dataGridView3
            // 
            dataGridView3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView3.Dock = DockStyle.Bottom;
            dataGridView3.Location = new Point(0, 36);
            dataGridView3.Margin = new Padding(2);
            dataGridView3.Name = "dataGridView3";
            dataGridView3.RowHeadersWidth = 62;
            dataGridView3.Size = new Size(549, 231);
            dataGridView3.TabIndex = 3;
            // 
            // lblDealers
            // 
            lblDealers.AutoSize = true;
            lblDealers.Location = new Point(12, 25);
            lblDealers.Margin = new Padding(2, 0, 2, 0);
            lblDealers.Name = "lblDealers";
            lblDealers.Size = new Size(45, 15);
            lblDealers.TabIndex = 4;
            lblDealers.Text = "Dealers";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(489, 108);
            textBox1.Margin = new Padding(2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(127, 23);
            textBox1.TabIndex = 5;
            // 
            // button1
            // 
            button1.Location = new Point(489, 79);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(78, 27);
            button1.TabIndex = 6;
            button1.Text = "Filter";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(368, 22);
            btnSearch.Margin = new Padding(2);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(117, 109);
            btnSearch.TabIndex = 7;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // lblFrom
            // 
            lblFrom.AutoSize = true;
            lblFrom.Location = new Point(22, 57);
            lblFrom.Margin = new Padding(2, 0, 2, 0);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new Size(35, 15);
            lblFrom.TabIndex = 8;
            lblFrom.Text = "From";
            // 
            // lblTo
            // 
            lblTo.AutoSize = true;
            lblTo.Location = new Point(37, 85);
            lblTo.Margin = new Padding(2, 0, 2, 0);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(20, 15);
            lblTo.TabIndex = 9;
            lblTo.Text = "To";
            // 
            // cbDealers
            // 
            cbDealers.FormattingEnabled = true;
            cbDealers.Location = new Point(62, 22);
            cbDealers.Name = "cbDealers";
            cbDealers.Size = new Size(289, 23);
            cbDealers.TabIndex = 10;
            // 
            // dtpFrom
            // 
            dtpFrom.Location = new Point(62, 51);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.Size = new Size(148, 23);
            dtpFrom.TabIndex = 11;
            dtpFrom.Value = new DateTime(2025, 9, 29, 2, 17, 42, 0);
            // 
            // dtpTo
            // 
            dtpTo.Location = new Point(62, 80);
            dtpTo.Name = "dtpTo";
            dtpTo.Size = new Size(148, 23);
            dtpTo.TabIndex = 11;
            // 
            // lblOrderId
            // 
            lblOrderId.AutoSize = true;
            lblOrderId.Location = new Point(12, 111);
            lblOrderId.Margin = new Padding(2, 0, 2, 0);
            lblOrderId.Name = "lblOrderId";
            lblOrderId.Size = new Size(47, 15);
            lblOrderId.TabIndex = 9;
            lblOrderId.Text = "OrderId";
            // 
            // txtOrderId
            // 
            txtOrderId.Location = new Point(62, 108);
            txtOrderId.Name = "txtOrderId";
            txtOrderId.Size = new Size(148, 23);
            txtOrderId.TabIndex = 12;
            txtOrderId.Text = "24269";
            // 
            // btnClear
            // 
            btnClear.Location = new Point(489, 25);
            btnClear.Margin = new Padding(2);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(117, 50);
            btnClear.TabIndex = 13;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += button2_Click_1;
            // 
            // gbSearch
            // 
            gbSearch.Controls.Add(txtOrderId);
            gbSearch.Controls.Add(btnClear);
            gbSearch.Controls.Add(lblDealers);
            gbSearch.Controls.Add(btnSearch);
            gbSearch.Controls.Add(textBox1);
            gbSearch.Controls.Add(button1);
            gbSearch.Controls.Add(lblFrom);
            gbSearch.Controls.Add(dtpTo);
            gbSearch.Controls.Add(lblTo);
            gbSearch.Controls.Add(dtpFrom);
            gbSearch.Controls.Add(lblOrderId);
            gbSearch.Controls.Add(cbDealers);
            gbSearch.Location = new Point(12, 12);
            gbSearch.Name = "gbSearch";
            gbSearch.Size = new Size(621, 144);
            gbSearch.TabIndex = 14;
            gbSearch.TabStop = false;
            // 
            // richTextBox1
            // 
            richTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            richTextBox1.Location = new Point(639, 0);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(518, 156);
            richTextBox1.TabIndex = 15;
            richTextBox1.Text = "";
            // 
            // lblOrders
            // 
            lblOrders.AutoSize = true;
            lblOrders.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblOrders.Location = new Point(12, 174);
            lblOrders.Margin = new Padding(2, 0, 2, 0);
            lblOrders.Name = "lblOrders";
            lblOrders.Size = new Size(75, 30);
            lblOrders.TabIndex = 14;
            lblOrders.Text = "Orders";
            // 
            // splitContainer1
            // 
            splitContainer1.Location = new Point(11, 507);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(dataGridView1);
            splitContainer1.Panel1.Controls.Add(label1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(dataGridView3);
            splitContainer1.Panel2.Controls.Add(label2);
            splitContainer1.Size = new Size(1134, 267);
            splitContainer1.SplitterDistance = 581;
            splitContainer1.TabIndex = 16;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(0, 4);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(138, 30);
            label1.TabIndex = 17;
            label1.Text = "MyDoor Data";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(3, 4);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(115, 30);
            label2.TabIndex = 18;
            label2.Text = "iStore Data";
            // 
            // statusStrip1
            // 
            statusStrip1.Location = new Point(0, 777);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1155, 22);
            statusStrip1.TabIndex = 17;
            statusStrip1.Text = "statusStrip1";
            // 
            // dgvOrders
            // 
            dgvOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOrders.Location = new Point(12, 207);
            dgvOrders.Name = "dgvOrders";
            dgvOrders.Size = new Size(804, 294);
            dgvOrders.TabIndex = 18;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1155, 799);
            Controls.Add(dgvOrders);
            Controls.Add(statusStrip1);
            Controls.Add(splitContainer1);
            Controls.Add(lblOrders);
            Controls.Add(richTextBox1);
            Controls.Add(gbSearch);
            Controls.Add(dgvOrdersList);
            Margin = new Padding(2);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvOrdersList).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView3).EndInit();
            gbSearch.ResumeLayout(false);
            gbSearch.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvOrders).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvOrdersList;
        private DataGridView dataGridView3;
        private Label lblDealers;
        private TextBox textBox1;
        private Button button1;
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
        private StatusStrip statusStrip1;
        private DataGridView dgvOrders;
    }
}
