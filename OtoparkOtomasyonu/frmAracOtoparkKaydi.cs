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

namespace OtoparkOtomasyonu
{
    public partial class frmAracOtoparkKaydi : Form
    {
        public frmAracOtoparkKaydi()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-E8LRUC4\SQLEXPRESS;Initial Catalog=OtoparkOtomasyonu;Integrated Security=True");


        private void frmAracOtoparkKaydi_Load(object sender, EventArgs e)
        {
            BosAraclar();
            Markalar();

        }

        private void Markalar()
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

        private void BosAraclar()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From AracDurumu Where durumu='Boş'", baglanti);
            SqlDataReader read = komut.ExecuteReader();

            while (read.Read())
            {
                cmbParkYeri.Items.Add(read["parkyeri"].ToString());
            }
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand($@"insert into AracOtoparkKaydi
                                                (tc,ad,soyad,telefon,email,plaka,marka,seri,renk,parkyeri,tarih)
                                                values 
                                                (@tc,@ad,@soyad,@telefon,@email,@plaka,@marka,@seri,@renk,@parkyeri,@tarih)", baglanti);
            komut.Parameters.AddWithValue("@tc", txtTc.Text);
            komut.Parameters.AddWithValue("@ad", txtAd.Text);
            komut.Parameters.AddWithValue("@soyad", txtSoyad.Text);
            komut.Parameters.AddWithValue("@Telefon", txtTelefon.Text);
            komut.Parameters.AddWithValue("@email", txtEmail.Text);
            komut.Parameters.AddWithValue("@plaka", txtPlaka.Text);
            komut.Parameters.AddWithValue("@marka", cmbMarka.Text);
            komut.Parameters.AddWithValue("@seri", cmbSeri.Text);
            komut.Parameters.AddWithValue("@renk", txtRenk.Text);
            komut.Parameters.AddWithValue("@parkyeri", cmbParkYeri.Text);
            komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
            komut.ExecuteNonQuery();

            //Park yerini doluya çevir
            SqlCommand komut2 = new SqlCommand("Update AracDurumu set durumu='Dolu' Where parkyeri='" + cmbParkYeri.SelectedItem + "'", baglanti);
            komut2.ExecuteNonQuery();

            baglanti.Close();
            MessageBox.Show("Araç kaydı oluşturuldu", "Kayıt");
            cmbParkYeri.Items.Clear();
            BosAraclar();
            cmbMarka.Items.Clear();
            Markalar();
            cmbSeri.Items.Clear();

            GroupBoxlarıBosalt();
        }

        private void GroupBoxlarıBosalt()
        {
            //Kişi groupbox'ı dolaş
            foreach (Control item in grupKisi.Controls)
            {
                //Groupbox'ın içindeki textbox'ları boşalt
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
            foreach (Control item in grupArac.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }

            foreach (Control item in grupArac.Controls)
            {
                if (item is ComboBox)
                {
                    item.Text = "";
                }
            }
        }

        private void btnMarka_Click(object sender, EventArgs e)
        {
            frmMarka frmMarka = new frmMarka();
            frmMarka.Show();
        }

        private void btnSeri_Click(object sender, EventArgs e)
        {
            frmSeri frmSeri = new frmSeri();
            frmSeri.Show();
        }

        private void cmbMarka_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From SeriBilgileri where Marka='"+ cmbMarka.SelectedItem+"' ", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            cmbSeri.Items.Clear();
           

            while (read.Read())
            {
                cmbSeri.Items.Add(read["seri"].ToString());
                cmbSeri.SelectedIndex = 0;
            }
          
          
            baglanti.Close();
        }
    }
}
