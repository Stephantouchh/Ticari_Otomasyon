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
    public partial class FrmFaturalar : Form
    {
        public FrmFaturalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_faturabılgı order by faturabılgııd asc", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            txtid.Text = "";
            txtseri.Text = "";
            txtsırano.Text = "";
            msktarih.Text = "";
            msksaat.Text = "";
            txtvergidaire.Text = "";
            txtalici.Text = "";
            txtteslimalan.Text = "";
            txtteslimeden.Text = "";
            //txturunid.Text = "";
            //txturunad.Text = "";
            //txtmiktar.Text = "";
            //txtfiyat.Text = "";
            //txttutar.Text = "";
            //txtfaturaid.Text = "";
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
           
            if (txtfaturaid.Text == "")
            {

                SqlCommand komut = new SqlCommand("insert into tbl_faturabılgı (serı,sırano,tarıh,saat,vergıdaıre,alıcı,teslımeden,teslımalan) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtseri.Text);
                komut.Parameters.AddWithValue("@p2", txtsırano.Text);
                komut.Parameters.AddWithValue("@p3", msktarih.Text);
                komut.Parameters.AddWithValue("@p4", msksaat.Text);
                komut.Parameters.AddWithValue("@p5", txtvergidaire.Text);
                komut.Parameters.AddWithValue("@p6", txtalici.Text);
                komut.Parameters.AddWithValue("@p7", txtteslimeden.Text);
                komut.Parameters.AddWithValue("@p8", txtteslimalan.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                listele();
                temizle();
                MessageBox.Show("Fatura Bilgisi Sisteme Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //Firma Carisi
            if (txtfaturaid.Text != "" && cmbcari.Text == "Firma")
            {
                double miktar, tutar, fiyat;
                fiyat = Convert.ToDouble(txtfiyat.Text);
                miktar = Convert.ToDouble(txtmiktar.Text);
                tutar = miktar * fiyat;
                txttutar.Text = tutar.ToString();
                SqlCommand komut2 = new SqlCommand("insert into tbl_faturadetay (faturaurunad,mıktar,fıyat,tutar,faturaıd) values (@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
                komut2.Parameters.AddWithValue("@p1", txturunad.Text);
                komut2.Parameters.AddWithValue("@p2", txtmiktar.Text);
                komut2.Parameters.AddWithValue("@p3", decimal.Parse(txtfiyat.Text));
                komut2.Parameters.AddWithValue("@p4", decimal.Parse(txttutar.Text));
                komut2.Parameters.AddWithValue("@p5", txtfaturaid.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();
                listele();

                SqlCommand komut3 = new SqlCommand("insert into tbl_fırmahareketler (urunıd,adet,personel,fırma,fıyat,toplam,faturaıd,tarıh) values (@h1,@h2,@h3,@h4,@h5,@h6,@h7,@h8)", bgl.baglanti());
                komut3.Parameters.AddWithValue("@h1", txturunid.Text);
                komut3.Parameters.AddWithValue("@h2", txtmiktar.Text);
                komut3.Parameters.AddWithValue("@h3", txtpersonel.Text);
                komut3.Parameters.AddWithValue("@h4", txtfirma.Text);
                komut3.Parameters.AddWithValue("@h5", decimal.Parse(txtfiyat.Text));
                komut3.Parameters.AddWithValue("@h6", decimal.Parse(txttutar.Text));
                komut3.Parameters.AddWithValue("@h7", txtfaturaid.Text);
                komut3.Parameters.AddWithValue("@h8", msktarih.Text);
                komut3.ExecuteNonQuery();
                bgl.baglanti().Close();

                SqlCommand komut4 = new SqlCommand("update tbl_urunler set adet=adet-@s1 where ıd=@s2", bgl.baglanti());
                komut4.Parameters.AddWithValue("s1", txtmiktar.Text);
                komut4.Parameters.AddWithValue("s2", txturunid.Text);
                komut4.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Faturaya Ait Ürün Sisteme Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //Müşteri Carisi
            if (txtfaturaid.Text != "" && cmbcari.Text == "Müşteri")
            {
                double miktar, tutar, fiyat;
                fiyat = Convert.ToDouble(txtfiyat.Text);
                miktar = Convert.ToDouble(txtmiktar.Text);
                tutar = miktar * fiyat;
                txttutar.Text = tutar.ToString();
                SqlCommand komut2 = new SqlCommand("insert into tbl_faturadetay (faturaurunad,mıktar,fıyat,tutar,faturaıd) values (@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
                komut2.Parameters.AddWithValue("@p1", txturunad.Text);
                komut2.Parameters.AddWithValue("@p2", txtmiktar.Text);
                komut2.Parameters.AddWithValue("@p3", decimal.Parse(txtfiyat.Text));
                komut2.Parameters.AddWithValue("@p4", decimal.Parse(txttutar.Text));
                komut2.Parameters.AddWithValue("@p5", txtfaturaid.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();
                listele();

                SqlCommand komut3 = new SqlCommand("insert into TBL_MUSTERIHAREKETLER (urunıd,adet,personel,musterı,fıyat,toplam,faturaıd,tarıh) values (@h1,@h2,@h3,@h4,@h5,@h6,@h7,@h8)", bgl.baglanti());
                komut3.Parameters.AddWithValue("@h1", txturunid.Text);
                komut3.Parameters.AddWithValue("@h2", txtmiktar.Text);
                komut3.Parameters.AddWithValue("@h3", txtpersonel.Text);
                komut3.Parameters.AddWithValue("@h4", txtfirma.Text);
                komut3.Parameters.AddWithValue("@h5", decimal.Parse(txtfiyat.Text));
                komut3.Parameters.AddWithValue("@h6", decimal.Parse(txttutar.Text));
                komut3.Parameters.AddWithValue("@h7", txtfaturaid.Text);
                komut3.Parameters.AddWithValue("@h8", msktarih.Text);
                komut3.ExecuteNonQuery();
                bgl.baglanti().Close();

                SqlCommand komut4 = new SqlCommand("update tbl_urunler set adet=adet-@s1 where ıd=@s2", bgl.baglanti());
                komut4.Parameters.AddWithValue("s1", txtmiktar.Text);
                komut4.Parameters.AddWithValue("s2", txturunid.Text);
                komut4.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Faturaya Ait Ürün Sisteme Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

            private void FrmFaturalar2_Load(object sender, EventArgs e)
        {
            listele();

            temizle();


        }
        private void btnsil_Click_1(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from tbl_faturabılgı where faturabılgııd=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            listele();
            temizle();
            MessageBox.Show("Fatura Bilgisi Sistemden Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnguncelle_Click_1(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update tbl_faturabılgı set serı=@p1,sırano=@p2,tarıh=@p3,saat=@p4,vergıdaıre=@p5,alıcı=@p6,teslımeden=@p7,teslımalan=@p8 where faturabılgııd=@p9", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtseri.Text);
            komut.Parameters.AddWithValue("@p2", txtsırano.Text);
            komut.Parameters.AddWithValue("@p3", msktarih.Text);
            komut.Parameters.AddWithValue("@p4", msksaat.Text);
            komut.Parameters.AddWithValue("@p5", txtvergidaire.Text);
            komut.Parameters.AddWithValue("@p6", txtalici.Text);
            komut.Parameters.AddWithValue("@p7", txtteslimeden.Text);
            komut.Parameters.AddWithValue("@p8", txtteslimalan.Text);
            komut.Parameters.AddWithValue("@p9", txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            listele();
            temizle();
            MessageBox.Show("Fatura Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void btntemizle_Click_1(object sender, EventArgs e)
        {
            temizle();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmFaturaUrunDetay fr = new FrmFaturaUrunDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                fr.id = dr["FATURABILGIID"].ToString();
            }
            fr.Show();
        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {

        }

        private void groupControl2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gridView1_FocusedRowChanged_1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtid.Text = dr["FATURABILGIID"].ToString();
                txtsırano.Text = dr["SIRANO"].ToString();
                txtseri.Text = dr["SERI"].ToString();
                msktarih.Text = dr["TARIH"].ToString();
                msksaat.Text = dr["SAAT"].ToString();
                txtalici.Text = dr["ALICI"].ToString();
                txtteslimeden.Text = dr["TESLIMEDEN"].ToString();
                txtteslimalan.Text = dr["TESLIMALAN"].ToString();
                txtvergidaire.Text = dr["VERGIDAIRE"].ToString();
            }
        }

        private void btnbul_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select urunad,satısFıyat from tbl_urunler where ıd=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txturunid.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txturunad.Text = dr[0].ToString();
                txtfiyat.Text = dr[1].ToString();
            }
            bgl.baglanti().Close();

        }
    }
}
