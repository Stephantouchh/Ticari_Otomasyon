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
    public partial class FrmAyarlar : Form
    {
        public FrmAyarlar()
        {
            InitializeComponent();
        }


        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from TBL_ADMIN", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void FrmAyarlar_Load(object sender, EventArgs e)
        {
            listele();
            txtkullaniciad.Text = "";
            txtsifre.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (btnİslem.Text == "Kaydet")
            {
                SqlCommand komut = new SqlCommand("insert into tbl_admın values(@p1,@p2)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtkullaniciad.Text);
                komut.Parameters.AddWithValue("@p2", txtsifre.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Admin Verileri Sisteme Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            if (btnİslem.Text == "Güncelle")
            {
                SqlCommand komut1 = new SqlCommand("update tbl_admın set sifre=@p2 where kullaniciad=@p1",bgl.baglanti());
                komut1.Parameters.AddWithValue("@p1", txtkullaniciad.Text);           
                komut1.Parameters.AddWithValue("@p2", txtsifre.Text);
                komut1.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Admin Verileri Güncellendi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                listele();
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtkullaniciad.Text = dr["Kullaniciad"].ToString();
                txtsifre.Text = dr["sifre"].ToString();
            }
        }

        private void txtkullaniciad_EditValueChanged(object sender, EventArgs e)
        {
            if (txtkullaniciad.Text != "")
            {
                btnİslem.Text = "Güncelle";
                btnİslem.BackColor = Color.GreenYellow;
            }
            else
            {
                btnİslem.Text = "Kaydet";
                btnİslem.BackColor = Color.MediumTurquoise;
            }
        }
    }
}
