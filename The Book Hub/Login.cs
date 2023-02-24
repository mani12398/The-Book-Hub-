using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace The_Book_Hub
{
    public partial class Login : Form
    {
        
       
        string connection_string_1 = System.Configuration.ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString;

        public static string to;

        static string Username;
        static string Password;
        public static string UserName
        {
            get { return Username; }
            set { Username = value; }
        }

        public static string PassWord
        {
            get { return Password; }
            set { Password = value; }
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int RightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        public Login()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
            LoadCredentials();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            label9.Text = "";
            label3.Text = "";

            guna2CircleButton4.Visible = false;
            bunifuTextBox2.UseSystemPasswordChar = true;
            guna2CheckBox2.Checked = true;

            AutoLoad();

            guna2CircleButton1.Visible = false;
            guna2CheckBox1.Checked= true;

            panel3.Visible = false;
            panel4.Visible = false;

            Label();

            bunifuTextBox4.UseSystemPasswordChar = true;
            bunifuTextBox3.UseSystemPasswordChar = true;

            bunifuTextBox10.Focus();

            bunifuButton27.Enabled = false;
            bunifuButton25.Enabled = false;

            bunifuTextBox9.Enabled = false;
            this.Size = new Size(775, 600);

            //Default Size this.Size = new Size(775, 600);

            panel3.Location = new Point(375, 0);
            panel4.Location = new Point(375, 0);
            Credentials();


            bunifuTextBox5.Visible = false;
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            System.Windows.Forms.Application.Exit();
        }

        private void bunifuButton23_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        public void ClearAll()
        {
            bunifuTextBox1.Clear();
            bunifuTextBox2.Clear();
            guna2CheckBox2.Checked = true;
            
        }
        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);

        }

        public void AutoLoad()
        {
            AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
            SqlDataReader dReader;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connection_string_1;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select distinct [Username] from [LoginTable]" + " order by [Username] asc";
            conn.Open();
            dReader = cmd.ExecuteReader();
            if (dReader.HasRows == true)
            {
                while (dReader.Read())
                {
                    namesCollection.Add(dReader["Username"].ToString());
                }
                    

            }
            else
            {
                
            }
            dReader.Close();

            bunifuTextBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            bunifuTextBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            bunifuTextBox1.AutoCompleteCustomSource = namesCollection;
            bunifuTextBox1.PlaceholderText = "Username";

        }

        

        private void guna2CheckBox1_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        public void SaveCredentials()
        {
            if (guna2CheckBox1.Checked == true)
            {
                Properties.Settings.Default.Username = bunifuTextBox1.Text;
                Properties.Settings.Default.Password = bunifuTextBox2.Text;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Username = "";
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.Save();
            }
        }

        public void LoadCredentials()
        {
            if (Properties.Settings.Default.Username != string.Empty) 
            {
                bunifuTextBox1.Text = Properties.Settings.Default.Username;
                bunifuTextBox2.Text = Properties.Settings.Default.Password;
            }
        }

       

        private void bunifuButton24_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            ForgotPassword f = new ForgotPassword();
            f.Show();
        }

        private void bunifuButton21_Click_1(object sender, EventArgs e)
        {
            if (guna2CheckBox3.Checked == true)
            {
                StudentSearch s = new StudentSearch();
                s.Show();
                this.Hide();
            }

            if (guna2CheckBox2.Checked == true)
            {
                SqlConnection sqlcon = new SqlConnection(connection_string_1);
                sqlcon.Open();
                string query = "select * from LoginTable where Username='" + bunifuTextBox1.Text.Trim() + "' and Password='" + bunifuTextBox2.Text.Trim() + "'";
                SqlDataAdapter sda = new SqlDataAdapter(query, sqlcon);
                DataTable dtbl = new DataTable();

                sda.Fill(dtbl);
                if (bunifuTextBox1.Text == "" || bunifuTextBox2.Text == "")
                {
                    MessageBox.Show("Please Enter Details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    bunifuTextBox1.Focus();
                    return;
                }

                else
                {
                    if (dtbl.Rows.Count == 1)
                    {
                        using(StreamWriter sw =new StreamWriter("Credentials.txt"))
                        {
                            sw.WriteLine(bunifuTextBox1.Text);
                            sw.WriteLine(bunifuTextBox2.Text);
                        }
                        UserName = bunifuTextBox1.Text;
                        PassWord = bunifuTextBox2.Text;
                        SaveCredentials();
                        MainForm m = new MainForm();
                        m.Show();
                        this.Hide();
                        ClearAll();
                        bunifuTextBox1.Focus();
                    }

                    else
                    {
                        
                        MessageBox.Show("Invalid Login Details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
                        bunifuTextBox1.Focus();

                    }
                }


                sqlcon.Close();
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuTextBox1_TextChanged_1(object sender, EventArgs e)
        {
            if (bunifuTextBox1.Text != "" && bunifuTextBox2.Text != "")
            {
                bunifuButton21.Enabled = true;

            }
            else
            {
                bunifuButton21.Enabled = false;

            }
            if (bunifuTextBox1.Text != "" || bunifuTextBox2.Text != "")
            {
                bunifuButton22.Enabled = true;

            }
            else
            {
                bunifuButton22.Enabled = false;

            }
        }

        private void bunifuTextBox2_TextChanged_1(object sender, EventArgs e)
        {
            if (bunifuTextBox1.Text != "" && bunifuTextBox2.Text != "")
            {
                bunifuButton21.Enabled = true;

            }
            else
            {
                bunifuButton21.Enabled = false;

            }

            if (bunifuTextBox1.Text != "" || bunifuTextBox2.Text != "")
            {
                bunifuButton22.Enabled = true;

            }
            else
            {
                bunifuButton22.Enabled = false;

            }

        }

        private void guna2CheckBox1_CheckedChanged_2(object sender, EventArgs e)
        {

        }

        private void guna2CheckBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            if (guna2CheckBox2.Checked == true)
            {
                guna2CheckBox3.Checked = false;
                if (bunifuTextBox1.Text == "" || bunifuTextBox2.Text == "")
                {
                    bunifuButton21.Enabled = false;
                    bunifuButton22.Enabled = false;
                }
                else
                {
                    bunifuButton21.Enabled = true;
                    bunifuButton22.Enabled = true;
                }
            }
            if (guna2CheckBox2.Checked == false)
            {
                guna2CheckBox3.Checked = true;
            }
        }

        private void guna2CheckBox3_CheckedChanged_1(object sender, EventArgs e)
        {
            bunifuTextBox1.Clear();
            bunifuTextBox2.Clear();


            if (guna2CheckBox3.Checked == true)
            {
                guna2CheckBox2.Checked = false;
                bunifuTextBox1.Enabled = false;
                bunifuTextBox2.Enabled = false;

                bunifuButton22.Enabled = false;
                bunifuButton21.Enabled = true;
                guna2CircleButton1.Enabled = false;
                guna2CircleButton2.Enabled = false;
                guna2CheckBox1.Enabled = false;
            }

            if (guna2CheckBox3.Checked == false)
            {
                guna2CheckBox2.Checked = true;
                bunifuTextBox1.Enabled = true;
                bunifuTextBox2.Enabled = true;

                bunifuButton22.Enabled = true;
                guna2CircleButton1.Enabled = true;
                guna2CircleButton2.Enabled = true;
                guna2CheckBox1.Enabled = true;
            }
        }

        private void bunifuButton22_Click_1(object sender, EventArgs e)
        {
            ClearAll();
            bunifuTextBox2.UseSystemPasswordChar = true;
            guna2CircleButton2.Visible = true;
            guna2CircleButton1.Visible = false;

        }

        private void bunifuButton24_Click_2(object sender, EventArgs e)
        {
            panel3.Visible = true;
            panel1.Visible = false;
            bunifuTextBox10.Focus();
        }

        private void guna2CircleButton2_Click_1(object sender, EventArgs e)
        {
            bunifuTextBox2.UseSystemPasswordChar = false;
            guna2CircleButton1.Visible = true;
            guna2CircleButton2.Visible = false;
        }

        private void guna2CircleButton1_Click_1(object sender, EventArgs e)
        {
            bunifuTextBox2.UseSystemPasswordChar = true;
            guna2CircleButton2.Visible = true;
            guna2CircleButton1.Visible = false;
        }

        private void guna2CheckBox3_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (guna2CheckBox3.Checked == true)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    bunifuButton21_Click_1(this, new EventArgs());
                    e.Handled = e.SuppressKeyPress = true;
                }
            }
            
        }

        private void bunifuTextBox2_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bunifuButton21_Click_1(this, new EventArgs());
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        int counter = 0;
        int len = 0;
        string txt;

        private void bunifuLabel1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            counter++;
            if (counter > len)
            {
                counter = 0;
                label1.Text = "";
            }
            else
            {
                bunifuLabel1.Text = txt.Substring(0, counter);
            }
           
        }

        public void Label()
        {
            txt = bunifuLabel1.Text;
            len = txt.Length;
            bunifuLabel1.Text = "";
            timer1.Start();
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        public void Credentials()
        {
            if(File.Exists("Credentials.txt"))
            {
                using (StreamReader sw = new StreamReader("Credentials.txt"))
                {
                    bunifuTextBox1.Text = sw.ReadLine();
                    bunifuTextBox2.Text = sw.ReadLine();
                }
            }
            
        }

        private void bunifuButton28_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuButton26_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel1.Visible = true;
        }

        private void bunifuTextBox10_TextChanged(object sender, EventArgs e)
        {
            if (bunifuTextBox10.Text != "")
            {
                bunifuButton27.Enabled = true;
            }
            else if(bunifuTextBox10.Text == "")
            {
                bunifuButton27.Enabled = false;
            }
        }
        string randomcode;
        private void bunifuButton27_Click(object sender, EventArgs e)
        {
            
            var Email = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zAZ]{2,9})$+";
            
            
            if (bunifuTextBox10.Text == "")
            {
                
                MessageBox.Show("Please Enter Email", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bunifuButton27.Focus();
                return;

            }
            else
            {
                if (!Regex.IsMatch(bunifuTextBox10.Text, Email))
                {
                    System.Media.SystemSounds.Beep.Play();
                    MessageBox.Show("Please Provide Valid Email", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    bunifuTextBox10.Focus();
                    bunifuTextBox10.Text = string.Empty;
                }
                else
                {
                    if (!check_email())
                    {
                        MessageBox.Show("Email Not Exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        bunifuTextBox10.Focus();
                    }
                    else
                    {
                        
                        string from, pass, messagebody;
                        Random rand = new Random();
                        randomcode = (rand.Next(9999)).ToString();
                        MailMessage message = new MailMessage();
                        to = (bunifuTextBox10.Text).ToString();
                        from = "muhammadabdurrehman516@gmail.com";
                        pass = "drwbgnzrjmbimjve";
                        messagebody = $"Your Reset Code is {randomcode}";
                        message.To.Add(to);
                        message.From = new MailAddress(from);
                        message.Body = messagebody;
                        message.Subject = "The Book Hub Password Reset Code";
                        SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                        smtp.EnableSsl = true;
                        smtp.Port = 587;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Credentials = new NetworkCredential(from, pass);
                        try
                        {
                            smtp.Send(message);
                            MessageBox.Show("OTP Send Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            bunifuTextBox9.Enabled = true;
                            bunifuTextBox9.Focus();
                            //GetOldPassword();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            
                        }
                    }
                }
                
            }


        }

        private void bunifuTextBox9_TextChanged(object sender, EventArgs e)
        {
            if (bunifuTextBox9.Text != "")
            {
                bunifuButton25.Enabled = true;
            }
            else if (bunifuTextBox9.Text == "")
            {
                bunifuButton25.Enabled = false;
            }

            //if (bunifuTextBox9.TextLength == 1)
            //{
            //    bunifuTextBox8.Focus();
            //}
        }

        private void bunifuTextBox8_TextChanged(object sender, EventArgs e)
        {
            //if (bunifuTextBox8.TextLength == 1)
            //{
            //    bunifuTextBox7.Focus();
            //}
        }

        private void bunifuTextBox7_TextChanged(object sender, EventArgs e)
        {
            //if (bunifuTextBox7.TextLength == 1)
            //{
            //    bunifuTextBox6.Focus();
            //}
        }

        private void bunifuTextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuButton25_Click(object sender, EventArgs e)
        {
           

            if (bunifuTextBox9.Text == "")
            {
                
                MessageBox.Show("Please Enter Code", "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                bunifuTextBox9.Focus();
                return;
            }
            else
            {
                if (randomcode == (bunifuTextBox9.Text).ToString())
                {
                    to = bunifuTextBox10.Text;
                    panel3.Visible = false;
                    panel4.Visible = true;
                    bunifuTextBox4.Focus();
                    GetOldPassword();
                    bunifuButton210.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Invalid OTP", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    bunifuTextBox9.Focus();
                    
                }
            }

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void bunifuTextBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bunifuButton25_Click(this, new EventArgs());
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void bunifuTextBox10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bunifuButton27_Click(this, new EventArgs());
                e.Handled = e.SuppressKeyPress = true;
            }
            if (e.KeyCode == Keys.Enter)
            {
                bunifuTextBox9_TextChanged(this, new EventArgs());
                e.Handled = e.SuppressKeyPress = true;
            }
            
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuButton211_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
            panel1.Visible = true;
        }

        private void bunifuButton210_Click(object sender, EventArgs e)
        {
            

            

            //Password = Login.PassWord;
            var input = bunifuTextBox4.Text;
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]+");

            if (bunifuTextBox4.Text != "" || bunifuTextBox3.Text != "")
            {
                if (bunifuTextBox5.Text == bunifuTextBox4.Text)
                {
                    MessageBox.Show("New Password cannot be the same as your Old Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    bunifuTextBox4.Focus();
                }
                else
                {
                    if (bunifuTextBox4.Text == bunifuTextBox3.Text)
                    {
                        if (bunifuTextBox4.Text.Length < 8 || !hasNumber.IsMatch(input) || !hasUpperChar.IsMatch(input) || !hasLowerChar.IsMatch(input) || !hasSymbols.IsMatch(input))
                        {
                            MessageBox.Show("Password must be of Minimum 8 Characters with at least One Uppercase, One Lowercase, One Numeric, and One Special Characters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            bunifuTextBox4.Focus();
                        }
                        else
                        {
                           
                                SqlConnection con = new SqlConnection(connection_string_1);
                                con.Open();
                                SqlCommand cmd = new SqlCommand("update LoginTable set Password='" + bunifuTextBox4.Text + "' where Email ='" + to + "'", con);
                                SqlDataReader sdr = cmd.ExecuteReader();
                                con.Close();
                                MessageBox.Show("Password Change Successfully ", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                panel4.Visible = false;
                                panel1.Visible = true;
                                bunifuTextBox4.Clear();
                                bunifuTextBox3.Clear();
                            

                        }

                    }
                    else
                    {
                        MessageBox.Show("Passwords Don’t Match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        bunifuTextBox4.Focus();
                    }
                }
                
            }
            else
            {
                MessageBox.Show("Please Enter Details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bunifuTextBox4.Focus();
            }


        }

        private void bunifuTextBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void bunifuTextBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void bunifuTextBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void bunifuTextBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        bool check_email()
        {
            //Boolean email = false;
            string myquery = "select * from LoginTable where Email='" + bunifuTextBox10.Text + "'";
            SqlConnection con = new SqlConnection(connection_string_1);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = myquery;
            cmd.Connection = con;
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                con.Close();
                return true;
            }
            else
            {
                con.Close();
                return false;
            }
            
            
        }

        private void bunifuTextBox9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bunifuButton25_Click(this, new EventArgs());
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void bunifuTextBox5_TextChanged(object sender, EventArgs e)
        {
            

        }

        public void GetOldPassword()
        {
            string myquery = "select * from LoginTable where Email='" + bunifuTextBox10.Text + "'";
            SqlConnection con = new SqlConnection(connection_string_1);
            SqlCommand cmd = new SqlCommand(myquery, con);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                bunifuTextBox5.Text = sdr["Password"].ToString();
            }
            con.Close();
        }

        private void bunifuTextBox6_TextChanged_1(object sender, EventArgs e)
        {
            //SqlCommand com;

            //string str;

            //SqlConnection con = new SqlConnection(connStr);

            //con.Open();

            //str = "select * from std where REGDNO='" + TextBox1.Text.Trim() + "'";

            //com = new SqlCommand(str, con);

            //SqlDataReader reader = com.ExecuteReader();

            //if (reader.Read())

            //{

            //    TextBox2.Text = reader["Password"].ToString();

            //    TextBox3.Text = reader["BRANCH"].ToString();

            //    TextBox4.Text = reader["ADDRESS"].ToString();

            //    TextBox5.Text = reader["PHNO"].ToString();

            //    TextBox6.Text = reader["STATE"].ToString();

            //    reader.Close();

            //    con.Close();

            //}
        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void bunifuTextBox4_TextChanged(object sender, EventArgs e)
        {
            if (bunifuTextBox4.Text != "" && bunifuTextBox3.Text != "")
            {
                bunifuButton210.Enabled = true;
            }
            else
            {
                bunifuButton210.Enabled = false;

            }
            PasswordValidation();
            PasswordValidation2();
        }

        private void bunifuTextBox3_TextChanged(object sender, EventArgs e)
        {
            if (bunifuTextBox4.Text != "" && bunifuTextBox3.Text != "")
            {
                bunifuButton210.Enabled = true;
            }
            else
            {
                bunifuButton210.Enabled = false;

            }
            PasswordValidation1();
        }

        private void guna2CircleButton3_Click(object sender, EventArgs e)
        {
            bunifuTextBox4.UseSystemPasswordChar = false;
            bunifuTextBox3.UseSystemPasswordChar = false;
            guna2CircleButton4.Visible = true;
            guna2CircleButton3.Visible = false;
        }

        private void guna2CircleButton4_Click(object sender, EventArgs e)
        {
            bunifuTextBox4.UseSystemPasswordChar = true;
            bunifuTextBox3.UseSystemPasswordChar = true;
            guna2CircleButton3.Visible = true;
            guna2CircleButton4.Visible = false;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        //bool check_password()
        //{
        //    //Boolean email = false;
        //    string myquery = "select * from LoginTable where Password='" + bunifuTextBox4.Text + "'";
        //    SqlConnection con = new SqlConnection(connection_string_1);
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandText = myquery;
        //    cmd.Connection = con;
        //    SqlDataAdapter sda = new SqlDataAdapter();
        //    sda.SelectCommand = cmd;
        //    DataSet ds = new DataSet();
        //    sda.Fill(ds);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        con.Close();
        //        return true;
        //    }
        //    else
        //    {
        //        con.Close();
        //        return false;
        //    }


        //}

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

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
                label9.Text = "";
            }
            else
            {
                if (bunifuTextBox4.Text.Length >= 1 && bunifuTextBox4.Text.Length < 5)
                {
                    label9.Text = "Weak";
                    label9.ForeColor = System.Drawing.Color.Red;
                }
                else if (bunifuTextBox4.Text.Length >= 5 && bunifuTextBox4.Text.Length < 8 && hasLowerChar.IsMatch(input) && hasUpperChar.IsMatch(input))
                {
                    label9.Text = "Average";
                    label9.ForeColor = System.Drawing.Color.Gray;
                }
                else if (bunifuTextBox4.Text.Length >= 8 && hasNumber.IsMatch(input) && hasSymbols.IsMatch(input))
                {
                    label9.Text = "Strong";
                    label9.ForeColor = System.Drawing.Color.Green;
                }
            }

        }
        public void PasswordValidation1()
        {
            if (bunifuTextBox3.Text == "")
            {
                label3.Text = "";
            }
            else
            {
                if (bunifuTextBox3.Text != "")
                {
                    if (bunifuTextBox4.Text == bunifuTextBox3.Text || bunifuTextBox3.Text == bunifuTextBox4.Text)
                    {
                        label3.Text = "Match";
                        label3.ForeColor = System.Drawing.Color.Green;
                        bunifuButton210.Enabled = true;
                    }
                    else
                    {
                        label3.Text = "Not Match";
                        label3.ForeColor = System.Drawing.Color.Red;
                        bunifuButton210.Enabled = false;
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
                if (bunifuTextBox3.Text != "")
                {
                    if (bunifuTextBox4.Text == bunifuTextBox3.Text || bunifuTextBox1.Text == bunifuTextBox4.Text)
                    {
                        label3.Text = "Match";
                        label3.ForeColor = System.Drawing.Color.Green;
                        bunifuButton210.Enabled = true;
                    }
                    else
                    {
                        label3.Text = "Not Match";
                        label3.ForeColor = System.Drawing.Color.Red;
                        bunifuButton210.Enabled = false;
                    }
                }
            }



        }

        private void bunifuTextBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bunifuButton210_Click(this, new EventArgs());
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void bunifuButton29_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuTextBox5_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
