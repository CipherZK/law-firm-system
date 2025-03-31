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
using DevExpress.XtraExport.Implementation;
using Excel = Microsoft.Office.Interop.Excel;

namespace Dip.Tables
{
    public partial class clients : Form
    {

        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataTable dt;
        //CustomerInfo
        string connectionString = connection.str_connect;

        public clients()
        {
            InitializeComponent();
            login view = new login();
            display();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (txtiDclient.Text == "" || txtName.Text == "" || txtDateOfBirth.Text == "" || txtnumberPass.Text == "" || txtIIN.Text == "" || txtAddress.Text == "" || txtTel.Text == "")
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

                        cmd = new SqlCommand("insert into клиент ([Код клиента], [ФИО клиента], [Дата рождения], [Номер паспорта], ИИН, Адрес, Телефон) values ('" + txtiDclient.Text + "', '" + txtName.Text + "', '" + txtDateOfBirth.Text + "', '" + txtnumberPass.Text + "', '" + txtIIN.Text + "', '" + txtAddress.Text + "', '" + txtTel.Text + "', '" + txtEmail.Text + "')", con);
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
        public void clear()
        {
            txtiDclient.Text = "";
            txtName.Text = "";
            txtDateOfBirth.Text = "";
            txtnumberPass.Text = "";
            txtIIN.Text = "";
            txtAddress.Text = "";
            txtTel.Text = "";
            txtEmail.Text = "";
        }


        public void display()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    dt = new DataTable();
                    con.Open();
                    adpt = new SqlDataAdapter("select * from клиент", con);
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
                        cmd = new SqlCommand("update клиент set [Код клиента]='" + txtiDclient.Text + "', [ФИО клиента]='" + txtName.Text + "', [Дата рождения]='" + txtDateOfBirth.Text + "', [Номер паспорта]='" + txtnumberPass.Text + "', ИИН='" + txtIIN.Text + "', Адрес='" + txtAddress.Text + "', Телефон='" + txtTel.Text + "', [Е-майл]='" + txtEmail.Text + "' where [Код клиента]='" + txtiDclient.Text + "'", con);
                        //cmd = new SqlCommand("update sale set date '" + txtDate.Text + "', book_id '" + txtBookID.Text + "', number_of_instances '" + txtPages + "', payment_amount '" + txtSum + "', employee_id '" + txtEmployeeID.Text + "', orderr='" + priznak + "', orderr_number '" + txtNumOrderr + "' where id=" + id + "'", con);

                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("You Data Has Been Updated ");
                        display();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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
                        cmd = new SqlCommand("delete from клиент where [Код клиента]= '" + txtiDclient + "'", con);
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

        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            txtiDclient.Text = Convert.ToString(dataGridView1[0, Convert.ToInt32(dataGridView1.CurrentRow.Index)].Value);
            txtName.Text = Convert.ToString(dataGridView1[1, Convert.ToInt32(dataGridView1.CurrentRow.Index)].Value);
            //txtBookID.Text = Convert.ToString(dgvContacts[2, Convert.ToInt32(dgvContacts.CurrentRow.Index)].Value);
            txtDateOfBirth.Text = Convert.ToString(dataGridView1[2, Convert.ToInt32(dataGridView1.CurrentRow.Index)].Value);
            txtnumberPass.Text = Convert.ToString(dataGridView1[3, Convert.ToInt32(dataGridView1.CurrentRow.Index)].Value);
            txtIIN.Text = Convert.ToString(dataGridView1[4, Convert.ToInt32(dataGridView1.CurrentRow.Index)].Value);
            txtAddress.Text = Convert.ToString(dataGridView1[5, Convert.ToInt32(dataGridView1.CurrentRow.Index)].Value);
            txtTel.Text = Convert.ToString(dataGridView1[6, Convert.ToInt32(dataGridView1.CurrentRow.Index)].Value);
            txtEmail.Text = Convert.ToString(dataGridView1[7, Convert.ToInt32(dataGridView1.CurrentRow.Index)].Value);

        }


        //Екземпляр приложения Excel
        Excel.Application xlApp;
        //Лист
        Excel.Worksheet xlSheet;
        //Выделеная область
        Excel.Range xlSheetRange;



        private DataTable GetData()
        {
            //строка соединения$"Data Source=DESKTOP-S23NBQ7;Initial Catalog=адвокатская;User ID={textBox1.Text};Password={tbPassword.Text};"
            string connString = "Data Source=DESKTOP-S23NBQ7;Initial Catalog=адвокатская; integrated security = true";

            //соединение
            SqlConnection con = new SqlConnection(connString);

            DataTable dt = new DataTable();
            try
            {
                string query = @"SELECT * FROM клиент";
                SqlCommand comm = new SqlCommand(query, con);

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(comm);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return dt;
        }

        //Освобождаем ресуры (закрываем Excel)
        void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show(ex.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                GC.Collect();
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            xlApp = new Excel.Application();

            try
            {
                //добавляем книгу
                xlApp.Workbooks.Add(Type.Missing);

                //делаем временно неактивным документ
                xlApp.Interactive = false;
                xlApp.EnableEvents = false;

                //выбираем лист на котором будем работать (Лист 1)
                xlSheet = (Excel.Worksheet)xlApp.Sheets[1];
                //Название листа
                xlSheet.Name = "Данные";

                //Выгрузка данных
                DataTable dt = GetData();

                int collInd = 0;
                int rowInd = 0;
                string data = "";

                //называем колонки
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    data = dt.Columns[i].ColumnName.ToString();
                    xlSheet.Cells[1, i + 1] = data;

                    //выделяем первую строку
                    xlSheetRange = xlSheet.get_Range("A1:Z1", Type.Missing);

                    //делаем полужирный текст и перенос слов
                    xlSheetRange.WrapText = true;
                    xlSheetRange.Font.Bold = true;
                }

                //заполняем строки
                for (rowInd = 0; rowInd < dt.Rows.Count; rowInd++)
                {
                    for (collInd = 0; collInd < dt.Columns.Count; collInd++)
                    {
                        data = dt.Rows[rowInd].ItemArray[collInd].ToString();
                        xlSheet.Cells[rowInd + 2, collInd + 1] = data;
                    }
                }

                //выбираем всю область данных
                xlSheetRange = xlSheet.UsedRange;

                //выравниваем строки и колонки по их содержимому
                xlSheetRange.Columns.AutoFit();
                xlSheetRange.Rows.AutoFit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                //Показываем ексель
                xlApp.Visible = true;

                xlApp.Interactive = true;
                xlApp.ScreenUpdating = true;
                xlApp.UserControl = true;

                //Отсоединяемся от Excel
                releaseObject(xlSheetRange);
                releaseObject(xlSheet);
                releaseObject(xlApp);
            }
        }
    }
}
