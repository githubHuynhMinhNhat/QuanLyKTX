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
using System.Drawing.Printing;

namespace QLKYTUCXA
{
    public partial class FrmBieuMau : Form
    {
        public FrmBieuMau(string id_temp)
        {
            InitializeComponent();
            id_Employee = id_temp;
        }
        string id_Employee = "";
        SqlConnection cnn = new SqlConnection(KetNoi.ConnectionString);
        SqlDataAdapter daHopDong;
        SqlDataAdapter daHoaDon;
        SqlDataAdapter daPhong;
        DataSet ds = new DataSet("daBieuMau");
        bool HopDongSaved = false, HoaDonSaved = false;
        private void FrmBieuMau_Load(object sender, EventArgs e)
        {
            //Load combobox
            string selectphong = @"select * from Phong";
            daPhong = new SqlDataAdapter(selectphong, cnn);
            daPhong.Fill(ds, "tbPhong");
            cbbMaPhong.DataSource = ds.Tables["tbPhong"];
            cbbMaPhong.DisplayMember = "MaPhong";
            cbbMaPhong.ValueMember = "MaPhong";
            //Tab HopDong
            //string selectHopDong = String.Format(@"select * from HopDong where MaNV='{0}'", id_Employee);
            string selectHopDong = @"select * from HopDong";
            daHopDong = new SqlDataAdapter(selectHopDong, cnn);
            daHopDong.Fill(ds, "tbHopDong");

            tabHopDong_dtgvHienThi.DataSource = ds;
            tabHopDong_dtgvHienThi.DataMember = "tbHopDong";

            tabHopDong_cbbTrangThai.Items.Add("Hết Thời Hạn");
            tabHopDong_cbbTrangThai.Items.Add("Còn Thời Hạn");

            tabHopDong_btnLuu.Enabled = false;

            HopDongCommand();
            //TabHoaDon
            string selectHoaDon = @"select hd.MaHoaDon, hd.MaSV, hd.Maphong, hd.NgayLap, hd.SoDien, hd.SoNuoc, hd.GiaDien, hd.GiaNuoc
, ph.GiaPhong, (hd.SoDien*hd.GiaDien + hd.SoNuoc*hd.GiaNuoc + ph.GiaPhong) as ThanhTien, hd.TrangThai
from HoaDon as hd
left join SinhVien as sv
on sv.MaSV = hd.MaSV
left join Phong as ph
on ph.MaPhong = hd.Maphong";
            daHoaDon = new SqlDataAdapter(selectHoaDon, cnn);
            daHoaDon.Fill(ds, "tbHoaDon");

            tabHoaDon_dtgvHienThi.DataSource = ds;
            tabHoaDon_dtgvHienThi.DataMember = "tbHoaDon";

            tabHoaDon_btnLuu.Enabled = false;

            daHoaDonCommand();

            //Cac thuoc tinh datagridview
            //HopDong
            SettingDataGridView(tabHopDong_dtgvHienThi);
            tabHopDong_dtgvHienThi.Columns["MaHopDong"].HeaderText = "Mã Hợp Đồng";
            tabHopDong_dtgvHienThi.Columns["MaSV"].HeaderText = "Mã Sinh Viên";
            tabHopDong_dtgvHienThi.Columns["Maphong"].HeaderText = "Mã Phòng";
            tabHopDong_dtgvHienThi.Columns["NgayLap"].HeaderText = "Ngày Lập";
            tabHopDong_dtgvHienThi.Columns["NgayBatDau"].HeaderText = "Ngày Bắt Đầu";
            tabHopDong_dtgvHienThi.Columns["NgayKetThuc"].HeaderText = "Ngày Kết Thúc";
            tabHopDong_dtgvHienThi.Columns["TrangThai"].HeaderText = "Trạng Thái";
            //HoaDon
            SettingDataGridView(tabHoaDon_dtgvHienThi);
            tabHoaDon_dtgvHienThi.Columns["MaHoaDon"].HeaderText = "Mã Hóa Đơn";
            tabHoaDon_dtgvHienThi.Columns["MaSV"].HeaderText = "Mã Sinh Viên";
            tabHoaDon_dtgvHienThi.Columns["Maphong"].HeaderText = "Mã Phòng";
            tabHoaDon_dtgvHienThi.Columns["NgayLap"].HeaderText = "Ngày Lập";
            tabHoaDon_dtgvHienThi.Columns["SoDien"].HeaderText = "Số Điện";
            tabHoaDon_dtgvHienThi.Columns["SoNuoc"].HeaderText = "Số Nước";
            tabHoaDon_dtgvHienThi.Columns["GiaDien"].HeaderText = "Giá Điện";
            tabHoaDon_dtgvHienThi.Columns["GiaNuoc"].HeaderText = "Giá Nước";
            tabHoaDon_dtgvHienThi.Columns["GiaPhong"].HeaderText = "Giá Phòng";
            tabHoaDon_dtgvHienThi.Columns["ThanhTien"].HeaderText = "Thành Tiền";
            tabHoaDon_dtgvHienThi.Columns["TrangThai"].HeaderText = "Trạng Thái";
        }

        private void SettingDataGridView(DataGridView dtgv)
        {
            dtgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dtgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgv.AllowUserToAddRows = false;
            dtgv.AllowUserToDeleteRows = false;
            dtgv.ScrollBars = ScrollBars.Both;
        }

        private void tabHopDong_dtgvHienThi_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow dtgvr = tabHopDong_dtgvHienThi.SelectedRows[0];
                tabHopDong_txtMaHDong.Text = dtgvr.Cells["MaHopDong"].Value.ToString().Trim();
                tabHopDong_txtMaSinhVien.Text = dtgvr.Cells["MaSV"].Value.ToString().Trim();
                tabHopDong_txtMaPhong.Text = dtgvr.Cells["Maphong"].Value.ToString().Trim();
                //Mã nhân viên lấy từ tài khoản
                tabHopDong_dateNgayLap.Text = dtgvr.Cells["NgayLap"].Value.ToString().Trim();
                tabHopDong_dateNgayBatDau.Text = dtgvr.Cells["NgayBatDau"].Value.ToString().Trim();
                tabHopDong_dateNgayKetThuc.Text = dtgvr.Cells["NgayKetThuc"].Value.ToString().Trim();
                tabHopDong_cbbTrangThai.Text = dtgvr.Cells["TrangThai"].Value.ToString().Trim();
            }
            catch
            {
                return;
            }
        }

        private void tabHopDong_btnThem_Click(object sender, EventArgs e)
        {
            if (KiemTraODuLieu(tabHopDong_gbThongTin))
            {
                DataRow dr = ds.Tables["tbHopDong"].NewRow();
                dr["MaHopDong"] = tabHopDong_txtMaHDong.Text;
                dr["MaSV"] = tabHopDong_txtMaSinhVien.Text;
                dr["MaNV"] = this.id_Employee;
                dr["Maphong"] = tabHopDong_txtMaPhong.Text;
                dr["NgayLap"] = tabHopDong_dateNgayLap.Text;
                dr["NgayBatDau"] = tabHopDong_dateNgayBatDau.Text;
                dr["NgayKetThuc"] = tabHopDong_dateNgayKetThuc.Text;
                dr["TrangThai"] = tabHopDong_cbbTrangThai.Text;

                ds.Tables["tbHopDong"].Rows.Add(dr);
                MessageBox.Show("Thêm Thành Công", "Thông Báo", MessageBoxButtons.OK);
                tabHopDong_btnLuu.Enabled = true;
                HopDongSaved = false;
            }
            else
            {
                MessageBox.Show("Vui Lòng Nhập Đầy Đủ Thông Tin!", "Thông Báo");
            }
        }

        private void tabHopDong_btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearInput(tabHopDong_gbThongTin);
            ClearInput(tabHopDong_gbTimKiem);
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

        private void tabHopDong_btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn Có Muốn Xóa", "Thông Báo", MessageBoxButtons.OKCancel);
            {
                DataGridViewRow dr = tabHopDong_dtgvHienThi.SelectedRows[0];
                tabHopDong_dtgvHienThi.Rows.Remove(dr);

                MessageBox.Show("Xóa Thành Công", "Thông Báo", MessageBoxButtons.OK);
                tabHopDong_btnLuu.Enabled = true;
                HopDongSaved = false;
            }
        }

        private void tabHopDong_btnSua_Click(object sender, EventArgs e)
        {
            if (KiemTraODuLieu(tabHopDong_gbThongTin))
            {
                DataGridViewRow dr = tabHopDong_dtgvHienThi.SelectedRows[0];
                tabHopDong_dtgvHienThi.BeginEdit(true);
                dr.Cells["MaHopDong"].Value = tabHopDong_txtMaHDong.Text;
                dr.Cells["MaSV"].Value = tabHopDong_txtMaSinhVien.Text;
                dr.Cells["MaNV"].Value = this.id_Employee;
                dr.Cells["Maphong"].Value = tabHopDong_txtMaPhong.Text;
                dr.Cells["NgayLap"].Value = tabHopDong_dateNgayLap.Text;
                dr.Cells["NgayBatDau"].Value = tabHopDong_dateNgayBatDau.Text;
                dr.Cells["NgayKetThuc"].Value = tabHopDong_dateNgayKetThuc.Text;
                dr.Cells["TrangThai"].Value = tabHopDong_cbbTrangThai.Text;
                tabHopDong_dtgvHienThi.EndEdit();
                tabHopDong_btnLuu.Enabled = true;
                HopDongSaved = false;
                MessageBox.Show("Cập Nhật Thành Công", "Thông Báo", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Vui Lòng Nhập Đầy Đủ Thông Tin!", "Thông Báo");
            }
        }
        private void tabHopDong_btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                daHopDong.Update(ds, "tbHopDong");
                MessageBox.Show("Lưu thành công", "Thông Báo");
                tabHopDong_dtgvHienThi.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lưu Không Thành Công Vui Lòng Kiểm Tra Lại", "Thông Báo", MessageBoxButtons.OK);
            }
            HopDongSaved = true;
        }

        private void HopDongCommand()
        {
            //Insert command
            string insert_query = "insert into HopDong values (@MaHopDong, @MaNV, @MaSV, @Maphong, @NgayLap, @NgayBatDau, @NgayKetThuc, @TrangThai)";
            SqlCommand insert_cmd = new SqlCommand(insert_query, cnn);
            insert_cmd.Parameters.Add("@MaHopDong", SqlDbType.Char, 10, "MaHopDong");
            insert_cmd.Parameters.Add("@MaNV", SqlDbType.Char, 10, "MaNV");
            insert_cmd.Parameters.Add("@MaSV", SqlDbType.Char, 10, "MaSV");
            insert_cmd.Parameters.Add("@Maphong", SqlDbType.Char, 10, "Maphong");
            insert_cmd.Parameters.Add("@NgayLap", SqlDbType.Date, 31, "NgayLap");
            insert_cmd.Parameters.Add("@NgayBatDau", SqlDbType.Date, 31, "NgayBatDau");
            insert_cmd.Parameters.Add("@NgayKetThuc", SqlDbType.Date, 31, "NgayKetThuc");
            insert_cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 20, "TrangThai");

            daHopDong.InsertCommand = insert_cmd;

            //Update command
            //Không update mã nv
            string update_query = "update HopDong set MaSV=@MaSV, Maphong=@Maphong, NgayLap=@NgayLap, NgayBatDau=@NgayBatDau, NgayKetThuc=@NgayKetThuc, TrangThai=@TrangThai where MaHopDong=@MaHopDong";
            SqlCommand update_cmd = new SqlCommand(update_query, cnn);
            update_cmd.Parameters.Add("@MaHopDong", SqlDbType.Char, 10, "MaHopDong");
            //update_cmd.Parameters.Add("@MaNV", SqlDbType.Char, 10, "MaNV");
            update_cmd.Parameters.Add("@MaSV", SqlDbType.Char, 10, "MaSV");
            update_cmd.Parameters.Add("@Maphong", SqlDbType.Char, 10, "Maphong");
            update_cmd.Parameters.Add("@NgayLap", SqlDbType.Date, 31, "NgayLap");
            update_cmd.Parameters.Add("@NgayBatDau", SqlDbType.Date, 31, "NgayBatDau");
            update_cmd.Parameters.Add("@NgayKetThuc", SqlDbType.Date, 31, "NgayKetThuc");
            update_cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 20, "TrangThai");
            daHopDong.UpdateCommand = update_cmd;

            //delete command 
            string delete_query = "delete HopDong where MaHopDong=@MaHopDong";
            SqlCommand delete_cmd = new SqlCommand(delete_query, cnn);
            delete_cmd.Parameters.Add("@MaHopDong", SqlDbType.Char, 10, "MaHopDong");

            daHopDong.DeleteCommand = delete_cmd;
        }

        private void tabHopDong_btnThoat_Click(object sender, EventArgs e)
        {
            if (HopDongSaved)
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

        private void tabHopDong_TimMaHopDong_TextChanged(object sender, EventArgs e)
        {
            DataView dv = ds.Tables["tbHopDong"].DefaultView;
            dv.RowFilter = string.Format("MaHopDong LIKE '%{0}%'", tabHopDong_TimMaHopDong.Text);
            tabHopDong_dtgvHienThi.DataSource = dv;
        }

        private void tabHopDong_TimMaSV_TextChanged(object sender, EventArgs e)
        {
            DataView dv = ds.Tables["tbHopDong"].DefaultView;
            dv.RowFilter = string.Format("MaSV LIKE '%{0}%'", tabHopDong_TimMaSV.Text);
            tabHopDong_dtgvHienThi.DataSource = dv;
        }

        private void tabHopDong_txtTimMaPhong_TextChanged(object sender, EventArgs e)
        {
            DataView dv = ds.Tables["tbHopDong"].DefaultView;
            dv.RowFilter = string.Format("Maphong LIKE '%{0}%'", tabHopDong_txtTimMaPhong.Text);
            tabHopDong_dtgvHienThi.DataSource = dv;
        }

        private void tabHopDong_TimNgayLap_TextChanged(object sender, EventArgs e)
        {
            DataView dv = ds.Tables["tbHopDong"].DefaultView;
            dv.RowFilter = string.Format("Convert(NgayLap, System.String) like '%{0}%'", tabHopDong_TimNgayLap.Text);
            tabHopDong_dtgvHienThi.DataSource = dv;
        }

        private void tabHopDong_txtTimTrangThai_TextChanged(object sender, EventArgs e)
        {
            DataView dv = ds.Tables["tbHopDong"].DefaultView;
            dv.RowFilter = string.Format("TrangThai LIKE '%{0}%'", tabHopDong_txtTimTrangThai.Text);
            tabHopDong_dtgvHienThi.DataSource = dv;
        }

        //TabHoaDon

        private void tabHoaDon_dtgvHienThi_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow dtgvr = tabHoaDon_dtgvHienThi.SelectedRows[0];
                tabHoaDon_txtMaHoaDon.Text = dtgvr.Cells["MaHoaDon"].Value.ToString().Trim();
                tabHoaDon_txtMaSinhVien.Text = dtgvr.Cells["MaSV"].Value.ToString().Trim();
                //tabHoaDon_txtMaPhong.Text = dtgvr.Cells["Maphong"].Value.ToString().Trim();
                //Mã nhân viên lấy từ tài khoản
                tabHoaDon_dateNgayLap.Text = dtgvr.Cells["NgayLap"].Value.ToString().Trim();
                tabHoaDon_txtSoDien.Text = dtgvr.Cells["SoDien"].Value.ToString().Trim();
                tabHoaDon_txtSoNuoc.Text = dtgvr.Cells["SoNuoc"].Value.ToString().Trim();
                tabHoaDon_txtGiaDien.Text = dtgvr.Cells["GiaDien"].Value.ToString().Trim();
                tabHoaDon_txtGiaNuoc.Text = dtgvr.Cells["GiaNuoc"].Value.ToString().Trim();
                if(dtgvr.Cells["TrangThai"].Value.ToString().Trim() == "Đã Thanh Toán")
                {
                    tabHoaDon_ckbThanhToan.Checked = true;
                }
                else { tabHoaDon_ckbThanhToan.Checked = false; }
            }
            catch
            {
                return;
            }
        }
        private void tabHoaDon_btnThem_Click(object sender, EventArgs e)
        {
            if (KiemTraODuLieu(tabHoaDon_gbThongTin))
            {
                DataRow dr = ds.Tables["tbHoaDon"].NewRow();
                dr["MaHoaDon"] = tabHoaDon_txtMaHoaDon.Text;
                dr["MaSV"] = tabHoaDon_txtMaSinhVien.Text;
                //dr["MaNV"] = this.id_Employee;
                dr["Maphong"] = cbbMaPhong.Text;
                dr["NgayLap"] = tabHoaDon_dateNgayLap.Text;
                dr["SoDien"] = tabHoaDon_txtSoDien.Text;
                dr["SoNuoc"] = tabHoaDon_txtSoNuoc.Text;
                dr["GiaDien"] = tabHoaDon_txtGiaDien.Text;
                dr["GiaNuoc"] = tabHoaDon_txtGiaNuoc.Text;

                foreach (DataRow datarow in ds.Tables["tbPhong"].Rows)
                {
                    if (cbbMaPhong.SelectedValue == datarow["MaPhong"])
                    {
                        dr["GiaPhong"] = datarow["GiaPhong"];
                    }
                }

                dr["TrangThai"] = tabHoaDon_ckbThanhToan.Checked ? "Đã Thanh Toán" : "Chưa Thanh Toán";
                dr["ThanhTien"] = (Convert.ToInt32(tabHoaDon_txtSoDien.Text) * Convert.ToInt32(tabHoaDon_txtGiaDien.Text)
                    + Convert.ToInt32(tabHoaDon_txtSoNuoc.Text) * Convert.ToInt32(tabHoaDon_txtGiaNuoc.Text) + Convert.ToInt32(dr["GiaPhong"])).ToString();

                ds.Tables["tbHoaDon"].Rows.Add(dr);
                MessageBox.Show("Thêm Thành Công", "Thông Báo", MessageBoxButtons.OK);
                tabHoaDon_btnLuu.Enabled = true;
                HoaDonSaved = false;
            }
            else
            {
                MessageBox.Show("Vui Lòng Nhập Đầy Đủ Thông Tin!", "Thông Báo");
            }
        }

        private void daHoaDonCommand()
        {
            //Insert command
            string insert_query = "insert into HoaDon values (@MaHoaDon, @MaNV, @MaSV, @Maphong, @NgayLap, @SoDien, @SoNuoc, @GiaDien, @GiaNuoc, @TrangThai)";
            SqlCommand insert_cmd = new SqlCommand(insert_query, cnn);
            insert_cmd.Parameters.Add("@MaHoaDon", SqlDbType.Char, 10, "MaHoaDon");
            insert_cmd.Parameters.Add("@MaNV", SqlDbType.Char, 10).Value = this.id_Employee;
            insert_cmd.Parameters.Add("@MaSV", SqlDbType.Char, 10, "MaSV");
            insert_cmd.Parameters.Add("@Maphong", SqlDbType.Char, 10, "Maphong");
            insert_cmd.Parameters.Add("@NgayLap", SqlDbType.Date, 31, "NgayLap");
            insert_cmd.Parameters.Add("@SoDien", SqlDbType.Int, 8, "SoDien");
            insert_cmd.Parameters.Add("@SoNuoc", SqlDbType.Int, 8, "SoNuoc");
            insert_cmd.Parameters.Add("@GiaDien", SqlDbType.Int, 8, "GiaDien");
            insert_cmd.Parameters.Add("@GiaNuoc", SqlDbType.Int, 8, "GiaNuoc");
            insert_cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 20, "TrangThai");

            daHoaDon.InsertCommand = insert_cmd;

            //Update command
            string update_query = "update HoaDon set MaSV=@MaSV, Maphong=@Maphong, NgayLap=@NgayLap, SoDien=@SoDien, SoNuoc=@SoNuoc, GiaDien=@GiaDien, GiaNuoc=@GiaNuoc,TrangThai=@TrangThai where MaHoaDon=@MaHoaDon";
            SqlCommand update_cmd = new SqlCommand(update_query, cnn);
            update_cmd.Parameters.Add("@MaHoaDon", SqlDbType.Char, 10, "MaHoaDon");
            //supdate_cmd.Parameters.Add("@MaNV", SqlDbType.Char, 10, "MaNV");
            update_cmd.Parameters.Add("@MaSV", SqlDbType.Char, 10, "MaSV");
            update_cmd.Parameters.Add("@Maphong", SqlDbType.Char, 10, "Maphong");
            update_cmd.Parameters.Add("@NgayLap", SqlDbType.Date, 31, "NgayLap");
            update_cmd.Parameters.Add("@SoDien", SqlDbType.Int, 8, "SoDien");
            update_cmd.Parameters.Add("@SoNuoc", SqlDbType.Int, 8, "SoNuoc");
            update_cmd.Parameters.Add("@GiaDien", SqlDbType.Int, 8, "GiaDien");
            update_cmd.Parameters.Add("@GiaNuoc", SqlDbType.Int, 8, "GiaNuoc");
            update_cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 20, "TrangThai");
            daHoaDon.UpdateCommand = update_cmd;

            //delete command 
            string delete_query = "delete HoaDon where MaHoaDon=@MaHoaDon";
            SqlCommand delete_cmd = new SqlCommand(delete_query, cnn);
            delete_cmd.Parameters.Add("@MaHoaDon", SqlDbType.Char, 10, "MaHoaDon");

            daHoaDon.DeleteCommand = delete_cmd;
        }

        private void tabHoaDon_btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearInput(tabHoaDon_gbThongTin);
            ClearInput(tabHoaDon_gbTimKiem);
        }

        private void tabHoaDon_btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn Có Muốn Xóa", "Thông Báo", MessageBoxButtons.OKCancel);
            if(rs == DialogResult.OK){
                DataGridViewRow dr = tabHoaDon_dtgvHienThi.SelectedRows[0];
                tabHoaDon_dtgvHienThi.Rows.Remove(dr);

                MessageBox.Show("Xóa Thành Công", "Thông Báo", MessageBoxButtons.OK);
                tabHoaDon_btnLuu.Enabled = true;
                HoaDonSaved = false;
            }
        }

        private void tabHoaDon_btnSua_Click(object sender, EventArgs e)
        {
            if (KiemTraODuLieu(tabHoaDon_gbThongTin))
            {
                DataGridViewRow dr = tabHoaDon_dtgvHienThi.SelectedRows[0];
                tabHoaDon_dtgvHienThi.BeginEdit(true);
                dr.Cells["MaHoaDon"].Value = tabHoaDon_txtMaHoaDon.Text;
                dr.Cells["MaSV"].Value = tabHoaDon_txtMaSinhVien.Text;
                //dr.Cells["MaNV"].Value = this.id_Employee;
                dr.Cells["Maphong"].Value = cbbMaPhong.Text;
                dr.Cells["NgayLap"].Value = tabHoaDon_dateNgayLap.Text;
                dr.Cells["SoDien"].Value = tabHoaDon_txtSoDien.Text;
                dr.Cells["SoNuoc"].Value = tabHoaDon_txtSoNuoc.Text;
                dr.Cells["GiaDien"].Value = tabHoaDon_txtGiaDien.Text;
                dr.Cells["GiaNuoc"].Value = tabHoaDon_txtGiaNuoc.Text;

                foreach (DataRow datarow in ds.Tables["tbPhong"].Rows)
                {
                    if (cbbMaPhong.SelectedValue == datarow["MaPhong"])
                    {
                        dr.Cells["GiaPhong"].Value = datarow["GiaPhong"];
                    }
                }

                dr.Cells["TrangThai"].Value = tabHoaDon_ckbThanhToan.Checked ? "Đã Thanh Toán" : "Chưa Thanh Toán";
                dr.Cells["ThanhTien"].Value = (Convert.ToInt32(tabHoaDon_txtSoDien.Text) * Convert.ToInt32(tabHoaDon_txtGiaDien.Text)
                    + Convert.ToInt32(tabHoaDon_txtSoNuoc.Text) * Convert.ToInt32(tabHoaDon_txtGiaNuoc.Text) + Convert.ToInt32(dr.Cells["GiaPhong"].Value)).ToString();
                tabHoaDon_dtgvHienThi.EndEdit();
                tabHoaDon_btnLuu.Enabled = true;
                HoaDonSaved = false;
                MessageBox.Show("Cập Nhật Thành Công", "Thông Báo", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Vui Lòng Nhập Đầy Đủ Thông Tin!", "Thông Báo");
            }
        }
        private void tabHoaDon_btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                daHoaDon.Update(ds, "tbHoaDon");
                MessageBox.Show("Lưu thành công", "Thông Báo");
                tabHoaDon_dtgvHienThi.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lưu Không Thành Công Vui Lòng Kiểm Tra Lại", "Thông Báo", MessageBoxButtons.OK);
            }
            HoaDonSaved = true;
        }

        private void tabHoaDon_btnThoat_Click(object sender, EventArgs e)
        {
            if (HoaDonSaved)
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