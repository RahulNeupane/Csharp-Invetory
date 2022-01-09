using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace StockTuto
{
    public partial class Stocks : Form
    {
        [Obsolete]
        public Stocks()
        {
            InitializeComponent();
            Showproduct();
            GetCategories();
            GetSuppliers();
       
        }
        private void Showproduct()
        {
            con.Open();
            string query = "Select * from ProductTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProductDgv.DataSource = ds.Tables[0];
            con.Close();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dell\OneDrive\Documents\StockTutoDb.mdf;Integrated Security=True;Connect Timeout=30");
        
        private void GetCategories()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from CategoryTbl", con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CatId", typeof(int));
            dt.Load(Rdr);
            CatCb.ValueMember = "CatId";
            CatCb.DataSource = dt;
            con.Close();
        }

        private void GetSuppliers()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from SupplierTbl", con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("SupCode", typeof(int));
            dt.Load(Rdr);
            SupCb.ValueMember = "SupCode";
            SupCb.DataSource = dt;
            con.Close();
        }
        
        private void BunifuTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Savebtn_Click(object sender, EventArgs e)
        {
            if(PrNameTb.Text == "" || QtyTb.Text ==""|| SPriceTb.Text ==""|| BPriceTb.Text == ""|| SupCb.SelectedIndex ==-1 || CatCb.SelectedIndex ==-1)
            {
                bunifuSnackbar1.Show(this, "Missing Data");
            }
            else
            {
                int Gain = Convert.ToInt32(SPriceTb.Text) - Convert.ToInt32(BPriceTb.Text);
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into ProductTbl values(@PN, @Pcat, @Pqty, @BPr, @SPr,@PDate, @Sup, @G)", con);
                    cmd.Parameters.AddWithValue("@PN", PrNameTb.Text);
                    cmd.Parameters.AddWithValue("@Pcat", CatCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Pqty", QtyTb.Text);
                    cmd.Parameters.AddWithValue("@BPr", BPriceTb.Text);
                    cmd.Parameters.AddWithValue("@SPr", SPriceTb.Text);
                    cmd.Parameters.AddWithValue("@PDate", PrDate.Value.Date);
                    cmd.Parameters.AddWithValue("@Sup", SupCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@G", Gain);
                    cmd.ExecuteNonQuery();
                    bunifuSnackbar1.Show(this, "Product Saved!!!!");
                    con.Close();
                    Showproduct();
                  

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        int key = 0;
        private void ProductDgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PrNameTb.Text = ProductDgv.SelectedRows[0].Cells[1].Value.ToString();
            CatCb.Text = ProductDgv.SelectedRows[0].Cells[2].Value.ToString();
            QtyTb.Text = ProductDgv.SelectedRows[0].Cells[3].Value.ToString();
            BPriceTb.Text = ProductDgv.SelectedRows[0].Cells[4].Value.ToString();
            SPriceTb.Text = ProductDgv.SelectedRows[0].Cells[5].Value.ToString();
            PrDate.Text = ProductDgv.SelectedRows[0].Cells[6].Value.ToString();
            SupCb.Text = ProductDgv.SelectedRows[0].Cells[7].Value.ToString();
            if (PrNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(ProductDgv.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (PrNameTb.Text == "" || QtyTb.Text == "" || SPriceTb.Text == "" || BPriceTb.Text == "" || SupCb.SelectedIndex == -1 || CatCb.SelectedIndex == -1)
            {
                bunifuSnackbar1.Show(this, "Missing Data");
            }
            else
            {
                int Gain = Convert.ToInt32(SPriceTb.Text) - Convert.ToInt32(BPriceTb.Text);
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update ProductTbl set PrName=@PN, PrCategory=@Pcat, PrQty=@Pqty,BPrice= @BPr, Sprice=@SPr, PrDate=@PDate, Ssup=@Sup, Gain=@G where PrCode=@PrKey", con);
                    cmd.Parameters.AddWithValue("@PN", PrNameTb.Text);
                    cmd.Parameters.AddWithValue("@Pcat", CatCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Pqty", QtyTb.Text);
                    cmd.Parameters.AddWithValue("@BPr", BPriceTb.Text);
                    cmd.Parameters.AddWithValue("@SPr", SPriceTb.Text);
                    cmd.Parameters.AddWithValue("@PDate", PrDate.Value.Date);
                    cmd.Parameters.AddWithValue("@Sup", SupCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@G", Gain);
                    cmd.Parameters.AddWithValue("@PrKey", key);
                    cmd.ExecuteNonQuery();
                    bunifuSnackbar1.Show(this, "Product Edited!!!!");
                    con.Close();
                    Showproduct();
                   

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
                bunifuSnackbar1.Show(this, "Select the Product");
            }
            else
            {
              int Gain = Convert.ToInt32(SPriceTb.Text) - Convert.ToInt32(BPriceTb.Text);
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from ProductTbl  where PrCode=@PrKey", con);
                    
                    cmd.Parameters.AddWithValue("@PrKey", key);
                    cmd.ExecuteNonQuery();
                    bunifuSnackbar1.Show(this, "Product Deleted!!!!");
                    con.Close();
                    Showproduct();
                  

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

       

        [Obsolete]
        private void label5_Click(object sender, EventArgs e)
        {
            Category Obj = new Category();
            Obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Customer obj = new Customer();
            obj.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Suppliers obj = new Suppliers();
            obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
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

        // private void ClrBtn_Click(object sender, EventArgs e)
        // {
        //  PrNameTb.Text = "";
        // QtyTb.Text = "";
        // BPriceTb.Text = "";
        //SPriceTb.Text = "";
        // }
        private void Clear()
        {
            PrNameTb.Text = "";

           
        }
    }
}
