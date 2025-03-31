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
using Dip;

namespace Diploma.Pages
{
    public partial class createSudDela : Form
    {
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataTable dt;
        //CustomerInfo
        string connectionString = connection.str_connect;
        SqlConnection con = new SqlConnection("server = DESKTOP-S23NBQ7; database = адвокатская; integrated security = true");

        public createSudDela()
        {
            InitializeComponent();
            display();
        }

        public void display()
        {
            try
            {

                dt = new DataTable();
                con.Open();
                adpt = new SqlDataAdapter("select * from дела", con);
                adpt.Fill(dt);
                dgvContacts.DataSource = dt;
                con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void clear()
        {
            /*txtiDadvokat.Text = "";
            txtName.Text = "";
            txtDateOfBirth.Text = "";
            txtStazh.Text = "";
            txtIIN.Text = "";
            txtNumberPass.Text = "";
            txtAddress.Text = "";
            txtTel.Text = "";*/
        }
        private void button1_Click(object sender, EventArgs e)
        {
           /* if (txtiDadvokat.Text == "" || txtName.Text == "" || txtDateOfBirth.Text == "" || txtNumberPass.Text == "" || txtIIN.Text == "" || txtAddress.Text == "" || txtTel.Text == "")
            {
                MessageBox.Show("Please Fill in the Blanks ");

            }
            else
            {

                try
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {

                        con.Open();

                        cmd = new SqlCommand("insert into адвокат ([Код Адвоката], [ФИО Адвоката], [Дата рождения], Стаж, ИИН,[Номер паспорта], Адрес, Телефон) values ('" + txtiDadvokat.Text + "', '" + txtName.Text + "', '" + txtDateOfBirth.Text + "', '" + txtNumberPass.Text + "', '" + txtIIN.Text + "', '" + txtAddress.Text + "', '" + txtTel.Text + "')", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("You Data has Benn Saved in the Database ");
                        clear();
                        display();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }*/
        }

        private void dgvContacts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            /*txtiDadvokat.Text = Convert.ToString(dataGridView1[0, Convert.ToInt32(dataGridView1.CurrentRow.Index)].Value);
            txtName.Text = Convert.ToString(dataGridView1[1, Convert.ToInt32(dataGridView1.CurrentRow.Index)].Value);
            //txtBookID.Text = Convert.ToString(dgvContacts[2, Convert.ToInt32(dgvContacts.CurrentRow.Index)].Value);
            txtDateOfBirth.Text = Convert.ToString(dataGridView1[2, Convert.ToInt32(dataGridView1.CurrentRow.Index)].Value);
            txtNumberPass.Text = Convert.ToString(dataGridView1[3, Convert.ToInt32(dataGridView1.CurrentRow.Index)].Value);
            txtIIN.Text = Convert.ToString(dataGridView1[4, Convert.ToInt32(dataGridView1.CurrentRow.Index)].Value);
            txtAddress.Text = Convert.ToString(dataGridView1[5, Convert.ToInt32(dataGridView1.CurrentRow.Index)].Value);
            txtTel.Text = Convert.ToString(dataGridView1[6, Convert.ToInt32(dataGridView1.CurrentRow.Index)].Value);
            */
        }

        private void SearchTxt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                SearchFromTxt();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
        }

        void SearchFromTxt()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("BooktViewOrSearch", con);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("@names", SearchTxt.Text.Trim());
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                dgvContacts.DataSource = dtbl;
                dgvContacts.Columns[0].Visible = false;
                con.Close();
            }

            //fioSearch
        }

        private void txtAdvokatSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    SqlDataAdapter sqlDa = new SqlDataAdapter("advokatSearch", con);
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("@names", txtAdvokatSearch.Text.Trim());
                    DataTable dtbl = new DataTable();
                    sqlDa.Fill(dtbl);
                    dgvContacts.DataSource = dtbl;
                    dgvContacts.Columns[0].Visible = false;
                    con.Close();
                }
            }
        }

        private void guna2TextBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    SqlDataAdapter sqlDa = new SqlDataAdapter("advokatSearch", con);
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("@names", txtAdvokatSearch.Text.Trim());
                    DataTable dtbl = new DataTable();
                    sqlDa.Fill(dtbl);
                    dgvContacts.DataSource = dtbl;
                    dgvContacts.Columns[0].Visible = false;
                    con.Close();
                }
            }
        }

        private void guna2TextBox3_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Enter)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    SqlDataAdapter sqlDa = new SqlDataAdapter("numberDogovora", con);
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("@num", guna2TextBox3.Text.Trim());
                    DataTable dtbl = new DataTable();
                    sqlDa.Fill(dtbl);
                    dgvContacts.DataSource = dtbl;
                    dgvContacts.Columns[0].Visible = false;
                    con.Close();
                }
            }
        }

        private void clients_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    SqlDataAdapter sqlDa = new SqlDataAdapter("nameClients", con);
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("@names", clients.Text.Trim());
                    DataTable dtbl = new DataTable();
                    sqlDa.Fill(dtbl);
                    dgvContacts.DataSource = dtbl;
                    dgvContacts.Columns[0].Visible = false;
                    con.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
