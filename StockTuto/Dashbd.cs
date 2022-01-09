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
    public partial class Dashbd : Form
    {
        public Dashbd()
        {
            InitializeComponent();
            CountCategorie();
            CountCustomer();
            CountSupplier();
            CountProduct();
            CountOrder();
            GetMaxOrder();
            GetLatestDate();
            CountUsers();
          
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dell\OneDrive\Documents\StockTutoDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void CountCategorie()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select count(*) from CategoryTbl", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CatNumLbl.Text = dt.Rows[0][0].ToString() + "Categories";
            con.Close();
        }
        private void CountCustomer()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select count(*) from CustomerTbl", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CustNumLBl.Text = dt.Rows[0][0].ToString() + "Customers";
            con.Close();
        }
        private void CountSupplier()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select count(*) from SupplierTbl", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            SupNumLbl.Text = dt.Rows[0][0].ToString() + "Suppliers";
            con.Close();
        }
        private void CountProduct()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select count(*) from ProductTbl", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            ProdNumLbl.Text = dt.Rows[0][0].ToString() + "Products";
            con.Close();
        }
        private void CountOrder()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select count(*) from OrderTbl", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            OrdNumLBl.Text = dt.Rows[0][0].ToString() + "Orders";
            con.Close();
        }
        private void CountUsers()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select count(*) from UserTbl", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            UserNumLbl.Text = dt.Rows[0][0].ToString() + "Users";
            con.Close();
        }
        private void GetMaxOrder()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Max(BAmount) from OrderTbl", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            MaxOrdLbl.Text = "Rs." + dt.Rows[0][0].ToString();
            con.Close();
        }

        private void GetLatestDate()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Max(BDate) from OrderTbl", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            LatestLbl.Text =  dt.Rows[0][0].ToString();
            con.Close();
        }
       
        private void Dashbd_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        [Obsolete]
        private void label4_Click(object sender, EventArgs e)
        {
            Stocks Obj = new Stocks();
            Obj.Show();
            this.Hide();
        }

        private void label20_Click(object sender, EventArgs e)
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
