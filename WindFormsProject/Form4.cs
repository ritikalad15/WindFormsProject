using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindFormsProject
{
    public partial class Form4 : Form
    {
        SqlConnection con;
        SqlDataAdapter adapter;
        SqlCommandBuilder sqlCommandBuilder;
        DataSet ds;
        public Form4()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString);

        }
        private DataSet GetAll()
        {
            adapter = new SqlDataAdapter("select * from Product", con);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            sqlCommandBuilder = new SqlCommandBuilder(adapter);
            ds = new DataSet();
            adapter.Fill(ds, "Product");
            return ds;
        }
        private void ClearForm()
        {
            txtPID.Clear();
            txtPName.Clear();
            txtPprice.Clear();
            txtPCname.Clear();

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAll();
                DataRow row = ds.Tables["Product"].NewRow();
                row["PRoductName"] = txtPCname.Text;
                row["PPrice"] = txtPprice.Text;
                row["Company"] = txtPCname.Text;
                ds.Tables["Product"].Rows.Add(row);
                int result = adapter.Update(ds.Tables["Product"]);
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

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAll();
                DataRow row = ds.Tables["Product"].Rows.Find(txtPID.Text);

                if (row != null)
                {
                    row.Delete();
                    int result = adapter.Update(ds.Tables["Product"]);
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
                DataRow row = ds.Tables["product"].Rows.Find(txtPID.Text);

                if (row != null)
                {
                    row["Productname"] = txtPName.Text;
                    row["ProdPrice"] = txtPprice.Text;
                    row["CompanyName"] = txtPCname.Text;
                    int result = adapter.Update(ds.Tables["product"]);
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
                DataRow row = ds.Tables["Product"].Rows.Find(txtPID.Text);

                if (row != null)
                {
                    txtPName.Text = row["Productname"].ToString();
                    txtPprice.Text = row["ProdPrice"].ToString();
                    txtPCname.Text = row["CompanyName"].ToString();

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
                dataGridView1.DataSource = ds.Tables["Product"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        
    }
}
