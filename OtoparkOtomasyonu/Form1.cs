using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtoparkOtomasyonu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void otoparkYerleriToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAracOtoparkKaydi frmAracOtoparkKaydi = new frmAracOtoparkKaydi();
            frmAracOtoparkKaydi.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmAracOtoparkYerleri frmAracOtoparkYerleri = new frmAracOtoparkYerleri();
            frmAracOtoparkYerleri.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmAracOtoparkCikis frmAracOtoparkCikis = new frmAracOtoparkCikis();
            frmAracOtoparkCikis.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmSatisListeleme frmSatisListeleme = new frmSatisListeleme();
            frmSatisListeleme.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
