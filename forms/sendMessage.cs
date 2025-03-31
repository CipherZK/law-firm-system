using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dip.Forms
{
    public partial class sendMessage : Form
    {
        public sendMessage()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            lblLocation.Text = openFileDialog1.FileName;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                string from, pass, messagebody;
                from = "kalaubekovazh@gmail.com";
                pass = "onwgyfilrtqpvrql";
                mail.From = new MailAddress(from);
                mail.To.Add(txtTo.Text);
                mail.Subject = txtTitle.Text;
                mail.Body = txtBody.Text;

                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(lblLocation.Text);
                mail.Attachments.Add(attachment);

                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential(from, pass);
                smtp.EnableSsl = true;
                smtp.Send(mail);
                MessageBox.Show("Mail has been successfully sent!", "Email sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
