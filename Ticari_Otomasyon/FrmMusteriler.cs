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
    public partial class FrmMusteriler : Form
    {
        public FrmMusteriler()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_musterıler order by ıd asc", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }


        void temizle()
        {
            txtad.Text = "";
            txtid.Text = "";
            txtmail.Text = "";
            txtsoyad.Text = "";
            txtvergid.Text = "";
            msktc.Text = "";
            msktel1.Text = "";
            msktel2.Text = "";
            rchadres.Text = "";
            cmbil.Text = "";
            cmbilce.Text = "";
        }

        void sehirlistesi()
        {
            SqlCommand komut = new SqlCommand("select sehır from tbl_ıller", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbil.Properties.Items.Add(dr[0]);

            }
            bgl.baglanti().Close();
        }
        private void FrmMusteriler_Load(object sender, EventArgs e)
        {
            listele();
            
            sehirlistesi();

            temizle();

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtid.Text = dr["ID"].ToString();
                txtad.Text = dr["AD"].ToString();
                txtsoyad.Text = dr["SOYAD"].ToString();
                msktel1.Text = dr["TELEFON"].ToString();
                msktel2.Text = dr["TELEFON2"].ToString();
                msktc.Text = dr["TC"].ToString();
                txtmail.Text = dr["MAIL"].ToString();
                cmbil.Text = dr["IL"].ToString();
                cmbilce.Text = dr["ILCE"].ToString();
                rchadres.Text = dr["ADRES"].ToString();
                txtvergid.Text = dr["VERGIDAIRE"].ToString();
            }
        }

        private void cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbilce.Properties.Items.Clear();

            SqlCommand komut = new SqlCommand("select ılce from tbl_ılceler where sehır=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbil.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbilce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into tbl_musterıler (ad,soyad,telefon,telefon2,tc,maıl,ıl,ılce,adres,vergıdaıre) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtad.Text);
            komut.Parameters.AddWithValue("@p2", txtsoyad.Text);
            komut.Parameters.AddWithValue("@p3", msktel1.Text);
            komut.Parameters.AddWithValue("@p4", msktel2.Text);
            komut.Parameters.AddWithValue("@p5", msktc.Text);
            komut.Parameters.AddWithValue("@p6", txtmail.Text);
            komut.Parameters.AddWithValue("@p7", cmbil.Text);
            komut.Parameters.AddWithValue("@p8", cmbilce.Text);
            komut.Parameters.AddWithValue("@p9", rchadres.Text);
            komut.Parameters.AddWithValue("@p10", txtvergid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri Sisteme Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
           
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from tbl_musterıler where ıd=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            listele();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update tbl_musterıler set ad=@p1,soyad=@p2,telefon=@p3,telefon2=@p4,tc=@p5,maıl=@p6,ıl=@p7,ılce=@p8,vergıdaıre=@p9,adres=@p10 where ıd=@p11", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtad.Text);
            komut.Parameters.AddWithValue("@p2", txtsoyad.Text);
            komut.Parameters.AddWithValue("@p3", msktel1.Text);
            komut.Parameters.AddWithValue("@p4", msktel2.Text);
            komut.Parameters.AddWithValue("@p5", msktc.Text);
            komut.Parameters.AddWithValue("@p6", txtmail.Text);
            komut.Parameters.AddWithValue("@p7", cmbil.Text);
            komut.Parameters.AddWithValue("@p8", cmbilce.Text);
            komut.Parameters.AddWithValue("@p9", txtvergid.Text);
            komut.Parameters.AddWithValue("@p10", rchadres.Text);
            komut.Parameters.AddWithValue("@p11", txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele(); 
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
