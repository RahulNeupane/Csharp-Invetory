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
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
            ShowUser();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dell\OneDrive\Documents\StockTutoDb.mdf;Integrated Security=True;Connect Timeout=30");
       

        private void ShowUser()
        {
            con.Open();
            string query = "Select * from UserTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            UserDGV.DataSource = ds.Tables[0];
            con.Close();
        }
       

        private void savebtn_Click(object sender, EventArgs e)
        {
            if (UnameTb.Text == "" || UGenCb.SelectedIndex == -1 || UPhoneTb.Text == "" || UAddTb.Text == "" || UPassTb.Text == "")
            {
                bunifuSnackbar1.Show(this, "Missing Data");
            }
            else
            {

                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into UserTbl(UName,UPhone,UGender,UAddress,UPassword) values(@UN, @UP, @UG,@UA,@UPA)", con);
                    cmd.Parameters.AddWithValue("@UN", UnameTb.Text);
                    cmd.Parameters.AddWithValue("@UP", UPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@UG", UGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@UA", UAddTb.Text);
                    cmd.Parameters.AddWithValue("@UPA", UPassTb.Text);



                    cmd.ExecuteNonQuery();
                    bunifuSnackbar1.Show(this, "User Saved!!!!");
                    con.Close();
                    ShowUser();
                    Clear();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }


        int Key = 0;
        private void UserDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            UnameTb.Text = UserDGV.SelectedRows[0].Cells[1].Value.ToString();
            UPhoneTb.Text = UserDGV.SelectedRows[0].Cells[2].Value.ToString();
            UGenCb.Text = UserDGV.SelectedRows[0].Cells[3].Value.ToString();
            UAddTb.Text = UserDGV.SelectedRows[0].Cells[4].Value.ToString();
            UPassTb.Text = UserDGV.SelectedRows[0].Cells[5].Value.ToString();

            if (UnameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(UserDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (UnameTb.Text == "" || UPhoneTb.Text == "" || UGenCb.SelectedIndex == -1 ||UAddTb.Text==""|| UPassTb.Text=="")
            {
                bunifuSnackbar1.Show(this, "Missing Data");
            }
            else
            {

                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update  UserTbl set UName=@UN,UPhone=@UP,UGender=@UG, UAddress=@UA, UPassword=@UPA where UNum=@Ukey", con);
                    cmd.Parameters.AddWithValue("@UN", UnameTb.Text);
                    cmd.Parameters.AddWithValue("@UP", UPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@UG", UGenCb.SelectedItem.ToString());

                    cmd.Parameters.AddWithValue("@UA", UAddTb.Text);
                    cmd.Parameters.AddWithValue("@UPA", UPassTb.Text);
                    cmd.Parameters.AddWithValue("@Ukey", Key);


                    cmd.ExecuteNonQuery();
                    bunifuSnackbar1.Show(this, "User Updated!!!!");
                    con.Close();
                    ShowUser();
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
            if(Key == 0)
            {
                bunifuSnackbar1.Show(this, "Select the User!!!");
            }
            else
            {

                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from UserTbl  where UNum=@Ukey", con);

                    cmd.Parameters.AddWithValue("@Ukey", Key);
                    cmd.ExecuteNonQuery();
                    bunifuSnackbar1.Show(this, "User Deleted!!!!");
                    con.Close();
                    ShowUser();
                    Clear();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
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

        private void label9_Click(object sender, EventArgs e)
        {
            Dashbd Obj = new Dashbd();
            Obj.Show();
            this.Hide();
        }

        private void ClrBtn_Click(object sender, EventArgs e)
        {
            UnameTb.Text = "";
            UPhoneTb.Text = "";
            UAddTb.Text = "";
            UGenCb.SelectedIndex = -1;
            UPassTb.Text = "";

        }
        private void Clear()
        {
            UnameTb.Text = "";
            UPhoneTb.Text = "";
            UAddTb.Text = "";
            UPassTb.Text = "";
            UGenCb.SelectedIndex = -1;
        }

       
        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }
    }
}



