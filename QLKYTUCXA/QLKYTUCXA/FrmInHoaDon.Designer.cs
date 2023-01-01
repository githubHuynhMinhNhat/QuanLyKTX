namespace QLKYTUCXA
{
    partial class FrmInHoaDon
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInHoaDon));
            this.btnThoát = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.dtgvHienThi = new System.Windows.Forms.DataGridView();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.csTieuThu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.donGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.thanhTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnPrintPreview = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSumPrice = new System.Windows.Forms.TextBox();
            this.printHoaDon = new System.Drawing.Printing.PrintDocument();
            this.printHoaDonPreview = new System.Windows.Forms.PrintPreviewDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvHienThi)).BeginInit();
            // 
            // btnThoát
            // 
            this.btnThoát.Location = new System.Drawing.Point(850, 611);
            this.btnThoát.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnThoát.Name = "btnThoát";
            this.btnThoát.Size = new System.Drawing.Size(112, 34);
            this.btnThoát.TabIndex = 0;
            this.btnThoát.Text = "Thoát";
            this.btnThoát.UseVisualStyleBackColor = true;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(606, 611);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(112, 34);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "In";
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // dtgvHienThi
            // 
            this.dtgvHienThi.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgvHienThi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvHienThi.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Title,
            this.csTieuThu,
            this.donGia,
            this.thanhTien});
            this.dtgvHienThi.Location = new System.Drawing.Point(20, 250);
            this.dtgvHienThi.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtgvHienThi.Name = "dtgvHienThi";
            this.dtgvHienThi.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgvHienThi.Size = new System.Drawing.Size(942, 219);
            this.dtgvHienThi.TabIndex = 2;
            // 
            // Title
            // 
            this.Title.HeaderText = "";
            this.Title.Name = "Title";
            // 
            // csTieuThu
            // 
            this.csTieuThu.HeaderText = "Chỉ Số Tiêu Thụ";
            this.csTieuThu.Name = "csTieuThu";
            // 
            // donGia
            // 
            this.donGia.HeaderText = "Đơn Giá";
            this.donGia.Name = "donGia";
            // 
            // thanhTien
            // 
            this.thanhTien.HeaderText = "Thành Tiền";
            this.thanhTien.Name = "thanhTien";
            // 
            // btnPrintPreview
            // 
            this.btnPrintPreview.Location = new System.Drawing.Point(728, 611);
            this.btnPrintPreview.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPrintPreview.Name = "btnPrintPreview";
            this.btnPrintPreview.Size = new System.Drawing.Size(112, 34);
            this.btnPrintPreview.TabIndex = 3;
            this.btnPrintPreview.Text = "Xem";
            this.btnPrintPreview.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(724, 483);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 19);
            this.label1.TabIndex = 4;
            this.label1.Text = "Tổng Cộng";
            // 
            // txtSumPrice
            // 
            this.txtSumPrice.Location = new System.Drawing.Point(809, 480);
            this.txtSumPrice.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSumPrice.Name = "txtSumPrice";
            this.txtSumPrice.Size = new System.Drawing.Size(148, 26);
            this.txtSumPrice.TabIndex = 5;
            // 
            // printHoaDon
            // 
            // 
            // printHoaDonPreview
            // 
            this.printHoaDonPreview.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printHoaDonPreview.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printHoaDonPreview.ClientSize = new System.Drawing.Size(400, 300);
            this.printHoaDonPreview.Document = this.printHoaDon;
            this.printHoaDonPreview.Enabled = true;
            this.printHoaDonPreview.Icon = ((System.Drawing.Icon)(resources.GetObject("printHoaDonPreview.Icon")));
            this.printHoaDonPreview.Name = "printHoaDonPreview";
            this.printHoaDonPreview.UseAntiAlias = true;
            this.printHoaDonPreview.Visible = false;
            // 
            // FrmInHoaDon
            // 

        }

        #endregion

        private System.Windows.Forms.Button btnThoát;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.DataGridView dtgvHienThi;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn csTieuThu;
        private System.Windows.Forms.DataGridViewTextBoxColumn donGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn thanhTien;
        private System.Windows.Forms.Button btnPrintPreview;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSumPrice;
        private System.Drawing.Printing.PrintDocument printHoaDon;
        private System.Windows.Forms.PrintPreviewDialog printHoaDonPreview;
    }
}