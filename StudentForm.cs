using System;

using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace ConnectedDemo
{
    public partial class StudentForm : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public StudentForm()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["studentConn"].ConnectionString);
        }

        private void clearFormFields()
        {
            txtRollNo.Clear();
            txtName.Clear();
            txtBranch.Clear();
            txtPercentage.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "insert into student values(@name,@branch,@percentage)";
                cmd= new SqlCommand(qry,con);
                cmd.Parameters.AddWithValue("@name",txtName.Text);  
                cmd.Parameters.AddWithValue("@branch",txtBranch.Text);
                cmd.Parameters.AddWithValue("@percentage",txtPercentage.Text);  
                con.Open();
                int result=cmd.ExecuteNonQuery();
                if(result>=1)
                {
                    MessageBox.Show("Student added successfully");
                    clearFormFields();

                }
            }
            catch(Exception ex)
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
                string qry = "select name,branch,percentage from student where rollno=@rollno";
                cmd= new SqlCommand(qry,con);
                cmd.Parameters.AddWithValue("@rollno",Convert.ToInt32(txtRollNo.Text));
                con.Open();
                dr=cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    if(dr.Read())
                    {
                        txtName.Text = dr["name"].ToString();
                        txtBranch.Text = dr["branch"].ToString();
                        txtPercentage.Text = dr["percentage"].ToString() + "%"; 
                    }
                    else
                    {
                        MessageBox.Show("Record not found");
                    }
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
                string qry = "update student set name=@name,branch=@branch,percentage=@percentage where rollno=@rollno";
                cmd= new SqlCommand(qry,con);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@branch", txtBranch.Text);
                cmd.Parameters.AddWithValue("@percentage", Convert.ToInt32 (txtPercentage.Text));
                cmd.Parameters.AddWithValue("@rollno", Convert.ToInt32(txtRollNo.Text));
                con.Open();
                int result=cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Employee updated successfully");
                    clearFormFields();
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
                string qry = "delete from student where rollno=@rollno";
                cmd= new SqlCommand(qry,con);
                cmd.Parameters.AddWithValue("@rollno", Convert.ToInt32(txtRollNo.Text));
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Student deleted successfully");
                    clearFormFields();
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
                string qry = "select * from student";
                cmd = new SqlCommand(qry, con);// fire , con
                con.Open();
                dr = cmd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(dr); // convert dr object in to row & col format
                dataGridView1.DataSource = table;
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
