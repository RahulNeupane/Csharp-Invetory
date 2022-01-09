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
    public partial class Orders : Form
    {
        public Orders()
        {
            InitializeComponent();
            GetCustomer();
            GetProduct();
            ShowOrders();
           // GetProdName();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dell\OneDrive\Documents\StockTutoDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void ShowOrders()
        {
            con.Open();
            string query = "Select * from OrderTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ORDERSDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void GetCustomer()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from CustomerTbl", con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CustId", typeof(int));
            dt.Load(Rdr);
            CustCb.ValueMember = "CustId";
            CustCb.DataSource = dt;
            con.Close();
        }

       
        private void GetProduct()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from ProductTbl", con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("PrCOde", typeof(int));
            dt.Load(Rdr);
            ProductCb.ValueMember = "PrCOde";
            ProductCb.DataSource = dt;
            con.Close();
        }

        private void GetProdName()
        {
            con.Open();
            string mysql ="Select * from ProductTbl where PrCOde ='" + ProductCb.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(mysql, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                ProdNameTb.Text = dr["PrName"].ToString();
                PriceTb.Text = dr["SPrice"].ToString();
            }
            con.Close();
        }

        //int key = 0;
        private void UpdateStock()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Update ProductTbl set  PrQty=@Pqty where PrCOde=@PrKey", con);
               
                cmd.Parameters.AddWithValue("@Pqty", QtyTb.Text);
                
                cmd.Parameters.AddWithValue("@PrKey", ProductCb.SelectedValue.ToString());
                cmd.ExecuteNonQuery();
               
                con.Close();
                
                //Clear();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        private void savebtn_Click(object sender, EventArgs e)
        {
           
        }

        private void Orders_Load(object sender, EventArgs e)
        {

        }

        int n = 0;
        int GTotal = 0;
        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if(PriceTb.Text ==""|| QtyTb.Text =="")
            {
                bunifuSnackbar1.Show(this, "Missing Information!!!!");
            }
            else
            {
                int total = Convert.ToInt32(QtyTb.Text) * Convert.ToInt32(PriceTb.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(BILLDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = ProdNameTb.Text;
                newRow.Cells[2].Value = PriceTb.Text;
                newRow.Cells[3].Value = QtyTb.Text;
                newRow.Cells[4].Value = total;
                BILLDGV.Rows.Add(newRow);
                GTotal = GTotal + total;
                GrdTotalLbl.Text = "Rs." + GTotal;
                AmountTb.Text = "" + GTotal;
                n++;
                UpdateStock();
                bunifuSnackbar1.Show(this, "Product Added!!!!");

            }
        }

        private void ProductCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetProdName();
        }

        [Obsolete]
        private void label4_Click(object sender, EventArgs e)
        {
            Stocks Obj = new Stocks();
            Obj.Show();
            this.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
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

        private void label9_Click(object sender, EventArgs e)
        {
            Dashbd Obj = new Dashbd();
            Obj.Show();
            this.Hide();
        }

        int key = 0;
        private void ORDERSDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CustCb.Text = ORDERSDGV.SelectedRows[0].Cells[1].Value.ToString();
            UserCb.Text = ORDERSDGV.SelectedRows[0].Cells[2].Value.ToString();
            OrderDate.Text = ORDERSDGV.SelectedRows[0].Cells[3].Value.ToString();
            AmountTb.Text = ORDERSDGV.SelectedRows[0].Cells[4].Value.ToString();
            //SPriceTb.Text = ProductDgv.SelectedRows[0].Cells[5].Value.ToString();
            //PrDate.Text = ProductDgv.SelectedRows[0].Cells[6].Value.ToString();
            //SupCb.Text = ProductDgv.SelectedRows[0].Cells[7].Value.ToString();
            if (CustCb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(ORDERSDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
           
        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            if(printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();

            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("INVENTORY MANAGEMENT SYSTEM", new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Red, new Point(200));

            e.Graphics.DrawString("ORDER DETAILS", new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Red, new Point(250, 85));
            e.Graphics.DrawString("CustId: " + CustCb.Text+"\t\t\tUserId: "+UserCb.Text, new Font("Century Gothic", 18, FontStyle.Regular), Brushes.DarkSlateBlue, new Point(50,120));
            e.Graphics.DrawString("BDate: " + OrderDate.Text + "\t\tBAmount: " + AmountTb.Text, new Font("Century Gothic", 18, FontStyle.Regular), Brushes.DarkSlateBlue, new Point(50, 160));
            // e.Graphics.DrawString("BDate: " + OrderDate.Text + "\tBAmount: " + AmountTb.Text, new Font("Century Gothic", 18, FontStyle.Regular), Brushes.DarkSlateBlue, new Point(10, 120));
           
            
            e.Graphics.DrawString("YOUR ORDERS", new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Red, new Point(250,230));
            e.Graphics.DrawString("PRODUCT: " + ProductCb.Text + "\t\t\tPRICE: " + PriceTb.Text, new Font("Century Gothic", 18, FontStyle.Regular), Brushes.DarkSlateBlue, new Point(50, 265));
            e.Graphics.DrawString("QUANTITY: " + QtyTb.Text + "\t\t\tTOTAL: " + GrdTotalLbl.Text, new Font("Century Gothic", 18, FontStyle.Regular), Brushes.DarkSlateBlue, new Point(50, 305));

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

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void DeleteBtn_Click_1(object sender, EventArgs e)
        {
            if (key == 0)
            {
                bunifuSnackbar1.Show(this, "Select the Order");
            }
            else
            {
                // int Gain = Convert.ToInt32(SPriceTb.Text) - Convert.ToInt32(BPriceTb.Text);
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from OrderTbl  where ONum=@Okey", con);

                    cmd.Parameters.AddWithValue("@Okey", key);
                    cmd.ExecuteNonQuery();
                    bunifuSnackbar1.Show(this, "Order Deleted!!!!");
                    con.Close();
                    ShowOrders();


                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void savebtn_Click_1(object sender, EventArgs e)
        {
            if (CustCb.SelectedIndex == -1 || UserCb.SelectedIndex == -1 || AmountTb.Text == "")
            {
                bunifuSnackbar1.Show(this, "Missing Data");
            }
            else
            {

                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into OrderTbl  (CustId, UserId, BDate, BAmount) values (@CI, @UI, @BD, @BA)", con);
                    cmd.Parameters.AddWithValue("@CI", ProductCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@UI", UserCb.SelectedIndex.ToString());
                    cmd.Parameters.AddWithValue("@BD", OrderDate.Value.Date);
                    cmd.Parameters.AddWithValue("@BA", AmountTb.Text);

                    cmd.ExecuteNonQuery();
                    bunifuSnackbar1.Show(this, "Order Added!!!!");
                    con.Close();
                    ShowOrders();
                    // Clear();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
    }
}
