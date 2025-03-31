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
using System.Net;
using System.Net.Mail;
namespace Dip
{
    public partial class login : Form
    {
        string randomcode;
        public static string to;

        public login()
        {
            InitializeComponent();
        }
        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
          
        }
        private void button3_MouseUp(object sender, MouseEventArgs e)
        {
           
        }
        private void btnSendMes_Click(object sender, EventArgs e)
        {
            string connectionString = ($"Data Source=DESKTOP-S23NBQ7;Initial Catalog=адвокатская;User ID={textBox1.Text};Password={tbPassword.Text};");
            connection.str_connect = connectionString;
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                try
                {
                    cn.Open();
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                    string from, pass, messagebody;
                    Random rand = new Random();
                    randomcode = (rand.Next(999999)).ToString();
                    MailMessage message = new MailMessage();
                    to = (textBox1.Text).ToString();
                    smtp.UseDefaultCredentials = false;
                    from = "kalaubekovazh@gmail.com";
                    pass = "thfthfilrtqpvrql";//
                    messagebody = $"Your code {randomcode}";//
                    message.To.Add(to);
                    message.From = new MailAddress(from);
                    message.Body = messagebody;
                    message.Subject = "Secret Key";
                    smtp.UseDefaultCredentials = false;
                    smtp.EnableSsl = true;
                    smtp.Port = 587;        
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(from, pass);
                    try
                    {
                        smtp.Send(message);
                        MessageBox.Show("Code Send Succesfully");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Not connected, error: {ex.Message}");
                }
            }
        }

        private void btnAuth_Click(object sender, EventArgs e)
        {
            if (randomcode == (textBox2.Text).ToString())
            {
                to = textBox1.Text;
                Form1 r = new Form1(); 
                r.asd = textBox1.Text;
                this.Visible = false;
                r.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Worng Code");
            }
        }

        private void button3_MouseDown_1(object sender, MouseEventArgs e)
        {
            tbPassword.PasswordChar = '\0';
            tbPassword.UseSystemPasswordChar = false;
        }

        private void button3_MouseUp_1(object sender, MouseEventArgs e)
        {
            tbPassword.PasswordChar = '*';
            tbPassword.UseSystemPasswordChar = true;
        }

        private void guna2Button2_MouseDown(object sender, MouseEventArgs e)
        {
            tbPassword.PasswordChar = '\0';
            tbPassword.UseSystemPasswordChar = false;
        }

        private void guna2Button2_MouseUp(object sender, MouseEventArgs e)
        {
            tbPassword.PasswordChar = '*';
            tbPassword.UseSystemPasswordChar = true;
        }
        //code formСhild = new code();  //создание дочерней формы

    }
}
