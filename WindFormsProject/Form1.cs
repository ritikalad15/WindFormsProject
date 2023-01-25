using System;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace WindFormsProject
{
    public partial class Form1 : Form
    {
        // step 1 --> set the configuraion with DB server
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        public Form1()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString);
        }

        private void ClearForm()
        {
            txtPID.Clear();
            txtPName.Clear();
            txtPprice.Clear();
            txtPCname.Clear();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)

        {
            try
            {
                // step 2 -  > write the sql query
                string qry = "insert into Product values(@name,@price,@comp)";
                //step 3 - assign the query to command class
                cmd = new SqlCommand(qry, con);
                // ste-4  assign values to the parameter
                cmd.Parameters.AddWithValue("@name", txtPName.Text);
                cmd.Parameters.AddWithValue("@price", Convert.ToInt32(txtPprice.Text));
                cmd.Parameters.AddWithValue("@comp", txtPCname.Text);
                // step 5 - open conn
                con.Open();
                // step 6 - fire the query
                int result = cmd.ExecuteNonQuery(); // fire the insert /update / delete
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
                // step 7
                con.Close();
            }


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
              string qry = "update Product set ProductName=@name,ProdPrice=@price, CompanyName=@comp where ProductId=@id";
             
              cmd = new SqlCommand(qry, con);
    
              cmd.Parameters.AddWithValue("@name",txtPName.Text);
              cmd.Parameters.AddWithValue("@price",Convert.ToInt32(txtPprice.Text));
              cmd.Parameters.AddWithValue("@comp",txtPCname.Text);
              cmd.Parameters.AddWithValue("@id",Convert.ToInt32(txtPID.Text));
              
              con.Open();
              int result = cmd.ExecuteNonQuery(); 
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

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "delete from Product where ProductId=@id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtPID.Text));
                con.Open();
                int result = cmd.ExecuteNonQuery();
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from Product where ProductId=@id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtPID.Text));
                con.Open();
                dr = cmd.ExecuteReader(); 
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtPName.Text = dr["ProductName"].ToString();
                        txtPprice.Text = dr["ProdPrice"].ToString();
                        txtPCname.Text = dr["CompanyName"].ToString();
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
                string qry = "select * from Product";
                cmd = new SqlCommand(qry, con);
                con.Open();
                dr = cmd.ExecuteReader(); // fire the select query
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
