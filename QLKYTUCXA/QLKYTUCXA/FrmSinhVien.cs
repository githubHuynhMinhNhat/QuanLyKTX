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
    public partial class FrmSinhVien : Form
    {
        public FrmSinhVien()
        {
            InitializeComponent();
        }

        SqlConnection cnn = new SqlConnection(KetNoi.ConnectionString);
        SqlDataAdapter daSinhVien;
        SqlDataAdapter daPhong;
        DataSet ds = new DataSet("dsThongTin");
        bool saved = false;

        private void FrmSinhVien_Load(object sender, EventArgs e)
        {
            //LoadCombobox
            string get_phong = @"select * from Phong";
            daPhong = new SqlDataAdapter(get_phong, cnn);
            daPhong.Fill(ds, "tbPhong");
            cbbPhongKTX.DataSource = ds.Tables["tbPhong"];
            cbbPhongKTX.ValueMember = "Maphong";
            cbbPhongKTX.DisplayMember = "Maphong";

            //Load dữ liệu lên datagridview
            SettingDataGridView();

            //
            daSinhVienUpdate();
            //
            btnLuu.Enabled = false;

        }

        #region Hàm Tự Xây Dựng
        private void SettingDataGridView()
        {
            string get_SinhVien = @"select * from SinhVien";
            daSinhVien = new SqlDataAdapter(get_SinhVien, cnn);
            daSinhVien.Fill(ds, "tbSinhVien");
            dtgvHienThi.DataSource = ds.Tables["tbSinhVien"];

            //
            dtgvHienThi.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dtgvHienThi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvHienThi.AllowUserToAddRows = false;

            //
            dtgvHienThi.Columns["MaSV"].HeaderText = "Mã Sinh Viên";
            dtgvHienThi.Columns["HoTen"].HeaderText = "Họ Tên";
            dtgvHienThi.Columns["GioiTinh"].HeaderText = "Giới Tính";
            dtgvHienThi.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            dtgvHienThi.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            dtgvHienThi.Columns["MaLop"].HeaderText = "Lớp";
            dtgvHienThi.Columns["SoDT"].HeaderText = "Số Điện Thoại";
            dtgvHienThi.Columns["Email"].HeaderText = "Email";
            dtgvHienThi.Columns["Maphong"].HeaderText = "Phòng";
        }

        private void daSinhVienUpdate()
        {
            //insert command
            string insert_query = @"insert into SinhVien values (@MaSV, @HoTen, @GioiTinh, @NgaySinh, @DiaChi, @MaLop, @SoDT, @Email, @MaPhong)";
            SqlCommand insert_cmd = new SqlCommand(insert_query, cnn);
            insert_cmd.Parameters.Add("@MaSV", SqlDbType.Char, 10, "MaSV");
            insert_cmd.Parameters.Add("@HoTen", SqlDbType.NVarChar, 50, "HoTen");
            insert_cmd.Parameters.Add("@GioiTinh", SqlDbType.NVarChar, 10, "GioiTinh");
            insert_cmd.Parameters.Add("@NgaySinh", SqlDbType.Date, 31, "NgaySinh");
            insert_cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 50, "DiaChi");
            insert_cmd.Parameters.Add("@MaLop", SqlDbType.Char, 10, "MaLop");
            insert_cmd.Parameters.Add("@SoDT", SqlDbType.Char, 20, "SoDT");
            insert_cmd.Parameters.Add("@Email", SqlDbType.Char, 50, "Email");
            insert_cmd.Parameters.Add("@MaPhong", SqlDbType.Char, 10, "MaPhong");

            //delete command
            string delete_query = @"delete SinhVien where MaSV=@MaSV";
            SqlCommand delete_cmd = new SqlCommand(delete_query, cnn);
            delete_cmd.Parameters.Add("@MaSV", SqlDbType.Char, 10, "MaSV");

            //update command
            string update_query = @"update SinhVien set HoTen=@HoTen, GioiTinh=@GioiTinh, NgaySinh=@NgaySinh, DiaChi=@DiaChi, MaLop=@MaLop, SoDT=@SoDT, Email=@Email, MaPhong=@MaPhong where MaSV=@MaSV";
            SqlCommand update_cmd = new SqlCommand(update_query, cnn);
            update_cmd.Parameters.Add("@MaSV", SqlDbType.Char, 10, "MaSV");
            update_cmd.Parameters.Add("@HoTen", SqlDbType.NVarChar, 50, "HoTen");
            update_cmd.Parameters.Add("@GioiTinh", SqlDbType.NVarChar, 10, "GioiTinh");
            update_cmd.Parameters.Add("@NgaySinh", SqlDbType.Date, 31, "NgaySinh");
            update_cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 50, "DiaChi");
            update_cmd.Parameters.Add("@MaLop", SqlDbType.Char, 10, "MaLop");
            update_cmd.Parameters.Add("@SoDT", SqlDbType.Char, 20, "SoDT");
            update_cmd.Parameters.Add("@Email", SqlDbType.Char, 50, "Email");
            update_cmd.Parameters.Add("@MaPhong", SqlDbType.Char, 10, "MaPhong");

            //
            //
            //
            daSinhVien.InsertCommand = insert_cmd;
            daSinhVien.DeleteCommand = delete_cmd;
            daSinhVien.UpdateCommand = update_cmd;
        }

        private void ClearInput(Control c)
        {
            foreach(Control control in c.Controls)
            {
                if(control is TextBox)
                {
                    TextBox textBox = (TextBox)control;
                    textBox.Clear();
                }
                if(control is ComboBox)
                {
                    ComboBox comboBox = (ComboBox)control;
                    comboBox.Text = "";
                }
            }
        }
        #endregion

        private void dtgvHienThi_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = dtgvHienThi.SelectedRows[0];
            txtMaSV.Text = dr.Cells["MaSV"].Value.ToString().Trim();
            txtHoTen.Text = dr.Cells["HoTen"].Value.ToString().Trim();

            if(dr.Cells["GioiTinh"].Value.ToString().Trim() == "Nam")
            {
                radNam.Checked = true;
            }
            else
            {
                radNu.Checked = true;
            }

            dateNgaySinh.Text = dr.Cells["NgaySinh"].Value.ToString().Trim();
            txtDiaChi.Text = dr.Cells["DiaChi"].Value.ToString().Trim();
            txtLop.Text = dr.Cells["MaLop"].Value.ToString().Trim();
            txtSoDT.Text = dr.Cells["SoDT"].Value.ToString().Trim();
            txtEmail.Text = dr.Cells["Email"].Value.ToString().Trim();
            cbbPhongKTX.SelectedValue = dr.Cells["MaPhong"].Value.ToString().Trim();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (KiemTraODuLieu(gbThongTin))
            {
                DataRow dr = ds.Tables["tbSinhVien"].NewRow();
                dr["MaSV"] = txtMaSV.Text;
                dr["HoTen"] = txtHoTen.Text;
                dr["GioiTinh"] = radNam.Checked ? "Nam" : "Nữ";
                dr["NgaySinh"] = dateNgaySinh.Text;
                dr["DiaChi"] = txtDiaChi.Text;
                dr["MaLop"] = txtLop.Text;
                dr["SoDT"] = txtSoDT.Text;
                dr["Email"] = txtEmail.Text;
                dr["MaPhong"] = cbbPhongKTX.SelectedValue;
                ds.Tables["tbSinhVien"].Rows.Add(dr);

                btnLuu.Enabled = true;
                saved = false;
                MessageBox.Show("Thêm Thành Công", "Thông Báo", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn Có Muốn Xóa", "Thông Báo", MessageBoxButtons.OKCancel);
            if(rs == DialogResult.OK)
            {
                DataGridViewRow dr = dtgvHienThi.SelectedRows[0];
                dtgvHienThi.Rows.Remove(dr);

                btnLuu.Enabled = true;
                saved = false;
                MessageBox.Show("Xóa Thành Công", "Thông Báo", MessageBoxButtons.OKCancel);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (KiemTraODuLieu(gbThongTin))
            {
                DataGridViewRow dr = dtgvHienThi.SelectedRows[0];
                dtgvHienThi.BeginEdit(true);
                dr.Cells["MaSV"].Value = txtMaSV.Text;
                dr.Cells["HoTen"].Value = txtHoTen.Text;
                dr.Cells["GioiTinh"].Value = radNam.Checked ? "Nam" : "Nữ";
                dr.Cells["NgaySinh"].Value = dateNgaySinh.Text;
                dr.Cells["DiaChi"].Value = txtDiaChi.Text;
                dr.Cells["MaLop"].Value = txtLop.Text;
                dr.Cells["SoDT"].Value = txtSoDT.Text;
                dr.Cells["Email"].Value = txtEmail.Text;
                dr.Cells["MaPhong"].Value = cbbPhongKTX.SelectedValue;
                dtgvHienThi.EndEdit();

                btnLuu.Enabled = true;
                saved = false;
                MessageBox.Show("Cập Nhật Thành Công", "Thông Báo", MessageBoxButtons.OK);
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

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                daSinhVien.Update(ds, "tbSinhVien");
                MessageBox.Show("Lưu thành công", "Thông Báo");
                dtgvHienThi.Refresh();
            }
            catch
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

        private void txtTimMaSV_TextChanged(object sender, EventArgs e)
        {
            DataView dv = ds.Tables["tbSinhVien"].DefaultView;
            dv.RowFilter = string.Format("MaSV LIKE '%{0}%'", txtTimMaSV.Text);
            dtgvHienThi.DataSource = dv;
        }

        private void txtTimHoTen_TextChanged(object sender, EventArgs e)
        {
            DataView dv = ds.Tables["tbSinhVien"].DefaultView;
            dv.RowFilter = string.Format("HoTen LIKE '%{0}%'", txtTimHoTen.Text);
            dtgvHienThi.DataSource = dv;
        }

        private void txtTimLop_TextChanged(object sender, EventArgs e)
        {
            DataView dv = ds.Tables["tbSinhVien"].DefaultView;
            dv.RowFilter = string.Format("MaLop LIKE '%{0}%'", txtTimLop.Text);
            dtgvHienThi.DataSource = dv;
        }

        private void txtTimPhong_TextChanged(object sender, EventArgs e)
        {
            DataView dv = ds.Tables["tbSinhVien"].DefaultView;
            dv.RowFilter = string.Format("MaPhong LIKE '%{0}%'", txtTimPhong.Text);
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
