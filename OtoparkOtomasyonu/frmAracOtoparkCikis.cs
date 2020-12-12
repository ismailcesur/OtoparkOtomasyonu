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
    public partial class frmAracOtoparkCikis : Form
    {
        public frmAracOtoparkCikis()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-E8LRUC4\SQLEXPRESS;Initial Catalog=OtoparkOtomasyonu;Integrated Security=True");


        private void frmAracOtoparkCikis_Load(object sender, EventArgs e)
        {
            PlakalariGetir();
            DoluParkYerleriGetir();
            timer1.Start();
        }

        private void DoluParkYerleriGetir()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From AracDurumu Where durumu='Dolu'", baglanti);
            SqlDataReader read = komut.ExecuteReader();

            while (read.Read())
            {
                cmbParkYeri.Items.Add(read["parkyeri"].ToString());
            }
            baglanti.Close();
        }

        private void PlakalariGetir()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From AracOtoparkKaydi", baglanti);
            SqlDataReader read = komut.ExecuteReader();

            while (read.Read())
            {
                cmbPlakaParkBilgileri.Items.Add(read["plaka"].ToString());
            }
            baglanti.Close();
        }

        private void cmbPlakaParkBilgileri_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From AracOtoparkKaydi Where plaka='" + cmbPlakaParkBilgileri.SelectedItem + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();

            while (read.Read())
            {
                txtParkYeriParkBilgileri.Text = read["parkyeri"].ToString();
            }
            baglanti.Close();
        }

        private void cmbParkYeri_SelectedIndexChanged(object sender, EventArgs e)
        {
            ParkyerindenKayıtlıBilgileriGetir();

        }

        private void ParkyerindenKayıtlıBilgileriGetir()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From AracOtoparkKaydi Where parkyeri='" + cmbParkYeri.SelectedItem + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();

            while (read.Read())
            {
                txtParkYeri.Text = read["parkyeri"].ToString();
                txtTc.Text = read["tc"].ToString();
                txtAd.Text = read["ad"].ToString();
                txtSoyad.Text = read["soyad"].ToString();
                txtMarka.Text = read["marka"].ToString();
                txtSeri.Text = read["seri"].ToString();
                txtRenk.Text = read["renk"].ToString();
                txtPlaka.Text = read["plaka"].ToString();
                lblGelisTarihi.Text = read["tarih"].ToString();
            }
            baglanti.Close();

            DateTime Gelis, Cikis;

            Gelis = DateTime.Parse(lblGelisTarihi.Text);
            Cikis = DateTime.Parse(lblCikisTarihi.Text);
            TimeSpan fark;
            fark = Cikis - Gelis;
            lblSure.Text = fark.TotalHours.ToString("0.00");
            lblToplamTutar.Text = (double.Parse(lblSure.Text) * 0.75).ToString("0.00");

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblCikisTarihi.Text = DateTime.Now.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand kayitsil = new SqlCommand("Delete From AracOtoparkKaydi Where plaka='" + txtPlaka.Text + "'", baglanti);
            kayitsil.ExecuteNonQuery();

            SqlCommand parkyeribosalt = new SqlCommand("update AracDurumu set durumu='Boş' Where parkyeri='" + txtParkYeri.Text + "'", baglanti);
            parkyeribosalt.ExecuteNonQuery();

            SqlCommand komut3 = new SqlCommand($@"insert into Satis
                                                    (parkyeri, plaka,gelistarihi,cikistarihi,süre,tutar)
                                               values
                                                    (@parkyeri, @plaka,@gelistarihi,@cikistarihi,@süre,@tutar)", baglanti);
            komut3.Parameters.AddWithValue("@parkyeri", txtParkYeri.Text);
            komut3.Parameters.AddWithValue("@plaka", txtPlaka.Text);
            komut3.Parameters.AddWithValue("@gelistarihi", lblGelisTarihi.Text);
            komut3.Parameters.AddWithValue("@cikistarihi", lblCikisTarihi.Text);
            komut3.Parameters.AddWithValue("@süre", double.Parse(lblSure.Text));
            komut3.Parameters.AddWithValue("@tutar", double.Parse(lblToplamTutar.Text));
            komut3.ExecuteNonQuery();

            baglanti.Close();
            MessageBox.Show("Araç çıkışı yapıldı", "Çıkış");

            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                    cmbPlakaParkBilgileri.Items.Clear();
                }
            }

            cmbParkYeri.Items.Clear();
            cmbParkYeri.Text = "";
            txtParkYeriParkBilgileri.Text = "";
            PlakalariGetir();
            DoluParkYerleriGetir();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
