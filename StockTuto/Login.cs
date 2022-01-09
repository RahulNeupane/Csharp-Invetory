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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
         //this.ActiveControl = UnameTb;
           // UnameTb.Focus();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dell\OneDrive\Documents\StockTutoDb.mdf;Integrated Security=True;Connect Timeout=30");

        [Obsolete]
        private void Loginbtn_Click(object sender, EventArgs e)
        {
           if(UnameTb.Text=="" || PasswordTb.Text=="")
            {
                bunifuSnackbar1.Show(this, "Missing Data");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("Select count(*) from UserTbl where UName ='" + UnameTb.Text + "' and UPassword='"+PasswordTb.Text+"'", con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if(dt.Rows[0][0].ToString() == "1")
                    {
                        Stocks Obj = new Stocks();
                        Obj.Show();
                        this.Hide();
                        con.Close();
                    }
                    else
                    {
                        bunifuSnackbar1.Show(this, "Wrong UserName or Password");
                    }
                    con.Close();
                }
                catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

       

        private void button4_Click(object sender, EventArgs e)
        {
            if (PasswordTb.PasswordChar == '*')
            {
                button3.BringToFront();
                PasswordTb.PasswordChar = '\0';
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (PasswordTb.PasswordChar == '\0')
            {
                button4.BringToFront();
                PasswordTb.PasswordChar = '*';
            }
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            UnameTb.Text = "";
            PasswordTb.Text = "";

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
