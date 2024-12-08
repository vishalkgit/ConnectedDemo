using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace ConnectedDemo
{
    public partial class DiscStudent : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder scb;

        public DiscStudent()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["studentConn"].ConnectionString);
        }

        private DataSet GetStudent()
        {
            da = new SqlDataAdapter("select * from student", con);
            da.MissingSchemaAction=MissingSchemaAction.Add; 
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "std");
            return ds;
        }

        private void ClearFormFields()
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
                ds=GetStudent();
                DataRow row=ds.Tables["std"].NewRow();
                row["name"]=txtName.Text;
                row["branch"]=txtBranch.Text;
                row["percentage"]=txtPercentage.Text;
                ds.Tables["std"].Rows.Add(row);
                int result = da.Update(ds.Tables["std"]);
                if (result>=1)
                {
                    MessageBox.Show("Record inserted");
                    ClearFormFields();

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
                ds = GetStudent();
                DataRow row = ds.Tables["std"].Rows.Find(txtRollNo.Text);
                if (row!=null)
                {
                    txtName.Text = row["name"].ToString();
                    txtBranch.Text = row["branch"].ToString();
                    txtPercentage.Text = row["percentage"].ToString();

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

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetStudent();
                // find the object in the DataSet
                DataRow row = ds.Tables["std"].Rows.Find(txtRollNo.Text);
                if (row != null)
                {
                    row["name"] = txtName.Text;
                    row["branch"] = txtBranch.Text;
                    row["percentage"] = txtPercentage.Text;
                    // reflect the changes to the DB
                    int result = da.Update(ds.Tables["std"]);
                    if (result >= 1)
                    {
                        MessageBox.Show("Record updated");
                        ClearFormFields();
                    }
                }
                else
                {
                    MessageBox.Show("Record not found for Id");
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
                ds = GetStudent();
                // find the object in the DataSet
                DataRow row = ds.Tables["std"].Rows.Find(txtRollNo.Text);
                if (row != null)
                {
                    row.Delete();
                    int result = da.Update(ds.Tables["std"]);
                    if (result >= 1)
                    {
                        MessageBox.Show("Record deleted");
                        ClearFormFields();
                    }
                }
                else
                {
                    MessageBox.Show("Record not found for Id");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            ds = GetStudent();
            dataGridView1.DataSource = ds.Tables["std"];

        }
    }
}
