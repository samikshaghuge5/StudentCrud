using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentDemoCrud
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form1()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);
        }

        private void Save_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "Insert into Student values (@rollno,@name,@percentage)";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@rollno", txtrollno.Text);
                cmd.Parameters.AddWithValue("@name", txtname.Text);
                cmd.Parameters.AddWithValue("@percentage", Convert.ToInt32(txtpercentage.Text));
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Recorded inserted..");
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

        private void Search_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from Student where rollno = @rollno";
                cmd=new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@rollno" , txtrollno.Text);
                con.Open();
                dr=cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtrollno.Text = dr["rollno"].ToString();
                        txtname.Text = dr["name"].ToString();
                        txtpercentage.Text = dr["percentage"].ToString();

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

        private void Update_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "Update Student set rollno = @rollno, name = @name, percentage= @percentage";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@rollno", txtrollno.Text);
                cmd.Parameters.AddWithValue("@name", txtname.Text);
                cmd.Parameters.AddWithValue("@percentage",txtpercentage.Text);
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("record updated");
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

        private void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "Delete from Student where rollno = @rollno";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("rollno",rollno.Text);
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Record deleted");
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

        private void ShowAllStudents_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from Student";
                cmd=new SqlCommand(qry, con);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(dr);
                    dataGridView1.DataSource = dataTable;
                }
                else
                {
                    MessageBox.Show("Record not found");
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
    }
}
