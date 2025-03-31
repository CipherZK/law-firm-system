//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Windows.Forms;
//using System.Net.Sockets;
//using System.Net;
//using System.IO;
//using System.Windows.Forms;
//using System.Net.Mail;
//using Diploma.Tables;
//using System.Runtime.Remoting.Messaging;
//using Diploma.Forms;

//namespace Diploma.Pages
//{
//    public partial class sv : Form
//    {
//        public delegate void Add(String Message);

//        Add mess;
//        public void AddMessage(String Message)
//        {
//            listBox1.Items.Add(Message);
//        }
//        public sv()
//        {
//            InitializeComponent();
//            mess = new Add(AddMessage);
//        }

//        private void button1_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                MailMessage mail = new MailMessage();
//                //put your SMTP address and port here.
//                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
//                //Put the email address
//                mail.From = new MailAddress("kalaubekovazh@gmail.com");

//                //Put the email where you want to send.
//                mail.To.Add("kalaubekovazh@gmail.com");

//                mail.Subject = "CheckoutPOS Exception Log";

//                StringBuilder sbBody = new StringBuilder();

//                sbBody.AppendLine("Hi Dev Team,");

//                sbBody.AppendLine("Something went wrong with CheckoutPOS");

//                sbBody.AppendLine("Here is the error log:");

//                sbBody.AppendLine("Exception: Object reference not set to an instance of an object....");

//                sbBody.AppendLine("Thanks,");

//                mail.Body = sbBody.ToString();

//                //Your log file path
//                System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(@"C:\Logs\CheckoutPOS.log");

//                mail.Attachments.Add(attachment);

//                //Your username and password!
//                SmtpServer.Credentials = new System.Net.NetworkCredential("UserName", "Password");
//                //Set Smtp Server port
//                SmtpServer.Port = 25;
//                //SmtpServer.EnableSsl = true;

//                SmtpServer.Send(mail);
//                MessageBox.Show("The exception has been sent! :)");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.ToString());
//            }
//        }
//        public string GetIP()
//        {
//            string name = Dns.GetHostName();
//            IPHostEntry entry = Dns.GetHostEntry(name);
//            IPAddress[] addr = entry.AddressList;
//            if (addr[1].ToString().Split('.').Length == 4)
//            {
//                return addr[1].ToString();
//            }
//            return addr[2].ToString();
//        }

//        public void Message(string data)
//        {
//            listBox1.BeginInvoke(mess, data);

//        }
//        IPAddress ip = IPAddress.Parse("127.0.0.1");
//        public void StartTcpServer(object state)
//        {
//            TcpListener filelistener = new TcpListener(ip, 9000);
//            filelistener.Start();
//            TcpClient client = filelistener.AcceptTcpClient();
//            Message("Client connection accepted from :" + client.Client.RemoteEndPoint + ".");
//            byte[] buffer = new byte[1500];
//            int bytesread = 1;

//            StreamWriter writer = new StreamWriter("C:\\Users\\Zhane\\Desktop\\Маг\\new.zip");  //"D:\\sample.rar");

//            while (bytesread > 0)
//            {
//                bytesread = client.GetStream().Read(buffer, 0, buffer.Length);
//                if (bytesread == 0)
//                    break;
//                writer.BaseStream.Write(buffer, 0, buffer.Length);
//                Message(bytesread + " Received. ");
//            }
//            writer.Close();

//        }

//        private void button2_Click(object sender, EventArgs e)
//        {
//            Microsoft.Win32.OpenFileDialog openFileDialog = new OpenFileDialog();

//            bool? response = openFileDialog.ShowDialog();

//            if (response == true)
//            {
//                string filepath = openFileDialog.FileName;
//                MessageBox.Show(filepath);
//            }
//        }
//        void DisconnectUser()
//        {
//            if (isConnected)
//            {
//                client.Disconnect(ID);
//                client = null;
//                tbUserName.IsEnabled = true;
//                bConnDicon.Content = "Connect";
//                isConnected = false;
//            }

//        }
//        private void sendTextBox_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.Key == Key.Enter)
//            {
//                if (client != null)
//                {
//                    client.SendMsg(tbMessage.Text, ID);
//                    tbMessage.Text = string.Empty;
//                }
//            }
//        }
//        bool isConnected = false;
//        ServiceChatClient client;
//        int ID;
//        void ConnectUser()
//        {
//            if (!isConnected)
//            {
//                client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
//                ID = client.Connect(tbUserName.Text);
//                tbUserName.IsEnabled = false;
//                bConnDicon.Content = "Disconnect";
//                isConnected = true;
//            }
//        }
//        private void button3_Click(object sender, EventArgs e)
//        {
//            if (isConnected)
//            {
//                DisconnectUser();
//            }
//            else
//            {
//                ConnectUser();
//            }
//        }
//    }
//}

