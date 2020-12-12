using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtoparkOtomasyonu
{
    public partial class frmSatisListeleme : Form
    {
        public frmSatisListeleme()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-E8LRUC4\SQLEXPRESS;Initial Catalog=OtoparkOtomasyonu;Integrated Security=True");
        DataSet dataSet = new DataSet();

        private void frmSatisListeleme_Load(object sender, EventArgs e)
        {
            SatisListele();

            Hesapla();

        }

        private void Hesapla()
        {
            baglanti.Open();
            SqlCommand sqlCommand = new SqlCommand("Select sum(tutar) from satis", baglanti);
            textBox1.Text = sqlCommand.ExecuteScalar() + " TL";
            baglanti.Close();
        }

        private void SatisListele()
        {
            baglanti.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * From Satis", baglanti);
            sqlDataAdapter.Fill(dataSet, "Satis");
            dataGridView1.DataSource = dataSet.Tables["Satis"];
            baglanti.Close();
        }
    }
}
