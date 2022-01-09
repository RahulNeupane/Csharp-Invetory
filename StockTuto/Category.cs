using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockTuto
{
    public partial class Category : Form
    {
        public Category()
        {
            InitializeComponent();
            ShowCategories();
            CountCat();
        }
        private void ShowCategories()
        {
            con.Open();
            string query = "Select * from CategoryTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CategoriesDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dell\OneDrive\Documents\StockTutoDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void savebtn_Click(object sender, EventArgs e)
        {
            if (CategoriesTb.Text == "")
            {
                bunifuSnackbar1.Show(this, "Missing Data");
            }
            else
            {

                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into CategoryTbl values (@CN)", con);
                    cmd.Parameters.AddWithValue("@CN", CategoriesTb.Text);
  


                    cmd.ExecuteNonQuery();
                    bunifuSnackbar1.Show(this, "Category Saved!!!!");
                    con.Close();
                    ShowCategories();
                    CountCat();
                     Clear();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        int key = 0;
        private void CategoriesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CategoriesTb.Text = CategoriesDGV.SelectedRows[0].Cells[1].Value.ToString();
           

            if (CategoriesTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(CategoriesDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (CategoriesTb.Text == "")
            {
                bunifuSnackbar1.Show(this, "Missing Data");
            }
            else
            {

                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update CategoryTbl set CatName=@CN where CatId=@CKey", con);
                    cmd.Parameters.AddWithValue("@CN", CategoriesTb.Text);
                    cmd.Parameters.AddWithValue("@CKey", key);



                    cmd.ExecuteNonQuery();
                    bunifuSnackbar1.Show(this, "Category Updated!!!!");
                    con.Close();
                    ShowCategories();
                     Clear();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void CountCat()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select count(*) from CategoryTbl", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CatNameLbl.Text = dt.Rows[0][0].ToString();

            con.Close();
        }
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                bunifuSnackbar1.Show(this, "Select the Category!!!");
            }
            else
            {

                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from CategoryTbl  where CatId=@CKey", con);

                    cmd.Parameters.AddWithValue("@CKey", key);
                    cmd.ExecuteNonQuery();
                    bunifuSnackbar1.Show(this, "Category Deleted!!!!");
                    con.Close();
                    ShowCategories();
                    Clear();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        [Obsolete]
        private void label4_Click(object sender, EventArgs e)
        {
            Stocks Obj = new Stocks();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Users Obj = new Users();
            Obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Customer Obj = new Customer();
            Obj.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Suppliers Obj = new Suppliers();
            Obj.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Orders Obj = new Orders();
            Obj.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Dashbd Obj = new Dashbd();
            Obj.Show();
            this.Hide();
        }

        private void ClrBtn_Click(object sender, EventArgs e)
        {
            CategoriesTb.Text = "";
        }
        private void Clear()
        {
            CategoriesTb.Text = "";
        }
    }
}
