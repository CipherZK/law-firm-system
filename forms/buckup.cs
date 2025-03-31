using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dip.Forms
{
    public partial class buckup : Form
    {
        SqlConnection con = new SqlConnection("server = DESKTOP-S23NBQ7; database = адвокатская; integrated security = true");

        public buckup()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dlg.SelectedPath;
                buc.Enabled = true;
            }
        }

        private void buc_Click(object sender, EventArgs e)
        {
            //
            string database = con.Database.ToString();
            if (textBox1.Text == string.Empty)
            {
                MessageBox.Show("Please enter backup file location");
            }
            else
            {
                //backup database excel to disk = 'C:\нов\Excel.bak with encryption ( algorithm = AES_256, server certificate = Zhan)
                string cmd = "BACKUP DATABASE [" + database + "] TO DISK= '" + textBox1.Text + " with encryption ( algorithm = AES_256, server certificate = Zhan)'";
                //string cmd = "BACKUP DATABASE [" + database + "] TO DISK= '" + textBox1.Text + "\\" + "database" + "-" + DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss") + ".bak'";
                con.Open();
                SqlCommand command = new SqlCommand(cmd, con);
                command.ExecuteNonQuery();
                MessageBox.Show("Database backup done successfuly");
                buc.Enabled = false;

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dlg.SelectedPath;
                buc.Enabled = true;
            }
        }
    }
}
