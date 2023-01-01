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
    public partial class FrmPhong : Form
    {
        SqlConnection cnn = new SqlConnection(KetNoi.Connection);
        public FrmPhong()
        {
            InitializeComponent();
        }

        private void FrmPhong_Load(object sender, EventArgs e)
        {
            try
            {
                cnn.Open();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                cnn.Close();
            }
        }
    }
}
