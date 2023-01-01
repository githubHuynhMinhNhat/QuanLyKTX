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
    public partial class FrmDangNhap : Form
    {
        public FrmDangNhap()
        {
            InitializeComponent();
        }

        SqlConnection cnn = new SqlConnection(KetNoi.ConnectionString);
        SqlDataAdapter daTaiKhoan;
        DataSet ds = new DataSet("dsTaiKhoan");

        private void FrmDangNhap_Load(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand("select tk.Taikhoan, tk.MatKhau, tk.MaNV, nv.HoTen, tk.Quyen from TaiKhoan as tk left join NhanVien as nv on nv.MaNV = tk.MaNV", cnn);
            daTaiKhoan = new SqlDataAdapter(sqlCommand);
            daTaiKhoan.Fill(ds, "tbTaiKhoan");
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if(txtTaiKhoan.Text == "" || txtMatKhau.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tài khoản và mật khẩu", "Thông Báo", MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
            }
            else
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["Taikhoan"].ToString().Trim() == txtTaiKhoan.Text && dr["MatKhau"].ToString().Trim() == txtMatKhau.Text)
                    {
                        MessageBox.Show("Đăng Nhập Thành Công");
                        FrmMain frmMain = new FrmMain(dr["HoTen"].ToString().Trim(), dr["Quyen"].ToString().Trim(), dr["MaNV"].ToString().Trim());
                        frmMain.Show();
                        this.Hide();
                    }
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn có muốn thoát", "Thông Báo", MessageBoxButtons.OKCancel);
            if (rs == DialogResult.OK)
            {
                Application.Exit();
            }
        }
    }
}
