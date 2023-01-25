using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindFormsProject
{
    public partial class Form5 : Form
    {

        SqlConnection con;
        SqlDataAdapter adapter;
        SqlCommandBuilder sqlCommandBuilder;
        DataSet ds;
        public Form5()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString);

        }

        private DataSet GetAll()
        {
            adapter = new SqlDataAdapter("select * from Employee", con);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            sqlCommandBuilder = new SqlCommandBuilder(adapter);
            ds = new DataSet();
            adapter.Fill(ds, "Employee");
            return ds;
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
                ds = GetAll();
                DataRow row = ds.Tables["Employee"].NewRow();
                row["EmpName"] = txtEmpName.Text;
                row["DeptName"] = txtDeptName.Text;
                row["salary"] = txtSalary.Text;
                row["Age"] = txtEmpAge.Text;
                ds.Tables["Employee"].Rows.Add(row);
                int result = adapter.Update(ds.Tables["Employee"]);
                if (result == 1)
                {
                    MessageBox.Show("Record inserted..");
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAll();
                DataRow row = ds.Tables["Employee"].Rows.Find(txtEmpid.Text);

                if (row != null)
                {
                    row.Delete();
                    int result = adapter.Update(ds.Tables["Employee"]);
                    if (result == 1)
                    {
                        MessageBox.Show("Record deleted..");
                        ClearForm();
                    }  
                }
                else
                {
                    MessageBox.Show("Record not found..");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                ds = GetAll();
                DataRow row = ds.Tables["Employee"].Rows.Find(txtEmpid.Text);

                if (row != null)
                {
                    row["EmpName"] = txtEmpName.Text;
                    row["DeptName"] = txtDeptName.Text;
                    row["salary"] = txtSalary.Text;
                    row["Age"] = txtEmpAge.Text;
                    int result = adapter.Update(ds.Tables["Employee"]);
                    if (result == 1)
                    {
                        MessageBox.Show("Record updated..");
                        ClearForm();
                    }
                }
                else
                {
                    MessageBox.Show("Record not found..");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAll();
                DataRow row = ds.Tables["Employee"].Rows.Find(txtEmpid.Text);

                if (row != null)
                {
                    txtEmpName.Text = row["EmpName"].ToString();
                    txtDeptName.Text = row["DeptName"].ToString();
                    txtSalary.Text = row["Salary"].ToString();
                    txtEmpAge.Text = row["Age"].ToString();

                }
                else
                {
                    MessageBox.Show("Record not found..");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnShowall_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAll();
                dataGridView1.DataSource = ds.Tables["Employee"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
