using Guna.UI2.WinForms;
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

namespace Dip
{
    public partial class Form1 : Form
    {
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataTable dt;
        //CustomerInfo
        string connectionString = connection.str_connect;
        //SqlConnection con = new SqlConnection("server = DESKTOP-S23NBQ7; database = bookstore; integrated security = true");
        public Form1()
        {
            InitializeComponent();
            display();
        }

        public void display()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    dt = new DataTable();
                    con.Open();
                    adpt = new SqlDataAdapter("select * from адвокат", con);
                    adpt.Fill(dt);
                    dgvContacts.DataSource = dt;
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void guna2Button9_Click(object sender, EventArgs e)
        {
            menu t = new menu();
            this.Visible = false;
            t.ShowDialog();
            this.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            menu t = new menu();
            this.Visible = false;
            t.ShowDialog();
            this.Close();
        }
        bool sidebarExpand;
   
        private void menuButton_Click(object sender, EventArgs e)
        {
            sidebarTimer.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void moveImageBox(object sender)
        {
            Guna2Button b = (Guna2Button)sender;
            imgSlide.Location = new Point(b.Location.X + 171, b.Location.Y - 15);
            imgSlide.SendToBack();
        }

        //Fields
        private Form activeForm;
   
        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
                activeForm.Close();
           
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktopPane.Controls.Add(childForm);
            this.panelDesktopPane.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitle.Text = childForm.Text;
        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            Application.OpenForms.Cast<Form>().Where(x => !(x is Form1))
            .ToList().ForEach(x => x.Close());
        }

        private void sidebarTimer_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                //if sideber is expan, minimize
                panelMenu.Width -= 10;
                if (panelMenu.Width == panelMenu.MinimumSize.Width)
                {
                    sidebarExpand = false;
                    sidebarTimer.Stop();
                }
            }
            else
            {
                panelMenu.Width += 10;
                if (panelMenu.Width == panelMenu.MaximumSize.Width)
                {
                    sidebarExpand = true;
                    sidebarTimer.Stop();
                }
            }
        }



        private void guna2Button1_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.calendar(), sender);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button8_CheckedChanged(object sender, EventArgs e)
        {
            moveImageBox(sender);
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.gmail(), sender);
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Tables.clients(), sender);
        }
        public string asd;
        private void Form1_Activated(object sender, EventArgs e)
        {
            label1.Text = "" + asd;
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            
        }

        private void btnGuides_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.buckup(), sender);
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
