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
    public partial class FrmFaturaUrunDuzenleme : Form
    {
        public FrmFaturaUrunDuzenleme()
        {
            InitializeComponent();
        }
        public string urunid;

        sqlbaglantisi bgl = new sqlbaglantisi();


        private void FrmFaturaUrunDuzenleme_Load(object sender, EventArgs e)
        {
            txturunid.Text = urunid;

            SqlCommand komut = new SqlCommand("select * from tbl_faturadetay where faturaurunıd=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", urunid);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtfiyat.Text = dr[3].ToString();
                txtmiktar.Text = dr[2].ToString();
                txttutar.Text = dr[4].ToString();
                txturunad.Text = dr[1].ToString();


                bgl.baglanti().Close();
            }

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update tbl_faturadetay set urunad=@p1,mıktar=@p2,fıyat=@p3,tutar=@p4 where faturaurunıd=@p5", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txturunad.Text);
            komut.Parameters.AddWithValue("@p2", txtmiktar.Text);
            komut.Parameters.AddWithValue("@p3", decimal.Parse(txtfiyat.Text));
            komut.Parameters.AddWithValue("@p4", decimal.Parse(txttutar.Text));
            komut.Parameters.AddWithValue("@p5", txturunid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Değişiklikler Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from tbl_faturadetay where faturaurunıd=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txturunid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Bilgisi Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
