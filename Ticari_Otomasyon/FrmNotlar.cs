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
    public partial class FrmNotlar : Form
    {
        public FrmNotlar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_notlar order by ıd asc", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            txtbaslik.Text = "";
            txthitap.Text = "";
            txtid.Text = "";
            txtolusturan.Text = "";
            msktarih.Text = "";
            msksaat.Text = "";
            rchdetay.Text = "";
        }

        private void FrmNotlar_Load(object sender, EventArgs e)
        {
            listele();

            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtbaslik.Text = dr["BASLIK"].ToString();
                txthitap.Text = dr["HITAP"].ToString();
                txtid.Text = dr["ID"].ToString();
                txtolusturan.Text = dr["OLUSTURAN"].ToString();
                msksaat.Text = dr["SAAT"].ToString();
                msktarih.Text = dr["TARIH"].ToString();
                rchdetay.Text = dr["DETAY"].ToString();
            }
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from tbl_notlar where ıd=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            listele();
            MessageBox.Show("Not Bilgisi Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into tbl_notlar(tarıh,saat,baslık,olusturan,hıtap,detay) values (@p1,@p2,@p3,@p4,@p5,@p6)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", msktarih.Text);
            komut.Parameters.AddWithValue("@p2", msksaat.Text);
            komut.Parameters.AddWithValue("@p3", txtbaslik.Text);
            komut.Parameters.AddWithValue("@p4", txtolusturan.Text);
            komut.Parameters.AddWithValue("@p5", txthitap.Text);
            komut.Parameters.AddWithValue("@p6", rchdetay.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            listele();
            MessageBox.Show("Not Bilgisi Sisteme Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update tbl_notlar set tarıh=@p1,saat=@p2,baslık=@p3,olusturan=@p4,hıtap=@p5,detay=@p6 where ıd=@p7", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", msktarih.Text);
            komut.Parameters.AddWithValue("@p2", msksaat.Text);
            komut.Parameters.AddWithValue("@p3", txtbaslik.Text);
            komut.Parameters.AddWithValue("@p4", txtolusturan.Text);
            komut.Parameters.AddWithValue("@p5", txthitap.Text);
            komut.Parameters.AddWithValue("@p6", rchdetay.Text);
            komut.Parameters.AddWithValue("@p7", txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            listele();
            MessageBox.Show("Not Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmNotDetay fr = new FrmNotDetay();

            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                fr.metin = dr["DETAY"].ToString();
            }
            fr.Show();
        }
    }
}
