using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace WindFormsProject
{
    public partial class Form8 : Form
    {
        SqlConnection con;
        SqlDataAdapter adapter;
        SqlCommandBuilder sqlCommandBuilder;
        DataSet ds;
        public Form8()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString);

        }
        private DataSet GetAll()
        {
            adapter = new SqlDataAdapter("select * from Student", con);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            sqlCommandBuilder = new SqlCommandBuilder(adapter);
            ds = new DataSet();
            adapter.Fill(ds, "Student");
            return ds;
        }

        private void ClearForm()
        {
            txtRollNo.Clear();
            txtStuName.Clear();
            txtStreams.Clear();
            txtPer.Clear();
            txtStudAge.Clear();
            txtstudGender.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAll();
                DataRow row = ds.Tables["Student"].NewRow();
                row["StudName"] = txtStuName.Text;
                row["Streams"] = txtStreams.Text;
                row["Per"] = txtPer.Text;
                row["StudAge"] = txtStudAge.Text;
                row["SGender"] = txtstudGender.Text;
                ds.Tables["Student"].Rows.Add(row);
                int result = adapter.Update(ds.Tables["Student"]);
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
                DataRow row = ds.Tables["Student"].Rows.Find(txtRollNo.Text);

                if (row != null)
                {
                    row.Delete();
                    int result = adapter.Update(ds.Tables["Student"]);
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
                DataRow row = ds.Tables["Student"].Rows.Find(txtRollNo.Text);

                if (row != null)
                {
                    row["StudName"] = txtStuName.Text;
                    row["Streams"] = txtStreams.Text;
                    row["Per"] = txtPer.Text;
                    row["StudAge"] = txtStudAge.Text;
                    row["SGender"] = txtstudGender.Text;
                    int result = adapter.Update(ds.Tables["Student"]);
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
                DataRow row = ds.Tables["Student"].Rows.Find(txtRollNo.Text);

                if (row != null)
                {
                    txtStuName.Text = row["StudName"].ToString();
                    txtStreams.Text = row["Streams"].ToString();
                    txtPer.Text = row["Per"].ToString();
                    txtStudAge.Text = row["StudAge"].ToString();
                    txtstudGender.Text = row["SGender"].ToString();
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

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAll();
                dataGridView1.DataSource = ds.Tables["Student"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
