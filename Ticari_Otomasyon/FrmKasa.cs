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
using DevExpress.Charts;

namespace Ticari_Otomasyon
{
    public partial class FrmKasa : Form
    {
        public FrmKasa()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_gıderler", bgl.baglanti());
            da.Fill(dt);
            gridControl2.DataSource = dt;
        }

        void musterihareket()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("execute Musterihareketler", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void firmahareket()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("execute firmahareketler", bgl.baglanti());
            da.Fill(dt);
            gridControl3.DataSource = dt;
        }

        public string ad;
        private void FrmKasa_Load(object sender, EventArgs e)
        {
            lblaktifkullanici.Text = ad;

            musterihareket();

            firmahareket();

            listele();

            //Toplam Tutarı Hesaplama
            SqlCommand komut1 = new SqlCommand("select sum(tutar) from tbl_faturadetay", bgl.baglanti());
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                lblkasatoplam.Text = dr1[0].ToString() + " TL ";
            }
            bgl.baglanti().Close();

            //Son Ayın Faturaları

            SqlCommand komut2 = new SqlCommand("select (ELEKTRIK+SU+DOGALGAZ+INTERNET+EKSTRA) from TBL_GIDERLER ORDER BY ID ASC ", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                lblodemeler.Text = dr2[0].ToString() + " TL";
            }
            bgl.baglanti().Close();

            //Son ayın personel maaşları

            SqlCommand komut3 = new SqlCommand("select maaslar from tbl_gıderler order by ID asc", bgl.baglanti());
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                lblpersonelmaaslari.Text = dr3[0].ToString() + " TL";
            }
            bgl.baglanti().Close();

            //Toplam Müşteri Sayısı

            SqlCommand komut4 = new SqlCommand("select count(*) from tbl_musterıler", bgl.baglanti());
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                lblmusterisayisi.Text = dr4[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam Firma Sayısı

            SqlCommand komut5 = new SqlCommand("select count(*) from tbl_fırmalar", bgl.baglanti());
            SqlDataReader dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
                lblfirmasayisi.Text = dr5[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam Firma Şehir Sayısı

            SqlCommand komut6 = new SqlCommand("select count(distinct(ıl)) from tbl_fırmalar", bgl.baglanti());
            SqlDataReader dr6 = komut6.ExecuteReader();
            while (dr6.Read())
            {
                lblsehirsayisi.Text = dr6[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam Müşteri Firma Sayısı

            SqlCommand komut7 = new SqlCommand("select count(distinct(ıl)) from tbl_musterıler", bgl.baglanti());
            SqlDataReader dr7 = komut7.ExecuteReader();
            while (dr7.Read())
            {
                lblsehirsayisi2.Text = dr7[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam Personel Sayısı

            SqlCommand komut8 = new SqlCommand("select count(*) from tbl_personeller", bgl.baglanti());
            SqlDataReader dr8 = komut8.ExecuteReader();
            while (dr8.Read())
            {
                lblpersonelsayisi.Text = dr8[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam Stok Sayısı

            SqlCommand komut9 = new SqlCommand("select sum(adet) from TBL_URUNLER", bgl.baglanti());
            SqlDataReader dr9 = komut9.ExecuteReader();
            while (dr9.Read())
            {
                lblstoksayisi.Text = dr9[0].ToString();
            }
            bgl.baglanti().Close();
        }
        int sayac = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;
            //Elektrik
            if (sayac > 0 && sayac <= 5)
            {
                groupControl1.Text = "Elektrik";

                chartControl1.Series["Aylar"].Points.Clear();

                SqlCommand komut10 = new SqlCommand("select top 4 AY,ELEKTRIK from TBL_GIDERLER order by ıd desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }
            //Su

            if (sayac > 5 && sayac <= 10)
            {
                groupControl1.Text = "Su";
                chartControl1.Series["Aylar"].Points.Clear();

              
                SqlCommand komut11 = new SqlCommand("select top 4 AY,SU from TBL_GIDERLER order by ıd desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }
            //Doğalgaz

            if (sayac > 10 && sayac <= 15)
            {
                groupControl1.Text = "Doğalgaz";
                chartControl1.Series["Aylar"].Points.Clear();


                SqlCommand komut11 = new SqlCommand("select top 4 AY,Dogalgaz from TBL_GIDERLER order by ıd desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }
            //İnternet

            if (sayac > 15 && sayac <= 20)
            {
                groupControl1.Text = "İnternet";
                chartControl1.Series["Aylar"].Points.Clear();


                SqlCommand komut11 = new SqlCommand("select top 4 AY,ınternet from TBL_GIDERLER order by ıd desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }

            //Ekstra

            if (sayac > 20 && sayac <= 25)
            {
                groupControl1.Text = "Ekstra";
                chartControl1.Series["Aylar"].Points.Clear();


                SqlCommand komut11 = new SqlCommand("select top 4 AY,Ekstra from TBL_GIDERLER order by ıd desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac == 26)
            {
                sayac = 0;
            }

           
        }
        int sayac2;
        private void timer2_Tick(object sender, EventArgs e)
        {
            sayac2++;
            //Elektrik
            if (sayac2 > 0 && sayac2 <= 5)
            {
                groupControl11.Text = "Elektrik";

                chartControl2.Series["Aylar"].Points.Clear();

                SqlCommand komut10 = new SqlCommand("select top 4 AY,ELEKTRIK from TBL_GIDERLER order by ıd desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }
            //Su

            if (sayac2 > 5 && sayac2 <= 10)
            {
                groupControl11.Text = "Su";
                chartControl2.Series["Aylar"].Points.Clear();


                SqlCommand komut11 = new SqlCommand("select top 4 AY,SU from TBL_GIDERLER order by ıd desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }
            //Doğalgaz

            if (sayac2 > 10 && sayac2 <= 15)
            {
                groupControl11.Text = "Doğalgaz";
                chartControl2.Series["Aylar"].Points.Clear();


                SqlCommand komut11 = new SqlCommand("select top 4 AY,Dogalgaz from TBL_GIDERLER order by ıd desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }
            //İnternet

            if (sayac2 > 15 && sayac2 <= 20)
            {
                groupControl11.Text = "İnternet";
                chartControl2.Series["Aylar"].Points.Clear();


                SqlCommand komut11 = new SqlCommand("select top 4 AY,ınternet from TBL_GIDERLER order by ıd desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }

            //Ekstra

            if (sayac2 > 20 && sayac2 <= 25)
            {
                groupControl11.Text = "Ekstra";
                chartControl2.Series["Aylar"].Points.Clear();


                SqlCommand komut11 = new SqlCommand("select top 4 AY,Ekstra from TBL_GIDERLER order by ıd desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac2 == 26)
            {
                sayac2 = 0;
            }
        }
    }
}
