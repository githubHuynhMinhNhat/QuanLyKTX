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
    public partial class FrmPhongKTX : Form
    {
        SqlConnection cnn = new SqlConnection(KetNoi.ConnectionString);
        SqlDataAdapter daKhuNha;
        SqlDataAdapter daPhongKTX;
        DataSet ds = new DataSet("dsPhongKTX");
        bool saved = false;
        public FrmPhongKTX()
        {
            InitializeComponent();
        }
        private void FrmPhongKTX_Load(object sender, EventArgs e)
        {
            //load dữ liệu vào commbox
            string selectKhuNha = @"select n.MaNha, n.TenNha from Nha as n";
            daKhuNha = new SqlDataAdapter(selectKhuNha, cnn);
            daKhuNha.Fill(ds, "tbKhuNha");
            cbbMaNha.DataSource = ds.Tables["tbKhuNha"];
            cbbMaNha.DisplayMember = "TenNha";
            cbbMaNha.ValueMember = "MaNha";
            //
            //
            string selectPhong = @"select p.MaPhong, p.Manha, n.TenNha, count(sv.MaPhong) as DaDangKy, p.Toida, p.GiaPhong from Phong as p left join SinhVien as sv on sv.MaPhong = p.MaPhong left join Nha as n on n.MaNha = p.MaNha group by p.MaPhong, p.Manha, n.TenNha, p.Toida, p.GiaPhong";
            daPhongKTX = new SqlDataAdapter(selectPhong, cnn);
            daPhongKTX.Fill(ds, "tbPhong");
            dtgvHienThi.DataSource = ds;
            dtgvHienThi.DataMember = "tbPhong";

            dtgvHienThi.Columns["MaNha"].Visible = false;
            dtgvHienThi.Columns["MaPhong"].HeaderText = "Mã Phòng";
            dtgvHienThi.Columns["TenNha"].HeaderText = "Khu Nhà";
            dtgvHienThi.Columns["DaDangKy"].HeaderText = "Đã Đăng Ký";
            dtgvHienThi.Columns["Toida"].HeaderText = "Tối Đa";
            dtgvHienThi.Columns["GiaPhong"].HeaderText = "Giá Phòng";
            //
            SettingDataGridView();

            //
            SettingCommand();

            //
            btnLuu.Enabled = false;
        }

        private void SettingDataGridView()
        {
            dtgvHienThi.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dtgvHienThi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvHienThi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgvHienThi.AllowUserToAddRows = false;
            dtgvHienThi.AllowUserToDeleteRows = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (KiemTraODuLieu(gbThongTin))
            {
                DataRow dr = ds.Tables["tbPhong"].NewRow();
                dr["MaPhong"] = txtMaPhong.Text;
                dr["MaNha"] = cbbMaNha.SelectedValue;
                dr["TenNha"] = cbbMaNha.Text;
                dr["DaDangKy"] = 0;
                dr["ToiDa"] = numUpDownSoNguoiToiDa.Value;
                dr["GiaPhong"] = txtGiaPhong.Text;
                ds.Tables["tbPhong"].Rows.Add(dr);
                MessageBox.Show("Thêm Thành Công", "Thông Báo", MessageBoxButtons.OK);
                btnLuu.Enabled = true;
                saved = false;
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo");
            }
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
            
            if (KiemTraODuLieu(gbThongTin))
            {
                DataGridViewRow dr = dtgvHienThi.SelectedRows[0];
                dtgvHienThi.BeginEdit(true);
                dr.Cells["MaPhong"].Value = txtMaPhong.Text;
                dr.Cells["MaNha"].Value = cbbMaNha.SelectedValue;
                dr.Cells["TenNha"].Value = cbbMaNha.Text;
                dr.Cells["ToiDa"].Value = numUpDownSoNguoiToiDa.Text;
                dr.Cells["GiaPhong"].Value = txtGiaPhong.Text;
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
                daPhongKTX.Update(ds, "tbPhong");
                MessageBox.Show("Lưu thành công", "Thông Báo");
                dtgvHienThi.Refresh();
                saved = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lưu Không Thành Công Vui Lòng Kiểm Tra Lại", "Thông Báo", MessageBoxButtons.OK);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearInput(gbThongTin);
            ClearInput(gbTimKiem);
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

        private void txtTimMaPhong_TextChanged(object sender, EventArgs e)
        {
            DataView dv = ds.Tables["tbPhong"].DefaultView;
            dv.RowFilter = string.Format("MaPhong LIKE '%{0}%'", txtTimMaPhong.Text);
            dtgvHienThi.DataSource = dv;
        }

        private void txtTimMaNha_TextChanged(object sender, EventArgs e)
        {
            DataView dv = ds.Tables["tbPhong"].DefaultView;
            dv.RowFilter = string.Format("MaNha LIKE '%{0}%'", txtTimMaNha.Text);
            dtgvHienThi.DataSource = dv;
        }


        private void ckbTimPhongTrong_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbTimPhongTrong.Checked)
            {
                DataView dv = ds.Tables["tbPhong"].DefaultView;
                dv.RowFilter = "DaDangKy < Toida";
                dtgvHienThi.DataSource = dv;
            }
            else
            {
                return;
            }
        }

        private void txtSoPhong_TextChanged(object sender, EventArgs e)
        {
            DataView dv = ds.Tables["tbPhong"].DefaultView;
            dv.RowFilter = string.Format("Convert(Toida, System.String) LIKE '%{0}%'", txtSoPhong.Text);
            dtgvHienThi.DataSource = dv;
        }


        private void dtgvHienThi_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow dtgvr = dtgvHienThi.SelectedRows[0];
                txtMaPhong.Text = dtgvr.Cells["MaPhong"].Value.ToString().Trim();
                cbbMaNha.SelectedValue = dtgvr.Cells["MaNha"].Value.ToString().Trim();
                numUpDownSoNguoiToiDa.Value = decimal.Parse(dtgvr.Cells["ToiDa"].Value.ToString().Trim());
                txtGiaPhong.Text = dtgvr.Cells["GiaPhong"].Value.ToString().Trim();
            }
            catch
            {
                return;
            }
        }

        private void SettingCommand()
        {
            //Insert command
            string insert_command = @"insert into Phong values (@MaPhong, @Manha, @Toida, @GiaPhong)";
            SqlCommand cmd_insert = new SqlCommand(insert_command, cnn);
            cmd_insert.Parameters.Add("@MaPhong", SqlDbType.Char, 10, "MaPhong");
            cmd_insert.Parameters.Add("@Manha", SqlDbType.Char, 10, "Manha");
            cmd_insert.Parameters.Add("@ToiDa", SqlDbType.Int, 8, "Toida");
            cmd_insert.Parameters.Add("@GiaPhong", SqlDbType.Int, 8, "GiaPhong");

            daPhongKTX.InsertCommand = cmd_insert;
            //Update command
            string update_command = @"Update Phong set Manha=@Manha, Toida=@ToiDa, GiaPhong=@GiaPhong where MaPhong=@MaPhong";
            SqlCommand cmd_update = new SqlCommand(update_command, cnn);
            cmd_update.Parameters.Add("@MaPhong", SqlDbType.Char, 10, "MaPhong");
            cmd_update.Parameters.Add("@Manha", SqlDbType.Char, 10, "Manha");
            cmd_update.Parameters.Add("@ToiDa", SqlDbType.Int, 8, "Toida");
            cmd_update.Parameters.Add("@GiaPhong", SqlDbType.Int, 8, "GiaPhong");

            daPhongKTX.UpdateCommand = cmd_update;

            //delete
            string delete_command = @"Delete Phong where MaPhong=@MaPhong";
            SqlCommand cmd_delete = new SqlCommand(delete_command,cnn);
            cmd_delete.Parameters.Add("@MaPhong", SqlDbType.Char, 10, "MaPhong");

            daPhongKTX.DeleteCommand = cmd_delete;
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

        private bool KiemTraODuLieu(GroupBox gb)
        {
            foreach(Control c in gb.Controls)
            {
                if (c.Text == "")
                    return false;
            }
            return true;
        }

    }
}
