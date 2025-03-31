//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using System.Data.SqlClient;
//using Z.Dapper.Plus;
//using System.IO;
//using ExcelDataReader;

//namespace Dip.Pages
//{
//    public partial class advokatForm : Form
//    { 
//        //string path = @"Data Source=DESKTOP-CK1PKGU;Initial Catalog=bookstore;User ID=Librarian;Password=123";
//        //SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-CK1PKGU;Initial Catalog=TestDb;Integrated Security=True");
//        // con;
//        SqlCommand cmd;
//        SqlDataAdapter adpt;
//        DataTable dt;
//        //CustomerInfo
//        string connectionString = connection.str_connect;

//        public advokatForm()
//        {
//            InitializeComponent();
//            //con = new SqlConnection(path);
//            login view = new login();
//            display();
//        }

//        private void button1_Click(object sender, EventArgs e)
//        {
//            if (txtBookID.Text == "" || txtName.Text == "" || txtclassifierID.Text == "" || txtnumber_of_pages.Text == "" || txtnumber_of_illustrations.Text == "")
//            {
//                MessageBox.Show("Please Fill in the Blanks ");

//            }
//            else
//            {

//                try
//                {
//                    using (SqlConnection con = new SqlConnection(connectionString))
//                    {

//                        con.Open();

//                        cmd = new SqlCommand("insert into CustomerInfo (CustomerId, CustomerName, EncryptedCustomerPhone) values ('" + txtBookID.Text + "', '" + txtName.Text + "')", con);
//                        cmd.ExecuteNonQuery();
//                        con.Close();
//                        MessageBox.Show("You Data has Benn Saved in the Database ");
//                        clear();
//                        display();
//                    }

//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show(ex.Message);
//                }

//            }
//        }
//        public void clear()
//        {
//            txtBookID.Text = "";
//            txtName.Text = "";
//            txtclassifierID.Text = "";
//            txtnumber_of_pages.Text = "";
//            txtnumber_of_illustrations.Text = "";

//        }


//        public void display()
//        {
//            try
//            {
//                using (SqlConnection con = new SqlConnection(connectionString))
//                {
//                    dt = new DataTable();
//                    con.Open();
//                    adpt = new SqlDataAdapter("select * from CustomerInfo", con);
//                    adpt.Fill(dt);
//                    dgvContacts.DataSource = dt;
//                    con.Close();
//                }

//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message);
//            }
//        }

//        public void displayFirst()
//        {
//            try
//            {
//                using (SqlConnection con = new SqlConnection(connectionString))
//                {
//                    dt = new DataTable();
//                    con.Open();
//                    adpt = new SqlDataAdapter("SELECT  *, DecryptedCustomerPhone = CONVERT(CHAR(11), DECRYPTBYASYMKEY(ASYMKEY_ID('AsymKey_TestDb'), EncryptedCustomerPhone, N'Password4@Asy')) " +
//                    " FROM CustomerInfo", con);
//                    adpt.Fill(dt);
//                    dgvContacts.DataSource = dt;
//                    con.Close();
//                }

//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message);
//            }
//        }
//        private void button2_Click(object sender, EventArgs e)
//        {
//            if (MessageBox.Show("вы действительно хотите обновить?", "Message", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)

//                try
//                {

//                    using (SqlConnection con = new SqlConnection(connectionString))
//                    {
//                        con.Open();
//                        cmd = new SqlCommand("update CustomerInfo set CustomerId='" + txtBookID.Text + "', CustomerName='" + txtName.Text + "', EncryptedCustomerPhone='" + txtclassifierID.Text + "' where CustomerId='" + txtBookID.Text + "'", con);
//                        //cmd = new SqlCommand("update sale set date '" + txtDate.Text + "', book_id '" + txtBookID.Text + "', number_of_instances '" + txtPages + "', payment_amount '" + txtSum + "', employee_id '" + txtEmployeeID.Text + "', orderr='" + priznak + "', orderr_number '" + txtNumOrderr + "' where id=" + id + "'", con);

//                        cmd.ExecuteNonQuery();
//                        con.Close();
//                        MessageBox.Show(" You Data Has Been Updated ");
//                        display();
//                    }
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show(ex.Message);
//                }
//        }

//        private void button3_Click(object sender, EventArgs e)
//        {
//            if (MessageBox.Show("вы действительно хотите удалить?", "Message", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)

//                try
//                {
//                    using (SqlConnection con = new SqlConnection(connectionString))
//                    {
//                        con.Open();
//                        cmd = new SqlCommand("delete from CustomerInfo where CustomerId= '" + txtBookID + "'", con);
//                        cmd.ExecuteNonQuery();
//                        con.Close();
//                        MessageBox.Show("Your Record Has Been Deleted ");
//                        display();
//                    }

//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show(ex.Message);
//                }
//        }
//        int binary = Convert.ToInt32("00000101", 2);
//        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
//        {

//            txtBookID.Text = Convert.ToString(dgvContacts[0, Convert.ToInt32(dgvContacts.CurrentRow.Index)].Value);
//            txtName.Text = Convert.ToString(dgvContacts[1, Convert.ToInt32(dgvContacts.CurrentRow.Index)].Value);
//            //txtBookID.Text = Convert.ToString(dgvContacts[2, Convert.ToInt32(dgvContacts.CurrentRow.Index)].Value);


//        }


//        void FillDataGridView()
//        {
//            using (SqlConnection con = new SqlConnection(connectionString))
//            {
//                if (con.State == ConnectionState.Closed)
//                    con.Open();
//                SqlDataAdapter sqlDa = new SqlDataAdapter("BooktViewOrSearch", con);
//                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
//                sqlDa.SelectCommand.Parameters.AddWithValue("@names", txtSearch.Text.Trim());
//                DataTable dtbl = new DataTable();
//                sqlDa.Fill(dtbl);
//                dgvContacts.DataSource = dtbl;
//                dgvContacts.Columns[0].Visible = false;
//                con.Close();
//            }

//            //fioSearch
//        }


//        private void btnSearch_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                FillDataGridView();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message, "Error Message");
//            }
//        }

//        private void button4_Click(object sender, EventArgs e)
//        {

//            try
//            {
//                using (SqlConnection con = new SqlConnection(connectionString))
//                {

//                    con.Open();
//                    cmd = new SqlCommand("USE TestDb  SELECT  *, DecryptedCustomerPhone = CONVERT(CHAR(11), DECRYPTBYASYMKEY(ASYMKEY_ID('AsymKey_TestDb'), EncryptedCustomerPhone, N'Password4@Asy')) " +
//                    " FROM CustomerInfo", con);
//                    cmd.ExecuteNonQuery();
//                    con.Close();
//                    displayFirst();
//                }


//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message);
//            }



//        }

//        private void button5_Click(object sender, EventArgs e)
//        {
//            using (SqlConnection sqlCon = new SqlConnection(connectionString))
//            {
//                sqlCon.Open();
//                SqlDataAdapter sda = new SqlDataAdapter("use bookstore " +
//                "truncate table classifierIM", sqlCon);

//                System.Data.DataTable dt = new System.Data.DataTable();
//                sda.Fill(dt);
//                dgvContacts.DataSource = dt;
//                sqlCon.Close();
//            }
//        }
//        DataTableCollection tableCollection;
//        private void button6_Click(object sender, EventArgs e)
//        {

//            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Excel 97-2003 Workbook|*.xls|Excel Workbook|*.xlsx" })
//            {
//                if (ofd.ShowDialog() == DialogResult.OK)
//                {
//                    txtFilename.Text = ofd.FileName;
//                    using (var stream = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read))
//                    {
//                        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
//                        {
//                            DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
//                            {
//                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
//                                {
//                                    UseHeaderRow = true
//                                }
//                            });
//                            tableCollection = result.Tables;
//                            cboSheet.Items.Clear();
//                            foreach (System.Data.DataTable table in tableCollection)
//                                cboSheet.Items.Add(table.TableName);
//                        }
//                    }
//                }
//            }
//        }

//        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
//        {

//            try
//            {
//                using (SqlConnection sqlCon = new SqlConnection(connectionString))
//                {
//                    //string connectionString = @"Data Source=DESKTOP-CK1PKGU;Initial Catalog=bookstore;Integrated Security=True";
//                    DapperPlusManager.Entity<imClassif>().Table("classifierIM");
//                    List<imClassif> book1s = booksBindingSource.DataSource as List<imClassif>;
//                    if (book1s != null)
//                    {
//                        using (IDbConnection db = new SqlConnection(connectionString))
//                        {
//                            db.BulkInsert(book1s);
//                        }
//                        MessageBox.Show("Finished !");
//                    }
//                    else
//                    { MessageBox.Show("Выберите файл !"); }
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//    }
//}
