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

namespace Dip.Tables
{
    public partial class advokat : Form
    {
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataTable dt;
        //CustomerInfo
        string connectionString = connection.str_connect;

        public advokat()
        {
            InitializeComponent();
            display();
        }

        public void clear()
        {
            txtiDadvokat.Text = "";
            txtName.Text = "";
            txtDateOfBirth.Text = "";
            txtStazh.Text = "";
            txtIIN.Text = "";
            txtNumberPass.Text = "";
            txtAddress.Text = "";
            txtTel.Text = "";
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
                    dataGridView1.DataSource = dt;
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("вы действительно хотите обновить?", "Message", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)

                try
                {

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        cmd = new SqlCommand("update адвокат set [Код Адвоката]='" + txtiDadvokat.Text + "', [ФИО Адвоката]='" + txtName.Text + "', [Дата рождения]='" + txtDateOfBirth.Text + "', Стаж='" + txtStazh.Text + "', ИИН='" + txtIIN.Text + "', [Номер паспорта]='" + txtNumberPass.Text + "', Адрес='" + txtAddress.Text + "', [Телефон номер]='" + txtTel.Text + "' where [Код Адвоката]='" + txtiDadvokat.Text + "'", con);
                        //cmd = new SqlCommand("update sale set date '" + txtDate.Text + "', book_id '" + txtBookID.Text + "', number_of_instances '" + txtPages + "', payment_amount '" + txtSum + "', employee_id '" + txtEmployeeID.Text + "', orderr='" + priznak + "', orderr_number '" + txtNumOrderr + "' where id=" + id + "'", con);

                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show(" You Data Has Been Updated ");
                        display();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (txtiDadvokat.Text == "" || txtName.Text == "" || txtDateOfBirth.Text == "" || txtNumberPass.Text == "" || txtIIN.Text == "" || txtAddress.Text == "" || txtTel.Text == "")
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

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("вы действительно хотите удалить?", "Message", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)

                try
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        cmd = new SqlCommand("delete from адвокат where [Код Адвоката]= '" + txtiDadvokat + "'", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Your Record Has Been Deleted ");
                        display();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            txtiDadvokat.Text = Convert.ToString(dataGridView1[0, Convert.ToInt32(dataGridView1.CurrentRow.Index)].Value);
            txtName.Text = Convert.ToString(dataGridView1[1, Convert.ToInt32(dataGridView1.CurrentRow.Index)].Value);
            //txtBookID.Text = Convert.ToString(dgvContacts[2, Convert.ToInt32(dgvContacts.CurrentRow.Index)].Value);
            txtDateOfBirth.Text = Convert.ToString(dataGridView1[2, Convert.ToInt32(dataGridView1.CurrentRow.Index)].Value);
            txtNumberPass.Text = Convert.ToString(dataGridView1[3, Convert.ToInt32(dataGridView1.CurrentRow.Index)].Value);
            txtIIN.Text = Convert.ToString(dataGridView1[4, Convert.ToInt32(dataGridView1.CurrentRow.Index)].Value);
            txtAddress.Text = Convert.ToString(dataGridView1[5, Convert.ToInt32(dataGridView1.CurrentRow.Index)].Value);
            txtTel.Text = Convert.ToString(dataGridView1[6, Convert.ToInt32(dataGridView1.CurrentRow.Index)].Value);

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }
    }
}
