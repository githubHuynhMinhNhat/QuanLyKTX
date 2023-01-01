using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace QLKYTUCXA
{
    public partial class FrmQuanTri : Form
    {
        public FrmQuanTri()
        {
            InitializeComponent();
        }
        SqlConnection cnn = new SqlConnection(KetNoi.ConnectionString);
        SqlDataAdapter daTaiKhoan;
        DataSet ds = new DataSet("dsQuanTri");
        bool saved = false;
        private void FrmQuanTri_Load(object sender, EventArgs e)
        {
            //Load dữ liệu vào datagridview
            string TaiKhoan_query = "select * from TaiKhoan";
            daTaiKhoan = new SqlDataAdapter(TaiKhoan_query, cnn);
            daTaiKhoan.Fill(ds, "tbTaiKhoan");
            //
            dtgvHienThi.DataSource = ds;
            dtgvHienThi.DataMember = "tbTaiKhoan";
            //
            dtgvHienThi.Columns["Taikhoan"].HeaderText = "Tài Khoản";
            dtgvHienThi.Columns["MatKhau"].HeaderText = "Mật Khẩu";
            dtgvHienThi.Columns["MaNV"].HeaderText = "Mã Nhân Viên";
            dtgvHienThi.Columns["Quyen"].HeaderText = "Quyền Truy Cập";
            //
            SettingDataGridView();
            //
            SettingCommand();
            //
            //cbQuyenHeThong.Items.Add("Admin");
            cbQuyenHeThong.Items.Add("Cơ Bản");
            //
            btnLuu.Enabled = false;
            //
        }

        private void dtgvHienThi_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow dtgvr = dtgvHienThi.SelectedRows[0];
                txtTaiKhoan.Text = dtgvr.Cells["Taikhoan"].Value.ToString().Trim();
                txtMatKhau.Text = dtgvr.Cells["MatKhau"].Value.ToString().Trim();
                txtMaNV.Text = dtgvr.Cells["MaNV"].Value.ToString().Trim();
                cbQuyenHeThong.Text = dtgvr.Cells["Quyen"].Value.ToString().Trim();
            }
            catch
            {
                return;
            }
        }

        private void btnTao_Click(object sender, EventArgs e)
        {
            if (KiemTraODuLieu(gbThongTin))
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["Taikhoan"] = txtTaiKhoan.Text;
                dr["MatKhau"] = txtMatKhau.Text;
                dr["MaNV"] = txtMaNV.Text;
                dr["Quyen"] = cbQuyenHeThong.Text;
                ds.Tables[0].Rows.Add(dr);
                btnLuu.Enabled = true;
                saved = false;
                MessageBox.Show("Tạo Thành Công", "Thông Báo", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo");
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearInput(gbThongTin);
            ClearInput(gbTimKiem);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn Có Muốn Xóa", "Thông Báo", MessageBoxButtons.OKCancel);
            if(rs == DialogResult.OK){
                DataGridViewRow dr = dtgvHienThi.SelectedRows[0];
                dtgvHienThi.Rows.Remove(dr);
                btnLuu.Enabled = true;
                saved = false;
                MessageBox.Show("Xóa Thành Công", "Thông Báo", MessageBoxButtons.OK);

            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {

            if (KiemTraODuLieu(gbThongTin)) {
                DataGridViewRow dr = dtgvHienThi.SelectedRows[0];
                dtgvHienThi.BeginEdit(true);
                dr.Cells["Taikhoan"].Value = txtTaiKhoan.Text;
                dr.Cells["MatKhau"].Value = txtMatKhau.Text;
                dr.Cells["MaNV"].Value = txtMaNV.Text;
                dr.Cells["Quyen"].Value = cbQuyenHeThong.Text;
                dtgvHienThi.EndEdit();
                btnLuu.Enabled = true;
                saved = false;
                MessageBox.Show("Cập Nhật Thành Công", "Thông Báo", MessageBoxButtons.OK);
            }
            else {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo");
            }
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (saved)
            {
                DialogResult rs = MessageBox.Show("Bạn có muốn thoát", "Thông Báo", MessageBoxButtons.OKCancel);
                if(rs == DialogResult.OK)
                {
                    this.Close();
                }
            }
            else
            {
                DialogResult rs = MessageBox.Show("Bạn chưa lưu dữ liệu! Bạn có muốn tiếp tục thoát", "Thông Báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if(rs == DialogResult.OK)
                {
                    this.Close();
                }

            }
        }

        private void SettingDataGridView()
        {
            dtgvHienThi.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dtgvHienThi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvHienThi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgvHienThi.AllowUserToAddRows = false;
            dtgvHienThi.AllowUserToDeleteRows = false;
        }

        private void ClearInput(Control c)
        {
            foreach (Control control in c.Controls)
            {
                if (control is TextBox)
                {
                    TextBox textBox = (TextBox)control;
                    textBox.Clear();
                }
                if (control is ComboBox)
                {
                    ComboBox comboBox = (ComboBox)control;
                    comboBox.Text = "";
                }
            }
        }

        private void SettingCommand()
        {
            //Insert command
            string insert_query = "insert into TaiKhoan values (@TaiKhoan, @MatKhau, @MaNV, @Quyen)";
            SqlCommand insert_cmd = new SqlCommand(insert_query, cnn);
            insert_cmd.Parameters.Add("@TaiKhoan", SqlDbType.Char, 20, "Taikhoan");
            insert_cmd.Parameters.Add("@MatKhau", SqlDbType.Char, 20, "MatKhau");
            insert_cmd.Parameters.Add("@MaNV", SqlDbType.Char, 10, "MaNV");
            insert_cmd.Parameters.Add("@Quyen", SqlDbType.NVarChar, 50, "Quyen");

            daTaiKhoan.InsertCommand = insert_cmd;

            //Update command
            string update_query = "update TaiKhoan set MatKhau=@MatKhau, MaNV=@MaNV, Quyen=@Quyen where Taikhoan=@TaiKhoan";
            SqlCommand update_cmd = new SqlCommand(update_query, cnn);
            update_cmd.Parameters.Add("@TaiKhoan", SqlDbType.Char, 20, "Taikhoan");
            update_cmd.Parameters.Add("@MatKhau", SqlDbType.Char, 20, "MatKhau");
            update_cmd.Parameters.Add("@MaNV", SqlDbType.Char, 10, "MaNV");
            update_cmd.Parameters.Add("@Quyen", SqlDbType.NVarChar, 50, "Quyen");

            daTaiKhoan.UpdateCommand = update_cmd;

            //delete command 
            string delete_query = "delete TaiKhoan where Taikhoan=@TaiKhoan";
            SqlCommand delete_cmd = new SqlCommand(delete_query, cnn);
            delete_cmd.Parameters.Add("@TaiKhoan", SqlDbType.Char, 20, "Taikhoan");

            daTaiKhoan.DeleteCommand = delete_cmd;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                daTaiKhoan.Update(ds, "tbTaiKhoan");
                MessageBox.Show("Lưu thành công", "Thông Báo");
                dtgvHienThi.Refresh();
            }
            catch
            {
                MessageBox.Show("Lưu Không Thành Công Vui Lòng Kiểm Tra Lại", "Thông Báo", MessageBoxButtons.OK);
            }

            saved = true;
        }

        private void txtTimTaiKhoan_TextChanged(object sender, EventArgs e)
        {
            DataView dv = ds.Tables["tbTaiKhoan"].DefaultView;
            dv.RowFilter = string.Format("Taikhoan LIKE '%{0}%'", txtTimTaiKhoan.Text);
            dtgvHienThi.DataSource = dv;
        }

        private void txtTimMaNV_TextChanged(object sender, EventArgs e)
        {
            DataView dv = ds.Tables["tbTaiKhoan"].DefaultView;
            dv.RowFilter = string.Format("MaNV LIKE '%{0}%'", txtTimMaNV.Text);
            dtgvHienThi.DataSource = dv;
        }

        private bool KiemTraODuLieu(GroupBox gb)
        {
            foreach (Control c in gb.Controls)
            {
                if (c.Text == "")
                    return false;
            }
            return true;
        }

    }
}
