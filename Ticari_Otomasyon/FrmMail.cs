using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;


namespace Ticari_Otomasyon
{
    public partial class FrmMail : Form
    {
        public FrmMail()
        {
            InitializeComponent();
        }



        public string mail;
        private void FrmMail_Load(object sender, EventArgs e)
        {
            txtmailadres.Text = mail;
        }

        private void btngonder_Click(object sender, EventArgs e)
        {
            MailMessage mesajım = new MailMessage();
            SmtpClient istemci = new SmtpClient();
            istemci.Credentials = new System.Net.NetworkCredential("novalioglu2@gmail.com", "23042020nihat");
            istemci.Port = 587;
            istemci.Host = "smtp.gmail.com";
            istemci.EnableSsl = true;
            mesajım.To.Add(txtmailadres.Text);
            mesajım.From = new MailAddress("Mail");
            mesajım.Subject = txtkonu.Text;
            mesajım.Body = txtmesaj.Text;
            istemci.Send(mesajım);
        }
    }
}
