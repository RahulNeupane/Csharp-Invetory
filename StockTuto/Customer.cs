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
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
            ShowCustomer();
        }
        private void ShowCustomer()
        {
            con.Open();
            string query = "Select * from CustomerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CustomerDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dell\OneDrive\Documents\StockTutoDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void savebtn_Click(object sender, EventArgs e)
        {
            if (CustNameTb.Text == "" || CustPhnTb.Text == "" || CustAddTb.Text == ""  || CustGenCb.SelectedIndex == -1)
            {
                bunifuSnackbar1.Show(this, "Missing Data");
            }
            else
            {
            
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into CustomerTbl values(@CN, @CP, @CA, @CG)", con);
                    cmd.Parameters.AddWithValue("@CN", CustNameTb.Text);
                    cmd.Parameters.AddWithValue("@CP", CustPhnTb.Text);
                    cmd.Parameters.AddWithValue("@CA", CustAddTb.Text);
                    cmd.Parameters.AddWithValue("@CG", CustGenCb.SelectedItem.ToString());

                  
                    cmd.ExecuteNonQuery();
                    bunifuSnackbar1.Show(this, "Customer Saved!!!!");
                    con.Close();
                    ShowCustomer();
                    Clear();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        int key = 0;
        private void CustomerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CustNameTb.Text = CustomerDGV.SelectedRows[0].Cells[1].Value.ToString();
            CustPhnTb.Text = CustomerDGV.SelectedRows[0].Cells[2].Value.ToString();
            CustAddTb.Text = CustomerDGV.SelectedRows[0].Cells[3].Value.ToString();
            CustGenCb.Text = CustomerDGV.SelectedRows[0].Cells[4].Value.ToString();
            
            if (CustNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(CustomerDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (CustNameTb.Text == "" || CustPhnTb.Text == "" || CustAddTb.Text == "" || CustGenCb.SelectedIndex == -1)
            {
                bunifuSnackbar1.Show(this, "Missing Data");
            }
            else
            {

                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update  CustomerTbl set CustName=@CN, CustPhone=@CP, CustAdd=@CA, CustGen=@CG where CustId=@CKey", con);
                    cmd.Parameters.AddWithValue("@CN", CustNameTb.Text);
                    cmd.Parameters.AddWithValue("@CP", CustPhnTb.Text);
                    cmd.Parameters.AddWithValue("@CA", CustAddTb.Text);
                    cmd.Parameters.AddWithValue("@CG", CustGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@CKey", key);

                    cmd.ExecuteNonQuery();
                    bunifuSnackbar1.Show(this, "Customer Updated!!!!");
                    con.Close();
                    ShowCustomer();
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
                bunifuSnackbar1.Show(this, "Select the Customer!!!");
            }
            else
            {
               
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from CustomerTbl  where CustId=@CKey", con);

                    cmd.Parameters.AddWithValue("@CKey", key);
                    cmd.ExecuteNonQuery();
                    bunifuSnackbar1.Show(this, "Customer Deleted!!!!");
                    con.Close();
                    ShowCustomer();
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
            Stocks obj = new Stocks();
            obj.Show();
            this.Hide();
        }

        private void CustGenCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CustAddTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void CustPhnTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label9_Click(object sender, EventArgs e)
        {
            Dashbd Obj = new Dashbd();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {
            Orders Obj = new Orders();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
            Suppliers Obj = new Suppliers();
            Obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
            Category Obj = new Category();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void ClrBtn_Click(object sender, EventArgs e)
        {
            CustAddTb.Text = "";
            CustNameTb.Text = "";
            CustGenCb.SelectedIndex = -1;
            CustPhnTb.Text = "";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

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

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void CustNameTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Customer_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Users Obj = new Users();
            Obj.Show();
            this.Hide();
        }
         private void Clear()
         {
            CustAddTb.Text = "";
           CustNameTb.Text = "";
        CustGenCb.SelectedIndex = -1;
         CustPhnTb.Text = "";
        //SPriceTb.Text = "";
        //SupCb.SelectedIndex = -1;

         }
    }
}
