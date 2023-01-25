using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Windows.Forms;


namespace WindFormsProject
{
    public partial class Form7 : Form
    {
        SqlConnection con;
        SqlCommand com;
        SqlDataReader dr;
        public Form7()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString);

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
                string qry = "insert into Book values(@BookName,@BookPrice,@Author,@BEdition,@Publication)";
                com = new SqlCommand(qry, con);
                com.Parameters.AddWithValue("@BookName", txtBName.Text);
                com.Parameters.AddWithValue("@BookPrice", Convert.ToInt32(txtBPrice.Text));
                com.Parameters.AddWithValue("@Author", txtAuthor.Text);
                com.Parameters.AddWithValue("@BEdition", txtBEdition.Text);
                com.Parameters.AddWithValue("@Publication", Convert.ToInt32(txtPublication.Text));
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
                string qry = "delete from Book where BookId=@id";
                com = new SqlCommand(qry, con);
                com.Parameters.AddWithValue("@id", Convert.ToInt32(txtBookId.Text));
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
                string qry = "update Book set BookName=@BookName,BookPrice=@BookPrice, Author=@Author,BEdition=@BEdition,BPublication=@BPublication where BookId=@BookId";

               com = new SqlCommand(qry, con);
               
               com.Parameters.AddWithValue("@BookName",txtBName.Text);
               com.Parameters.AddWithValue("@BookPrice", Convert.ToInt32(txtBPrice.Text));
               com.Parameters.AddWithValue("@Author",txtAuthor.Text);
               com.Parameters.AddWithValue("@BEdition", txtBEdition.Text);
               com.Parameters.AddWithValue("@BPublication",Convert.ToInt32(txtPublication.Text));
               com.Parameters.AddWithValue("@BookId", Convert.ToInt32(txtBookId.Text));

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
                string qry = "select * from Book where BookId=@id";
                com= new SqlCommand(qry, con);
                com.Parameters.AddWithValue("@id", Convert.ToInt32(txtBookId.Text));
                con.Open();
                dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtBName.Text = dr["BookName"].ToString();
                        txtBPrice.Text = dr["BookPrice"].ToString();
                        txtAuthor.Text = dr["Author"].ToString();
                        txtBEdition.Text = dr["BEdition"].ToString();
                        txtPublication.Text =dr["BPublication"].ToString();
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

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from Book";
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
