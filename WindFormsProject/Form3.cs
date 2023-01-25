using System;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Windows.Forms;


namespace WindFormsProject
{
    public partial class Form3 : Form
    {
        SqlConnection con;
        SqlCommand com;
        SqlDataReader dr;
        public Form3()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString);
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
                string qry = "insert into Student values(@StuName,@Streams,@Per,@StudAge,@StudGender)";
                com = new SqlCommand(qry, con);
                com.Parameters.AddWithValue("@StuName", txtStuName.Text);
                com.Parameters.AddWithValue("@Streams", txtStreams.Text);
                com.Parameters.AddWithValue("@Per", Convert.ToDecimal(txtPer.Text));
                com.Parameters.AddWithValue("@StudAge", Convert.ToInt32(txtStudAge.Text));
                com.Parameters.AddWithValue("@StudGender", txtstudGender.Text);
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
                string qry = "delete from Student where RollNo=@RollNo";
                com = new SqlCommand(qry, con);
                com.Parameters.AddWithValue("@RollNo", Convert.ToInt32(txtRollNo.Text));
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
                string qry = "update  Student set StudName=@sname,Streams=@stream,Per=@Per ,StudAge=@StudAge,SGender=@StudGender where RollNo=@RollNo";

                com = new SqlCommand(qry, con);

                com.Parameters.AddWithValue("@sname",txtStuName.Text);
                com.Parameters.AddWithValue("@stream", txtStreams.Text);
                com.Parameters.AddWithValue("@Per", Convert.ToDecimal(txtPer.Text));
                com.Parameters.AddWithValue("@StudAge", Convert.ToInt32(txtStudAge.Text));
                com.Parameters.AddWithValue("@stName", txtStreams.Text);
                com.Parameters.AddWithValue("@StudGender", txtstudGender.Text);
                com.Parameters.AddWithValue("@RollNo", Convert.ToInt32(txtRollNo.Text));



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
                string qry = "select * from Student where RollNo=@RollNo";
                com = new SqlCommand(qry, con);
                com.Parameters.AddWithValue("@RollNo", Convert.ToInt32(txtRollNo.Text));
                con.Open();
                dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtStuName.Text= dr["StudName"].ToString();
                        txtStreams.Text = dr["Streams"].ToString();
                        txtPer.Text = dr["Per"].ToString();
                        txtStudAge.Text = dr["StudAge"].ToString();
                        txtstudGender.Text = dr["SGender"].ToString();
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
                string qry = "select * from Student";
                com = new SqlCommand(qry, con);
                con.Open();
                dr = com.ExecuteReader(); 
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
