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
    public partial class frmSeri : Form
    {
        public frmSeri()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-E8LRUC4\SQLEXPRESS;Initial Catalog=OtoparkOtomasyonu;Integrated Security=True");


        private void frmSeri_Load(object sender, EventArgs e)
        {
            Marka();
        }

        private void Marka()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From MarkaBilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();

            while (read.Read())
            {
                cmbMarka.Items.Add(read["marka"].ToString());
            }
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into SeriBilgileri(marka,seri)values('" + cmbMarka.Text + "','" + textBox1.Text + "')", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Markaya bağlı araç serisi eklendi");
            textBox1.Clear();
            cmbMarka.Items.Clear();
            Marka();
        }
    }
}
