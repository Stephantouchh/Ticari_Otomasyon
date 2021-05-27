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
    public partial class FrmFirmalar : Form
    {
        public FrmFirmalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();


        void firmalistesi()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_fırmalar order by ıd asc", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void carikodaciklamalar()
        {
            SqlCommand komut = new SqlCommand("select fırmakod1 from tbl_kodlar ", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                rchkod1.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();
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
            void temizle()
        {
            txtad.Text = "";
            txtid.Text = "";
            txtkod1.Text = "";
            txtkod2.Text = "";
            txtkod3.Text = "";
            txtmail.Text = "";
            txtsektor.Text = "";
            txtvergid.Text = "";
            txtyetkili.Text = "";
            txtygorev.Text = "";
            mskfax.Text = "";
            msktel1.Text = "";
            msktel2.Text = "";
            msktel3.Text = "";
            msktc.Text = "";
            rchadres.Text = "";
            txtkod1.Text = "";
            txtkod2.Text = "";
            cmbil.Text = "";
            cmbilce.Text = "";
            txtkod3.Text = "";
            txtad.Focus();
        }

        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            firmalistesi();

            temizle();

            sehirlistesi();

            carikodaciklamalar();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into tbl_fırmalar(ad,yetkılıstatu,yetkılıadsoyad,yetkılıtc,sektor,telefon1,telefon2,telefon3,maıl,fax,ıl,ılce,vergıdaıre,adres,ozelkod1,ozelkod2,ozelkod3) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtad.Text);
            komut.Parameters.AddWithValue("@p2", txtygorev.Text);
            komut.Parameters.AddWithValue("@p3", txtyetkili.Text);
            komut.Parameters.AddWithValue("@p4", msktc.Text);
            komut.Parameters.AddWithValue("@p5", txtsektor.Text);
            komut.Parameters.AddWithValue("@p6", msktel1.Text);
            komut.Parameters.AddWithValue("@p7", msktel2.Text);
            komut.Parameters.AddWithValue("@p8", msktel3.Text);
            komut.Parameters.AddWithValue("@p9", txtmail.Text);
            komut.Parameters.AddWithValue("@p10", mskfax.Text);
            komut.Parameters.AddWithValue("@p11", cmbil.Text);
            komut.Parameters.AddWithValue("@p12", cmbilce.Text);
            komut.Parameters.AddWithValue("@p13", txtvergid.Text);
            komut.Parameters.AddWithValue("@p14", rchadres.Text);
            komut.Parameters.AddWithValue("@p15", txtkod1.Text);
            komut.Parameters.AddWithValue("@p16", txtkod2.Text);
            komut.Parameters.AddWithValue("@p17", txtkod3.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma Bilgisi Sisteme Kaydedildi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            firmalistesi();
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtid.Text = dr["ID"].ToString();
                txtad.Text = dr["AD"].ToString();
                txtygorev.Text = dr["YETKILISTATU"].ToString();
                txtyetkili.Text = dr["YETKILIADSOYAD"].ToString();
                msktc.Text = dr["YETKILITC"].ToString();
                txtsektor.Text = dr["SEKTOR"].ToString();
                msktel1.Text = dr["TELEFON1"].ToString();
                msktel2.Text = dr["TELEFON2"].ToString();
                msktel3.Text = dr["TELEFON3"].ToString();
                txtmail.Text = dr["MAIL"].ToString();
                mskfax.Text = dr["FAX"].ToString();
                cmbil.Text = dr["IL"].ToString();
                cmbilce.Text = dr["ILCE"].ToString();
                txtvergid.Text = dr["VERGIDAIRE"].ToString();
                rchadres.Text = dr["ADRES"].ToString();
                txtkod1.Text = dr["OZELKOD1"].ToString();
                txtkod2.Text = dr["OZELKOD2"].ToString();
                txtkod3.Text = dr["OZELKOD3"].ToString();
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

        private void btnsil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from tbl_fırmalar where ıd=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            firmalistesi();
            MessageBox.Show("Firma Bilgisi Sistemden Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            temizle();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update tbl_fırmalar set ad=@p1,yetkılıstatu=@p2,yetkılıadsoyad=@p3,yetkılıtc=@p4,sektor=@p5,telefon1=@p6,telefon2=@p7,telefon3=@p8,maıl=@p9,ıl=@p11,ılce=@p12,fax=@p10,vergıdaıre=@p13,adres=@p14,ozelkod1=@p15,ozelkod2=@p16,ozelkod3=@p17 where ıd=@p18", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtad.Text);
            komut.Parameters.AddWithValue("@p2", txtygorev.Text);
            komut.Parameters.AddWithValue("@p3", txtyetkili.Text);
            komut.Parameters.AddWithValue("@p4", msktc.Text);
            komut.Parameters.AddWithValue("@p5", txtsektor.Text);
            komut.Parameters.AddWithValue("@p6", msktel1.Text);
            komut.Parameters.AddWithValue("@p7", msktel2.Text);
            komut.Parameters.AddWithValue("@p8", msktel3.Text);
            komut.Parameters.AddWithValue("@p9", txtmail.Text);
            komut.Parameters.AddWithValue("@p10", mskfax.Text);
            komut.Parameters.AddWithValue("@p11", cmbil.Text);
            komut.Parameters.AddWithValue("@p12", cmbilce.Text);
            komut.Parameters.AddWithValue("@p13", txtvergid.Text);
            komut.Parameters.AddWithValue("@p14", rchadres.Text);
            komut.Parameters.AddWithValue("@p15", txtkod1.Text);
            komut.Parameters.AddWithValue("@p16", txtkod2.Text);
            komut.Parameters.AddWithValue("@p17", txtkod3.Text);
            komut.Parameters.AddWithValue("@p18", txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            firmalistesi();
            temizle();
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
