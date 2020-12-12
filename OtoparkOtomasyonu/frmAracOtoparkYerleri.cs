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
    public partial class frmAracOtoparkYerleri : Form
    {
        public frmAracOtoparkYerleri()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-E8LRUC4\SQLEXPRESS;Initial Catalog=OtoparkOtomasyonu;Integrated Security=True");

        private void frmAracOtoparkYerleri_Load(object sender, EventArgs e)
        {
            BosParkYerleri();
            DoluParkYerleri();
            ParkYerlerinePlakalarıGetir();
        }

        private void ParkYerlerinePlakalarıGetir()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From AracOtoparkKaydi", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                foreach (Control item in Controls)
                {
                    if (item is Button)
                    {
                        //Buton'daki parkyeri ile veritabanından gelen parkyeri eşitse
                        if (item.Text == read["parkyeri"].ToString())
                        {
                            //Okunan butonun text'ine plakayı yaz park yerindeki aracın plakasını yaz
                            item.Text = read["plaka"].ToString();
                        }
                    }
                }
            }
            baglanti.Close();
        }

        private void DoluParkYerleri()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From AracDurumu", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                foreach (Control item in Controls)
                {
                    if (item is Button)
                    {
                        //Park yeri P-1 ve durumu dolu ise
                        if (item.Text == read["parkyeri"].ToString() && read["durumu"].ToString() == "Dolu")
                        {
                            //Butonun rengini kırmızı yap
                            item.BackColor = Color.Red;
                        }
                    }
                }
            }
            baglanti.Close();
        }

        private void BosParkYerleri()
        {
            int sayac = 1;
            //Formdaki kontrolleri dolaş
            foreach (Control item in Controls)
            {
                //Formdaki kontroller buton ise
                if (item is Button)
                {
                    //Butonun texti P- ve o anda ki sayaç kaç ise (P-1)
                    item.Text = "P-" + sayac;
                    //Butonun name'i P- ve o anda ki sayaç kaç ise (P-1)
                    item.Name = "P-" + sayac;
                    sayac++;
                }
            }
        }
    }
}
