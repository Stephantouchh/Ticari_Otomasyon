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
    public partial class FrmGiderler : Form
    {
        public FrmGiderler()
        {
            InitializeComponent();
        }


        sqlbaglantisi bgl = new sqlbaglantisi();


        void temizle()
        {
            txtid.Text = "";
            cmbay.Text = "";
            cmbyil.Text = "";
            txtelektrik.Text = "";
            txtsu.Text = "";
            txtdogalgaz.Text = "";
            txtinternet.Text = "";
            txtmaas.Text = "";
            txtekstra.Text = "";
            rchnotlar.Text = "";
        }


        void listele()
        {
            DataTable dt = new DataTable(); 
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_gıderler order by ıd asc", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void FrmGiderler_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            temizle();

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtid.Text = dr["ID"].ToString();
                cmbay.Text = dr["AY"].ToString();
                cmbyil.Text = dr["YIL"].ToString();
                txtelektrik.Text = dr["ELEKTRIK"].ToString();
                txtsu.Text = dr["SU"].ToString();
                txtdogalgaz.Text = dr["DOGALGAZ"].ToString();
                txtinternet.Text = dr["INTERNET"].ToString();
                txtmaas.Text = dr["MAASLAR"].ToString();
                txtekstra.Text = dr["EKSTRA"].ToString();
                rchnotlar.Text = dr["NOTLAR"].ToString();
            }
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from TBL_GIDERLER where ıd=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Gider Bilgisi Listeden Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            listele();
            temizle();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into tbl_gıderler (ay,yıl,elektrık,su,dogalgaz,ınternet,maaslar,ekstra,notlar) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbay.Text);
            komut.Parameters.AddWithValue("@p2", cmbyil.Text);
            komut.Parameters.AddWithValue("@p3", decimal.Parse(txtelektrik.Text));
            komut.Parameters.AddWithValue("@p4", decimal.Parse(txtsu.Text));
            komut.Parameters.AddWithValue("@p5", decimal.Parse(txtdogalgaz.Text));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(txtinternet.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(txtmaas.Text));
            komut.Parameters.AddWithValue("@p8", decimal.Parse(txtekstra.Text));
            komut.Parameters.AddWithValue("@p9", rchnotlar.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Gider Bilgisi Listeye Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update tbl_gıderler set ay=@p1,yıl=@p2,elektrık=@p3,su=@p4,dogalgaz=@p5,ınternet=@p6,maaslar=@p7,ekstra=@p8,notlar=@p9 where ıd=@p10", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbay.Text);
            komut.Parameters.AddWithValue("@p2", cmbyil.Text);
            komut.Parameters.AddWithValue("@p3", decimal.Parse(txtelektrik.Text));
            komut.Parameters.AddWithValue("@p4", decimal.Parse(txtsu.Text));
            komut.Parameters.AddWithValue("@p5", decimal.Parse(txtdogalgaz.Text));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(txtinternet.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(txtmaas.Text));
            komut.Parameters.AddWithValue("@p8", decimal.Parse(txtekstra.Text));
            komut.Parameters.AddWithValue("@p9", rchnotlar.Text);
            komut.Parameters.AddWithValue("@p10", txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Gider Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
            temizle();
        }
    }
}
