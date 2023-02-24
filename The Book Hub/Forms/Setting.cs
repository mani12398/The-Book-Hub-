using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace The_Book_Hub.Forms
{
    public partial class Setting : Form
    {
        

        string connection_string_1 = System.Configuration.ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString;
        static string Username;
        static string Password;
        public Setting()
        {
            InitializeComponent();
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            bunifuTextBox5.Focus();
            bunifuTextBox5.UseSystemPasswordChar = true;
            bunifuTextBox4.UseSystemPasswordChar = true;
            bunifuTextBox1.UseSystemPasswordChar = true;
            panel2.Visible = false;
            label2.Text = "";
            label3.Text = "";
            guna2Button2.Enabled = false;
            guna2Button1.Enabled = false;

            guna2CircleButton1.Visible = false;
            guna2CircleButton4.Visible = false;
            panel2.Location = new Point(21, 78);
        }

        private void bunifuTextBox5_TextChanged(object sender, EventArgs e)
        {
            if (bunifuTextBox5.Text != "")
            {
                guna2Button1.Enabled = true;
            }
            else
            {
                guna2Button1.Enabled = false;
            }
        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            //if (guna2CheckBox1.Checked == true)
            //{
                
            //    bunifuTextBox4.UseSystemPasswordChar = false;
            //    bunifuTextBox1.UseSystemPasswordChar = false;
            //}
            //else if (guna2CheckBox1.Checked == false)
            //{
                
            //    bunifuTextBox4.UseSystemPasswordChar = true;
            //    bunifuTextBox1.UseSystemPasswordChar = true;

            //}
        }

        private void bunifuTextBox4_TextChanged(object sender, EventArgs e)
        {
            
            if (bunifuTextBox4.Text != "" && bunifuTextBox1.Text != "")
            {
                guna2Button2.Enabled = true;
            }
            else
            {
                guna2Button2.Enabled = false;

            }
            PasswordValidation();
            PasswordValidation2();
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (bunifuTextBox4.Text != "" && bunifuTextBox1.Text != "")
            {
                guna2Button2.Enabled = true;
            }
            else
            {
                guna2Button2.Enabled = false;

            }
            PasswordValidation1();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
           
            if (bunifuTextBox5.Text != "")
            {
                Username = Login.UserName;
                SqlConnection con = new SqlConnection(connection_string_1);
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from LoginTable where Username='" + Username + "' and Password ='" + bunifuTextBox5.Text + "'", con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    panel1.Visible = false;
                    panel2.Visible = true;
                    bunifuTextBox4.Focus();

                }
                else
                {
                    MessageBox.Show("Wrong Old Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    bunifuTextBox5.Focus();
                }
                sdr.Close();
                con.Close();
            }
            else
            {
               
                MessageBox.Show("Please Enter Details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bunifuTextBox5.Focus();
            }
            

        }

        private void guna2CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            //if (guna2CheckBox2.Checked == true)
            //{
            //    bunifuTextBox5.UseSystemPasswordChar = false;
               
            //}
            //else if (guna2CheckBox2.Checked == false)
            //{
            //    bunifuTextBox5.UseSystemPasswordChar = true;
                

            //}
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Password = Login.PassWord;
            var input = bunifuTextBox4.Text;
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]+");

            if (bunifuTextBox4.Text != "" || bunifuTextBox1.Text != "")
            {
                if (bunifuTextBox4.Text == bunifuTextBox1.Text)
                {
                    if (bunifuTextBox4.Text.Length < 8 || !hasNumber.IsMatch(input) || !hasUpperChar.IsMatch(input) || !hasLowerChar.IsMatch(input) || !hasSymbols.IsMatch(input))
                    {
                        MessageBox.Show("Password must be of Minimum 8 Characters with at least One Uppercase, One Lowercase, One Numeric, and One Special Characters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        bunifuTextBox4.Focus();
                    }
                    else
                    {
                        if (bunifuTextBox4.Text == Password)
                        {
                            MessageBox.Show("New Password cannot be the same as your Old Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                        else
                        {
                            SqlConnection con = new SqlConnection(connection_string_1);
                            con.Open();
                            SqlCommand cmd = new SqlCommand("update LoginTable set Password='" + bunifuTextBox4.Text + "' where Username ='" + Username + "'", con);
                            SqlDataReader sdr = cmd.ExecuteReader();
                            con.Close();
                            MessageBox.Show("Password Change Successfully ", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            panel1.Visible = true;
                            panel2.Visible = false;
                            bunifuTextBox5.Focus();
                            ClearAll();
                            this.Hide();
                            Login l = new Login();
                            l.Show();
                        }
                        
                    }
                    
                }
                else
                {
                    MessageBox.Show("Passwords Don’t Match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    bunifuTextBox4.Focus();
                }
            }
            else
            {
                MessageBox.Show("Please Enter Details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bunifuTextBox5.Focus();
            }
            
        }

        public void ClearAll()
        {
            bunifuTextBox5.Clear();
            bunifuTextBox4.Clear();
            bunifuTextBox1.Clear();
        }

        public void PasswordValidation()
        {
            var input = bunifuTextBox4.Text;
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]+");

            if (bunifuTextBox4.Text == "")
            {
                label2.Text = "";
            }
            else
            {
                if (bunifuTextBox4.Text.Length >= 1 && bunifuTextBox4.Text.Length < 5)
                {
                    label2.Text = "Weak";
                    label2.ForeColor = System.Drawing.Color.Red;
                }
                else if (bunifuTextBox4.Text.Length >= 5 && bunifuTextBox4.Text.Length < 8 && hasLowerChar.IsMatch(input) && hasUpperChar.IsMatch(input))
                {
                    label2.Text = "Average";
                    label2.ForeColor = System.Drawing.Color.Gray;
                }
                else if (bunifuTextBox4.Text.Length >= 8 && hasNumber.IsMatch(input) && hasSymbols.IsMatch(input))
                {
                    label2.Text = "Strong";
                    label2.ForeColor = System.Drawing.Color.Green;
                }
            }
            
        }
        public void PasswordValidation1()
        {
            if (bunifuTextBox1.Text == "")
            {
                label3.Text = "";
            }
            else
            {
                if(bunifuTextBox1.Text!="")
                {
                    if (bunifuTextBox4.Text == bunifuTextBox1.Text || bunifuTextBox1.Text == bunifuTextBox4.Text)
                    {
                        label3.Text = "Match";
                        label3.ForeColor = System.Drawing.Color.Green;
                        guna2Button2.Enabled = true;
                    }
                    else
                    {
                        label3.Text = "Not Match";
                        label3.ForeColor = System.Drawing.Color.Red;
                        guna2Button2.Enabled = false;
                    }
                }
                
            }

            

        }

        public void PasswordValidation2()
        {
            if (bunifuTextBox4.Text == "")
            {
                label3.Text = "";
            }
            else
            {
                if (bunifuTextBox1.Text != "")
                {
                    if (bunifuTextBox4.Text == bunifuTextBox1.Text || bunifuTextBox1.Text == bunifuTextBox4.Text)
                    {
                        label3.Text = "Match";
                        label3.ForeColor = System.Drawing.Color.Green;
                        guna2Button2.Enabled = true;
                    }
                    else
                    {
                        label3.Text = "Not Match";
                        label3.ForeColor = System.Drawing.Color.Red;
                        guna2Button2.Enabled = false;
                    }
                }
            }



        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            bunifuTextBox5.UseSystemPasswordChar = true;
            guna2CircleButton2.Visible = true;
            guna2CircleButton1.Visible = false;
        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
           
            bunifuTextBox5.UseSystemPasswordChar = false;
            guna2CircleButton1.Visible = true;
            guna2CircleButton2.Visible = false;
        }

        private void bunifuTextBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                guna2Button1_Click(this, new EventArgs());
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void guna2CircleButton4_Click(object sender, EventArgs e)
        {
            bunifuTextBox4.UseSystemPasswordChar = true;
            bunifuTextBox1.UseSystemPasswordChar = true;
            guna2CircleButton3.Visible = true;
            guna2CircleButton4.Visible = false;
            
        }

        private void guna2CircleButton3_Click(object sender, EventArgs e)
        {
         
            bunifuTextBox4.UseSystemPasswordChar = false;
            bunifuTextBox1.UseSystemPasswordChar = false;
            guna2CircleButton4.Visible = true;
            guna2CircleButton3.Visible = false;
            

        }

        private void bunifuTextBox1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void bunifuTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void bunifuTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                guna2Button2_Click(this, new EventArgs());
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
