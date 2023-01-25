using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace WindFormsProject
{
    public partial class Form6 : Form
    {

        SqlConnection con;
        SqlDataAdapter adapter;
        SqlCommandBuilder sqlCommandBuilder;
        DataSet ds;
        public Form6()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString);

        }
        private DataSet GetAll()
        {
            adapter = new SqlDataAdapter("select * from Book", con);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            sqlCommandBuilder = new SqlCommandBuilder(adapter);
            ds = new DataSet();
            adapter.Fill(ds, "Book");
            return ds;
        }

        private void ClearForm()
        {
            txtBookId.Clear();
            txtBName.Clear();
            txtBPrice.Clear();
            txtAuthor.Clear();
            txtBEdition.Clear();
            txtPublication.Clear();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAll();
                DataRow row = ds.Tables["Book"].NewRow();
                row["BookName"] = txtBName.Text;
                row["BookPrice"] = txtBPrice.Text;
                row["Author"] = txtAuthor.Text;
                row["BEdition"] = txtBEdition.Text;
                row["BPublication"] = txtPublication.Text;
                ds.Tables["Book"].Rows.Add(row);
                int result = adapter.Update(ds.Tables["Book"]);
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
                DataRow row = ds.Tables["Book"].Rows.Find(txtBookId.Text);

                if (row != null)
                {
                    row.Delete();
                    int result = adapter.Update(ds.Tables["Book"]);
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
                DataRow row = ds.Tables["Book"].Rows.Find(txtBookId.Text);

                if (row != null)
                {
                    row["BookName"] = txtBName.Text;
                    row["BookPrice"] = txtBPrice.Text;
                    row["Author"] = txtAuthor.Text;
                    row["BEdition"] = txtBEdition.Text;
                    row["BPublication"] = txtPublication.Text;
                    int result = adapter.Update(ds.Tables["Book"]);
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
                DataRow row = ds.Tables["Book"].Rows.Find(txtBookId.Text);

                if (row != null)
                {
                    txtBName.Text = row["BookName"].ToString();
                    txtBPrice.Text = row["BookPrice"].ToString();
                    txtAuthor.Text = row["Author"].ToString();
                    txtBEdition.Text = row["BEdition"].ToString();
                    txtPublication.Text = row["BPublication"].ToString();
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
                dataGridView1.DataSource = ds.Tables["Book"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
