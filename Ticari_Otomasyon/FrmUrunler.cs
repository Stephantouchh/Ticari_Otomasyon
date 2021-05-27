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
    public partial class FrmUrunler : Form
    {
        public FrmUrunler()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_urunler order by ıd asc", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void FrmUrunler_Load(object sender, EventArgs e)
        {
            listele();

            temizle();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_URUNLER(urunad,marka,model,yıl,adet,alısfıyat,satısfıyat,detay) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtad.Text);
            komut.Parameters.AddWithValue("@p2", txtmarka.Text);
            komut.Parameters.AddWithValue("@p3", txtmodel.Text);
            komut.Parameters.AddWithValue("@p4", mskyil.Text);
            komut.Parameters.AddWithValue("@p5", int.Parse((nudadet.Value).ToString()));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(txtalisfiyat.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(txtsatis.Text));
            komut.Parameters.AddWithValue("@p8", rchdetay.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Sisteme Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from tbl_urunler where ıd=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            listele();

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtid.Text = dr["ID"].ToString();
            txtad.Text = dr["URUNAD"].ToString();
            txtmarka.Text = dr["MARKA"].ToString();
            txtmodel.Text = dr["MODEL"].ToString();
            mskyil.Text = dr["YIL"].ToString();
            nudadet.Value = decimal.Parse(dr["ADET"].ToString());
            txtalisfiyat.Text = dr["ALISFIYAT"].ToString();
            txtsatis.Text = dr["SATISFIYAT"].ToString();
            rchdetay.Text = dr["DETAY"].ToString();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update tbl_urunler set urunad=@p1,marka=@p2,model=@p3,yıl=@p4,adet=@p5,alısfıyat=@p6,satısfıyat=@p7,detay=@p8 where ıd=@p9", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtad.Text);
            komut.Parameters.AddWithValue("@p2", txtmarka.Text);
            komut.Parameters.AddWithValue("@p3", txtmodel.Text);
            komut.Parameters.AddWithValue("@p4", mskyil.Text);
            komut.Parameters.AddWithValue("@p5", int.Parse((nudadet.Value).ToString()));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(txtalisfiyat.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(txtsatis.Text));
            komut.Parameters.AddWithValue("@p8", rchdetay.Text);
            komut.Parameters.AddWithValue("@p9", txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }

        void temizle()
        {
            txtad.Text = "";
            txtalisfiyat.Text = "";
            txtid.Text = "";
            txtmarka.Text = "";
            txtmodel.Text = "";
            txtsatis.Text = "";
            mskyil.Text = "";
            rchdetay.Text = "";
            nudadet.Value = 0;
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
