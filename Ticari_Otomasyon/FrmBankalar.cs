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
    public partial class FrmBankalar : Form
    {
        public FrmBankalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();


        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("execute bankabilgileri ", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
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


        void firmalistesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select ID,AD from tbl_fırmalar", bgl.baglanti());
            da.Fill(dt);
            lookUpEdit1.Properties.ValueMember = "ID";
            lookUpEdit1.Properties.DisplayMember = "AD";
            lookUpEdit1.Properties.DataSource = dt;
        }

        void temizle()
        {
            txtbankaadi.Text = "";
            lookUpEdit1.Text = "";
            txthesapno.Text = "";
            txthesapturu.Text = "";
            txtiban.Text = "";
            txtid.Text = "";
            txtsube.Text = "";
            txtyetkili.Text = "";
            cmbil.Text = "";
            cmbilce.Text = "";
            msktarih.Text = "";
            msktel.Text = "";
            lookUpEdit1.Text = "";
        }

        private void FrmBankalar_Load(object sender, EventArgs e)
        {
            listele();

            sehirlistesi();

            firmalistesi();

            temizle();
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

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtid.Text = dr["ID"].ToString();
                txtbankaadi.Text = dr["BANKAADI"].ToString();
                cmbil.Text = dr["IL"].ToString();
                cmbilce.Text = dr["ILCE"].ToString();
                txtsube.Text = dr["SUBE"].ToString();
                txtiban.Text = dr["IBAN"].ToString();
                txthesapno.Text = dr["HESAPNO"].ToString();
                txtyetkili.Text = dr["YETKILI"].ToString();
                msktel.Text = dr["TELEFON"].ToString();
                msktarih.Text = dr["TARIH"].ToString();
                txthesapturu.Text = dr["HESAPTURU"].ToString();
                //lookUpEdit1.Text = dr["FIRMAID"].ToString();
            }
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into tbl_bankalar (bankaadı,ıl,ılce,sube,ıban,hesapno,yetkılı,telefon,tarıh,hesapturu,fırmaıd) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtbankaadi.Text);
            komut.Parameters.AddWithValue("@p2", cmbil.Text);
            komut.Parameters.AddWithValue("@p3", cmbilce.Text);
            komut.Parameters.AddWithValue("@p4", txtsube.Text);
            komut.Parameters.AddWithValue("@p5", txtiban.Text);
            komut.Parameters.AddWithValue("@p6", txthesapno.Text);
            komut.Parameters.AddWithValue("@p7", txtyetkili.Text);
            komut.Parameters.AddWithValue("@p8", msktel.Text);
            komut.Parameters.AddWithValue("@p9", msktarih.Text);
            komut.Parameters.AddWithValue("@p10", txthesapturu.Text);
            komut.Parameters.AddWithValue("@p11", lookUpEdit1.EditValue);
            komut.ExecuteNonQuery();
            listele();
            bgl.baglanti().Close();
            MessageBox.Show("Banka Bilgileri Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from tbl_bankalar where ıd=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtid.Text);
            komut.ExecuteNonQuery();
            listele();
            bgl.baglanti().Close();
            MessageBox.Show("Banka Bilgisi Listeden Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            temizle();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update tbl_bankalar set BANKAADI=@P1,IL=@P2,ILCE=@P3,SUBE=@P4,IBAN=@P5,HESAPNO=@P6,YETKILI=@P7,TELEFON=@P8,TARIH=@P9,HESAPTURU=@P10,FIRMAID=@P11 WHERE ID=@P12", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtbankaadi.Text);
            komut.Parameters.AddWithValue("@p2", cmbil.Text);
            komut.Parameters.AddWithValue("@p3", cmbilce.Text);
            komut.Parameters.AddWithValue("@p4", txtsube.Text);
            komut.Parameters.AddWithValue("@p5", txtiban.Text);
            komut.Parameters.AddWithValue("@p6", txthesapno.Text);
            komut.Parameters.AddWithValue("@p7", txtyetkili.Text);
            komut.Parameters.AddWithValue("@p8", msktel.Text);
            komut.Parameters.AddWithValue("@p9", msktarih.Text);
            komut.Parameters.AddWithValue("@p10", txthesapturu.Text);
            komut.Parameters.AddWithValue("@p11", lookUpEdit1.EditValue);
            komut.Parameters.AddWithValue("@p12", txtid.Text);
            komut.ExecuteNonQuery();
            listele();
            bgl.baglanti().Close();
            MessageBox.Show("Banka Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}

