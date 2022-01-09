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
    public partial class Suppliers : Form
    {
        public Suppliers()
        {
            InitializeComponent();
            ShowSupplier();
        }

        private void ShowSupplier()
        {
            con.Open();
            string query = "Select * from SupplierTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            SuppliersDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dell\OneDrive\Documents\StockTutoDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            if (SupNameTb.Text == "" || SupPhnTb.Text == "" || SupAddTb.Text == "")
            {
                bunifuSnackbar1.Show(this, "Missing Data");
            }
            else
            {

                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into SupplierTbl(SupName,SupPhone,SupAdd) values(@SN, @SP, @SA)", con);
                    cmd.Parameters.AddWithValue("@SN", SupNameTb.Text);
                    cmd.Parameters.AddWithValue("@SP", SupPhnTb.Text);
                    cmd.Parameters.AddWithValue("@SA", SupAddTb.Text);



                    cmd.ExecuteNonQuery();
                    bunifuSnackbar1.Show(this, "Supplier Saved!!!!");
                    con.Close();
                    ShowSupplier();
                     Clear();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        int key = 0;
        private void SuppliersDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SupNameTb.Text = SuppliersDGV.SelectedRows[0].Cells[1].Value.ToString();
            SupPhnTb.Text = SuppliersDGV.SelectedRows[0].Cells[2].Value.ToString();
            SupAddTb.Text = SuppliersDGV.SelectedRows[0].Cells[3].Value.ToString();

            if (SupNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(SuppliersDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (SupNameTb.Text == "" || SupPhnTb.Text == "" || SupAddTb.Text == "")
            {
                bunifuSnackbar1.Show(this, "Missing Data");
            }
            else
            {

                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update  SupplierTbl set SupName=@SN,SupPhone=@SP,SupAdd=@SA where SupCode=@Skey", con);
                    cmd.Parameters.AddWithValue("@SN", SupNameTb.Text);
                    cmd.Parameters.AddWithValue("@SP", SupPhnTb.Text);
                    cmd.Parameters.AddWithValue("@SA", SupAddTb.Text);
                    cmd.Parameters.AddWithValue("@Skey", key);


                    cmd.ExecuteNonQuery();
                    bunifuSnackbar1.Show(this, "Supplier Updated!!!!");
                    con.Close();
                    ShowSupplier();
                     Clear();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                bunifuSnackbar1.Show(this, "Select the Supplier!!!");
            }
            else
            {

                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from SupplierTbl  where SupCode=@Skey", con);

                    cmd.Parameters.AddWithValue("@Skey", key);
                    cmd.ExecuteNonQuery();
                    bunifuSnackbar1.Show(this, "Supplier Deleted!!!!");
                    con.Close();
                    ShowSupplier();
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

        private void Suppliers_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
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

        private void label10_Click(object sender, EventArgs e)
        {
            Users Obj = new Users();
            Obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Category Obj = new Category();
            Obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Customer Obj = new Customer();
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
            SupAddTb.Text = "";
             SupNameTb.Text = "";
            SupPhnTb.Text = "";
            //SPriceTb.Text = "";
        }
        private void Clear()
        {
           SupAddTb.Text = "";
          SupNameTb.Text = "";
        
        SupPhnTb.Text = "";
        //SPriceTb.Text = "";
        //SupCb.SelectedIndex = -1;

         }
    }
}
