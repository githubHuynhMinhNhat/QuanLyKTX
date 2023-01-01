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
    public partial class FrmMain : Form
    {
        public FrmMain(string tmp__name, string tmp_access, string tmp_idaccount)
        {
            InitializeComponent();
            nameUser = tmp__name;
            access = tmp_access;
            id_accountUser = tmp_idaccount;
        }

        string nameUser = "";
        string access = "";
        string id_accountUser = "";
        private void FrmMain_Load(object sender, EventArgs e)
        {
            //Hiện form  đăng nhập trước

            //Load vào trang chủ khi đăng nhập thành công
            FrmTrangChu Trangchu = new FrmTrangChu();
            Trangchu.TopLevel = false;
            Trangchu.AutoScroll = false;
            Trangchu.Dock = DockStyle.Fill;
            this.pnlMainBoard.Controls.Add(Trangchu);
            Trangchu.Show();

            lblNameUser.Text = nameUser;
            lblSystemAcess.Text = String.Format("({0})", access);

            //
            if(access != "Admin")
            {
                btnQuanTri.Visible = false;
            }
            else
            {
                btnQuanTri.Visible = true;
            }

            //
            MakeLabelCenter(lblNameUser);
            MakeLabelCenter(lblSystemAcess);
        }

        private void btnQuanTri_Click(object sender, EventArgs e)
        {
            this.pnlMainBoard.Controls.Clear();
            FrmQuanTri Quantri = new FrmQuanTri();
            Quantri.TopLevel = false;
            Quantri.AutoScroll = false;
            Quantri.Dock = DockStyle.Fill;
            this.pnlMainBoard.Controls.Add(Quantri);
            Quantri.Show();

            colorbutton(btnQuanTri);
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            this.pnlMainBoard.Controls.Clear();
            FrmTrangChu Trangchu = new FrmTrangChu();
            Trangchu.TopLevel = false;
            Trangchu.AutoScroll = false;
            Trangchu.Dock = DockStyle.Fill;
            this.pnlMainBoard.Controls.Add(Trangchu);
            Trangchu.Show();

            colorbutton(btnTrangChu);
        }

        private void btnSinhVien_Click(object sender, EventArgs e)
        {
            this.pnlMainBoard.Controls.Clear();
            FrmSinhVien Sinhvien = new FrmSinhVien();
            Sinhvien.TopLevel = false;
            Sinhvien.AutoScroll = false;
            Sinhvien.Dock = DockStyle.Fill;
            this.pnlMainBoard.Controls.Add(Sinhvien);
            Sinhvien.Show();

            colorbutton(btnSinhVien);
        }

        private void btnNhanSu_Click(object sender, EventArgs e)
        {
            this.pnlMainBoard.Controls.Clear();
            FrmNhanSu Nhansu = new FrmNhanSu();
            Nhansu.TopLevel = false;
            Nhansu.AutoScroll = false;
            Nhansu.Dock = DockStyle.Fill;
            this.pnlMainBoard.Controls.Add(Nhansu);
            Nhansu.Show();

            colorbutton(btnNhanSu);
        }

        private void btnKyTucXa_Click(object sender, EventArgs e)
        {
            this.pnlMainBoard.Controls.Clear();
            FrmPhongKTX KTX = new FrmPhongKTX();
            KTX.TopLevel = false;
            KTX.AutoScroll = false;
            KTX.Dock = DockStyle.Fill;
            this.pnlMainBoard.Controls.Add(KTX);
            KTX.Show();

            colorbutton(btnKyTucXa);
        }

        private void btnBieuMau_Click(object sender, EventArgs e)
        {
            this.pnlMainBoard.Controls.Clear();
            FrmBieuMau Bieumau = new FrmBieuMau(id_accountUser);
            Bieumau.TopLevel = false;
            Bieumau.AutoScroll = false;
            Bieumau.Dock = DockStyle.Fill;
            this.pnlMainBoard.Controls.Add(Bieumau);
            Bieumau.Show();

            colorbutton(btnBieuMau);
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn Có Muốn Thoát Khỏi Hệ Thống"
                , "Thông Báo"
                , MessageBoxButtons.OKCancel);
            if(result == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void MakeLabelCenter(Label lbl)
        {
            int khoangdu = (pnlLeftBar.Width - lbl.Width) / 2;
            Point point = new Point(khoangdu, lbl.Location.Y);
            lbl.Location = point;
        }

        private void colorbutton(Button btn)
        {
            foreach(Control c in this.pnlLeftBar.Controls)
            {
                if(c is Button)
                {
                    if(c.Name == btn.Name)
                    {
                        c.BackColor = Color.FromArgb(238, 238, 228);
                        c.ForeColor = Color.Black;
                    }
                    else
                    {
                        c.BackColor = Color.FromArgb(51, 122, 183);
                        c.ForeColor = Color.White;
                    }
                }
            }
        }
    }
}
