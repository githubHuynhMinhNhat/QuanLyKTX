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
    public partial class FrmToaNha : Form
    {
        public FrmToaNha(string setId)
        {
            InitializeComponent();
            id_ToaNha = setId;
        }

        string id_ToaNha = "";
        SqlConnection cnn = new SqlConnection("Data Source=MSI;Initial Catalog=QUANLIKTX;Integrated Security=True");
        DataTable table = new DataTable();

        private void FrmToaNha_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " " + id_ToaNha;
            try {
                cnn.Open();
                SqlCommand cmd = new SqlCommand(String.Format("Select * from Phong where Manha='{0}'", id_ToaNha), cnn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(table);
            }
            catch(Exception ex) {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                cnn.Close();
            }
        }

        //Tạo các button của các phòng
        
    }
}
