using System;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Windows.Forms;

namespace WindFormsProject
{
    public partial class Form2 : Form
    {
        SqlConnection con;
        SqlCommand com;
        SqlDataReader dr;
        public Form2()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString);

        }

        private void ClearForm()
        {
            txtEmpid.Clear();
            txtEmpName.Clear();
            txtDeptName.Clear();
            txtSalary.Clear();
            txtEmpAge.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "insert into Employee values(@Empname,@DeptName,@Salary,@Age)";
                com = new SqlCommand(qry, con);
                com.Parameters.AddWithValue("@Empname", txtEmpName.Text);
                com.Parameters.AddWithValue("@DeptName",txtDeptName.Text);
                com.Parameters.AddWithValue("@Salary", Convert.ToInt32(txtSalary.Text));
                com.Parameters.AddWithValue("@Age", Convert.ToInt32(txtEmpAge.Text));
                con.Open();
                
                int result = com.ExecuteNonQuery(); 
                if (result == 1)
                {
                    MessageBox.Show("Record inserted");
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
               
                con.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "delete from Empolyee where EmpId=@id";
                com = new SqlCommand(qry, con);
                com.Parameters.AddWithValue("@id", Convert.ToInt32(txtEmpid.Text));
                con.Open();
                int result = com.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Record Deleted");
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                string qry = "update Employee set EmpName=@Ename,DeptName=@DName, Salary=@Esalary,Age=@Age where EmpId=@id";

                com = new SqlCommand(qry, con);
                 
                com.Parameters.AddWithValue("@EName", txtEmpName.Text);
                com.Parameters.AddWithValue("@DName",txtDeptName.Text);
                com.Parameters.AddWithValue("@Esalary", Convert.ToInt32(txtSalary.Text));
                com.Parameters.AddWithValue("@Age", Convert.ToInt32(txtEmpAge.Text));
                com.Parameters.AddWithValue("@id", Convert.ToInt32(txtEmpid.Text));
                


                con.Open();
                int result = com.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Record updated");
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            try
            {
                string qry = "select * from Employee where EmpId=@id";
                com = new SqlCommand(qry, con);
                com.Parameters.AddWithValue("@id", Convert.ToInt32(txtEmpid.Text));
                con.Open();
                dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtEmpName.Text = dr["EmpName"].ToString();
                        txtDeptName.Text = dr["DeptName"].ToString();
                        txtSalary.Text = dr["Salary"].ToString();
                        txtEmpAge.Text = dr["Age"].ToString();
                    }
                }
                else
                {
                    
                    MessageBox.Show("Record not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                con.Close();
            }
        }

        private void btnShowall_Click(object sender, EventArgs e)
        {

            try
            {
                string qry = "select * from Employee";
                com = new SqlCommand(qry, con);
                con.Open();
                dr = com.ExecuteReader(); // fire the select query
                if (dr.HasRows)
                {
                    DataTable table = new DataTable();
                    table.Load(dr);
                    dataGridView1.DataSource = table;
                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                con.Close();
            }
        }
    }
}
