using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace QLKYTUCXA
{
    public partial class FrmNhanSu : Form
    {
        public FrmNhanSu()
        {
            InitializeComponent();
        }
        SqlConnection cnn = new SqlConnection(KetNoi.ConnectionString);
        SqlDataAdapter daNhanSu;
        DataSet ds = new DataSet("dsNhanSu");
        bool saved = false;
        private void FrmNhanSu_Load(object sender, EventArgs e)
        {
            string select_query = @"select * from NhanVien";
            daNhanSu = new SqlDataAdapter(select_query, cnn);
            daNhanSu.Fill(ds, "tbNhanVien");

            dtgvHienThi.DataSource = ds;
            dtgvHienThi.DataMember = "tbNhanVien";

            SettingDataGridView();

            btnLuu.Enabled = false;

            SettingCommand();

            dtgvHienThi.Columns["MaNV"].HeaderText = "Mã Nhân Viên";
            dtgvHienThi.Columns["HoTen"].HeaderText = "Họ Tên";
            dtgvHienThi.Columns["GioiTinh"].HeaderText = "Giới Tính";
            dtgvHienThi.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            dtgvHienThi.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            dtgvHienThi.Columns["ChucVu"].HeaderText = "Chức Vụ";
            dtgvHienThi.Columns["SoDT"].HeaderText = "Số Điện Thoại";
            dtgvHienThi.Columns["Email"].HeaderText = "Email";
        }

        private void SettingDataGridView()
        {
            dtgvHienThi.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dtgvHienThi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvHienThi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgvHienThi.AllowUserToAddRows = false;
            dtgvHienThi.AllowUserToDeleteRows = false;
        }

        private void SettingCommand()
        {
            //Insert command
            string insert_query = "insert into NhanVien values (@MaNV, @HoTen, @GioiTinh, @NgaySinh, @DiaChi, @ChucVu, @SoDT, @Email)";
            SqlCommand insert_cmd = new SqlCommand(insert_query, cnn);
            insert_cmd.Parameters.Add("@MaNV", SqlDbType.Char, 10, "MaNV");
            insert_cmd.Parameters.Add("@HoTen", SqlDbType.NVarChar, 50, "HoTen");
            insert_cmd.Parameters.Add("@GioiTinh", SqlDbType.NVarChar, 10, "GioiTinh");
            insert_cmd.Parameters.Add("@NgaySinh", SqlDbType.Date, 31, "NgaySinh");
            insert_cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 50, "DiaChi");
            insert_cmd.Parameters.Add("@ChucVu", SqlDbType.NVarChar, 50, "ChucVu");
            insert_cmd.Parameters.Add("@SoDT", SqlDbType.Char, 20, "SoDT");
            insert_cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 50, "Email");

            daNhanSu.InsertCommand = insert_cmd;

            //Update command
            string update_query = "update NhanVien set HoTen=@HoTen, GioiTinh=@GioiTinh, NgaySinh=@NgaySinh, DiaChi=@DiaChi, ChucVu=@ChucVu, SoDT=@SoDT, Email=@Email where MaNV=@MaNV";
            SqlCommand update_cmd = new SqlCommand(update_query, cnn);
            update_cmd.Parameters.Add("@MaNV", SqlDbType.Char, 10, "MaNV");
            update_cmd.Parameters.Add("@HoTen", SqlDbType.NVarChar, 50, "HoTen");
            update_cmd.Parameters.Add("@GioiTinh", SqlDbType.NVarChar, 10, "GioiTinh");
            update_cmd.Parameters.Add("@NgaySinh", SqlDbType.Date, 31, "NgaySinh");
            update_cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 50, "DiaChi");
            update_cmd.Parameters.Add("@ChucVu", SqlDbType.NVarChar, 50, "ChucVu");
            update_cmd.Parameters.Add("@SoDT", SqlDbType.Char, 20, "SoDT");
            update_cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 50, "Email");

            daNhanSu.UpdateCommand = update_cmd;

            //delete command 
            string delete_query = "delete NhanVien where MaNV=@MaNV";
            SqlCommand delete_cmd = new SqlCommand(delete_query, cnn);
            delete_cmd.Parameters.Add("@MaNV", SqlDbType.Char, 10, "MaNV");

            daNhanSu.DeleteCommand = delete_cmd;
        }

        private void dtgvHienThi_Click(object sender, EventArgs e)
        {
            try {
                DataGridViewRow dtgvr = dtgvHienThi.SelectedRows[0];
                txtMaNV.Text = dtgvr.Cells["MaNV"].Value.ToString().Trim();
                txtHoTen.Text = dtgvr.Cells["HoTen"].Value.ToString().Trim();

                if (dtgvr.Cells["GioiTinh"].Value.ToString().Trim() == "Nam")
                {
                    radNam.Checked = true;
                }
                else
                    radNu.Checked = true;

                dateNgaySinh.Text = dtgvr.Cells["NgaySinh"].Value.ToString().Trim();
                txtDiaChi.Text = dtgvr.Cells["DiaChi"].Value.ToString().Trim();
                txtChucVu.Text = dtgvr.Cells["ChucVu"].Value.ToString().Trim();
                txtSoDT.Text = dtgvr.Cells["SoDT"].Value.ToString().Trim();
                txtEmail.Text = dtgvr.Cells["Email"].Value.ToString().Trim();
            }
            catch { return; }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (KiemTraODuLieu(gbThongTin))
            {
                DataRow dr = ds.Tables["tbNhanVien"].NewRow();
                dr["MaNV"] = txtMaNV.Text;
                dr["HoTen"] = txtHoTen.Text;
                dr["GioiTinh"] = radNam.Checked ? "Nam" : "Nữ";
                dr["NgaySinh"] = dateNgaySinh.Text;
                dr["DiaChi"] = txtDiaChi.Text;
                dr["ChucVu"] = txtChucVu.Text;
                dr["SoDT"] = txtSoDT.Text;
                dr["Email"] = txtEmail.Text;

                ds.Tables["tbNhanVien"].Rows.Add(dr);
                MessageBox.Show("Thêm Thành Công", "Thông Báo", MessageBoxButtons.OK);
                btnLuu.Enabled = true;
                saved = false;
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Thêm Thành Công", "Thông Báo", MessageBoxButtons.OKCancel);
            if(rs == DialogResult.OK){
                DataGridViewRow dr = dtgvHienThi.SelectedRows[0];
                dtgvHienThi.Rows.Remove(dr);
                MessageBox.Show("Xóa Thành Công", "Thông Báo", MessageBoxButtons.OK);
                btnLuu.Enabled = true;
                saved = false;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (KiemTraODuLieu(gbThongTin))
            {
                DataGridViewRow dr = dtgvHienThi.SelectedRows[0];
                dtgvHienThi.BeginEdit(true);
                dr.Cells["MaNV"].Value = txtMaNV.Text;
                dr.Cells["HoTen"].Value = txtHoTen.Text;
                dr.Cells["GioiTinh"].Value = radNam.Checked ? "Nam" : "Nữ";
                dr.Cells["NgaySinh"].Value = dateNgaySinh.Text;
                dr.Cells["DiaChi"].Value = txtDiaChi.Text;
                dr.Cells["ChucVu"].Value = txtChucVu.Text;
                dr.Cells["SoDT"].Value = txtSoDT.Text;
                dr.Cells["Email"].Value = txtEmail.Text;
                dtgvHienThi.EndEdit();
                MessageBox.Show("Cập Nhật Thành Công", "Thông Báo", MessageBoxButtons.OK);
                btnLuu.Enabled = true;
                saved = false;
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo");
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                daNhanSu.Update(ds, "tbNhanVien");
                MessageBox.Show("Lưu thành công", "Thông Báo");
                dtgvHienThi.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lưu Không Thành Công Vui Lòng Kiểm Tra Lại", "Thông Báo", MessageBoxButtons.OK);
            }
            saved = true;
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (saved)
            {
                DialogResult rs = MessageBox.Show("Bạn có muốn thoát", "Thông Báo", MessageBoxButtons.OKCancel);
                if (rs == DialogResult.OK)
                {
                    this.Close();
                }
            }
            else
            {
                DialogResult rs = MessageBox.Show("Bạn chưa lưu dữ liệu! Bạn có muốn tiếp tục thoát", "Thông Báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (rs == DialogResult.OK)
                {
                    this.Close();
                }

            }
        }

        private void txtTimMaNV_TextChanged(object sender, EventArgs e)
        {
            DataView dv = ds.Tables["tbNhanVien"].DefaultView;
            dv.RowFilter = string.Format("MaNV LIKE '%{0}%'", txtTimMaNV.Text);
            dtgvHienThi.DataSource = dv;
        }

        private void txtTimHoTen_TextChanged(object sender, EventArgs e)
        {
            DataView dv = ds.Tables["tbNhanVien"].DefaultView;
            dv.RowFilter = string.Format("HoTen LIKE '%{0}%'", txtTimHoTen.Text);
            dtgvHienThi.DataSource = dv;
        }

        private void txtTimDiaChi_TextChanged(object sender, EventArgs e)
        {
            DataView dv = ds.Tables["tbNhanVien"].DefaultView;
            dv.RowFilter = string.Format("DiaChi LIKE '%{0}%'", txtTimDiaChi.Text);
            dtgvHienThi.DataSource = dv;
        }

        private void timChucVu_TextChanged(object sender, EventArgs e)
        {
            DataView dv = ds.Tables["tbNhanVien"].DefaultView;
            dv.RowFilter = string.Format("ChucVu LIKE '%{0}%'", timChucVu.Text);
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
