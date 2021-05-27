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

namespace Ticari_Otomasyon
{
    public partial class FrmAdmin : Form
    {
        public FrmAdmin()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        private void button1_Click_1(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select * from tbl_ADMIN where kullaniciad=@p1 and sifre=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtkullanici.Text);
            komut.Parameters.AddWithValue("@p2", txtsifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                FrmAnaModul fr = new FrmAnaModul();
                fr.kullanici = txtkullanici.Text;
                fr.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı Adı veya Şifre", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            bgl.baglanti().Close();
        }

        private void button1_MouseHover_1(object sender, EventArgs e)
        {
            btngiris.BackColor = Color.Yellow;
        }

        private void button1_MouseLeave_1(object sender, EventArgs e)
        {
            btngiris.BackColor = Color.LightSeaGreen;
        }

        private void FrmAdmin_Load(object sender, EventArgs e)
        {

        }
    }
}
