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
using System.Xml.Linq;

namespace StudentDemoCrud
{
    public partial class Form2 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommandBuilder scb;
        DataSet ds;
        public Form2()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);
        }
        private DataSet GetAllProducts()
        {
            da = new SqlDataAdapter("select * from Student",con);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            scb=new SqlCommandBuilder(da);
            ds=new DataSet();
            da.Fill(ds, "student");
            return ds;
        }
        private void Save_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = GetAllProducts();
                DataRow row = ds.Tables["student"].NewRow();
                row["rollno"] = txtrollno.Text;
                row["name"] = txtname.Text;
                row["percentage"] = txtpercentage.Text;
                
                ds.Tables["student"].Rows.Add(row);
                int res = da.Update(ds.Tables["student"]);
                if (res >= 1)
                {
                    MessageBox.Show("Record inserted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = GetAllProducts();
                DataRow row = ds.Tables["student"].Rows.Find(txtrollno.Text);
                if (row != null)
                {
                    row.Delete();
                    int res = da.Update(ds.Tables["student"]);
                    if (res >= 1)
                    {
                        MessageBox.Show("Record deleted");
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

        }

        private void Search_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = GetAllProducts();
                DataRow row = ds.Tables["student"].Rows.Find(txtrollno.Text);
                if (row != null)
                {
                    txtname.Text = row["name"].ToString();
                    txtpercentage.Text = row["percentage"].ToString();
                    //txtPrice.Text = row["price"].ToString();
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Update_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = GetAllProducts();
                DataRow row = ds.Tables["student"].Rows.Find(txtrollno.Text);
                if (row != null)
                {
                    row["name"] = txtname.Text;
                    row["percentage"] = txtpercentage.Text;
                    
                    int res = da.Update(ds.Tables["student"]);
                    if (res >= 1)
                    {
                        MessageBox.Show("Record updated");
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

        }

        private void ShowAllStudents_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = GetAllProducts();
                dataGridView1.DataSource = ds.Tables["student"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void txtpercentage_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtname_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtrollno_TextChanged(object sender, EventArgs e)
        {

        }

        private void percentage_Click(object sender, EventArgs e)
        {

        }

        private void name_Click(object sender, EventArgs e)
        {

        }

        private void rollno_Click(object sender, EventArgs e)
        {

        }
    }
}
