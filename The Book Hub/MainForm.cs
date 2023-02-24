using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace The_Book_Hub
{
    public partial class MainForm : Form
    {
        

        string connection_string_1 = System.Configuration.ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString;

        static string Username;
        
        
        public void GetName()
        {
            Username = Login.UserName;
            SqlConnection con = new SqlConnection(connection_string_1);
            string query = "select * from LoginTable where Username=@Username";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Username", Username);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                string Name = sdr["Name"].ToString();
                
                label2.AutoSize = false;
                label2.TextAlign = ContentAlignment.MiddleLeft;
                label2.Dock = DockStyle.None;
                label2.Text = Name;

               


            }

            con.Close();
        }

        public void GetProfilePic()
        {
            SqlConnection con = new SqlConnection(connection_string_1);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(new SqlCommand("select ProfilePic from LoginTable where Username = '" + Username + "'", con));
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            if (dataSet.Tables[0].Rows.Count == 1)
            {
                Byte[] data = new Byte[0];
                data = (Byte[])(dataSet.Tables[0].Rows[0]["ProfilePic"]);
                MemoryStream mem = new MemoryStream(data);
                bunifuPictureBox1.Image = Image.FromStream(mem);
            }
        }



        private Button currentButton;
        private Random random;
        private int tempindex;
        private Form activeForm;
        public MainForm()
        {

            InitializeComponent();
            random = new Random();
            button10.Visible = false;
            GetName();
            GetProfilePic();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            hideSubMenu();
            label1.Visible = false;
            timer1.Start();
            label3.Text = DateTime.Now.ToLongTimeString();
            label4.Text = DateTime.Now.ToLongDateString();
            this.Text = Application.ProductName + " " + Application.ProductVersion;
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            Reset();
        }
        private void Reset()
        {
            DisableButton();
            DisableButton1();
            label1.Visible = false;
            pictureBox3.Visible = true;
         
            panel3.BackColor = Color.FromArgb(48, 103, 84);
            panel2.BackColor = Color.DarkSlateGray;
            currentButton = null;
            button10.Visible = false;
            panel5.Visible = false;
            panel5.BackColor = Color.FromArgb(48, 103, 84);
            panel6.Visible = false;
            panel6.BackColor = Color.FromArgb(48, 103, 84);

        }

        private void ActivateButton(object btnsender)
        {
            if (btnsender != null)
            {
                if (currentButton != (Button)btnsender)
                {
                    DisableButton();
                    Color color = SelectThemeColor();
                    currentButton = (Button)btnsender;
                    currentButton.BackColor = color;
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    panel3.BackColor = color;
                    panel2.BackColor = Colours.ChangeColorBrightness(color, -0.3);
                    Colours.PrimaryColor = color;
                    Colours.SecondaryColor = Colours.ChangeColorBrightness(color, -0.3);
                    button10.Visible = true;
                }
            }
        }

        private void ActivateButton1(object btnsender)
        {
            if (btnsender != null)
            {
                if (currentButton != (Button)btnsender)
                {
                    DisableButton1();
                    DisableButton2();
                    Color color = SelectThemeColor();
                    currentButton = (Button)btnsender;
                    currentButton.BackColor = color;
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    panel3.BackColor = color;
                    panel2.BackColor = Colours.ChangeColorBrightness(color, -0.3);
                    Colours.PrimaryColor = color;
                    Colours.SecondaryColor = Colours.ChangeColorBrightness(color, -0.3);
                    button10.Visible = true;

                }
            }
        }

        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panel4.Controls.Add(childForm);
            this.panel4.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            pictureBox3.Visible = false;
            label1.Visible = true;
            label1.Text = childForm.Text;
        }

        private void OpenChildForm1(Form childForm, object btnSender)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            ActivateButton1(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panel4.Controls.Add(childForm);
            this.panel4.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            pictureBox3.Visible = false;
            label1.Visible = true;
            label1.Text = childForm.Text;
        }

        private Color SelectThemeColor()
        {
            int index = random.Next(Colours.ColorList.Count);
            while (tempindex == index)
            {
                index = random.Next(Colours.ColorList.Count);
            }
            tempindex = index;
            string color = Colours.ColorList[index];
            return ColorTranslator.FromHtml(color);
        }
        private void DisableButton()
        {
            foreach (Control previousBtn in panel1.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(47, 90, 90);
                    previousBtn.ForeColor = Color.Gainsboro;
                    previousBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }

        private void DisableButton1()
        {
            foreach (Control previousBtn in panel5.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(48, 103, 84);
                    previousBtn.ForeColor = Color.Gainsboro;
                    previousBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }

        private void DisableButton2()
        {
            foreach (Control previousBtn in panel6.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(48, 103, 84);
                    previousBtn.ForeColor = Color.Gainsboro;
                    previousBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.Dashboard(), sender);
            hideSubMenu();
            DisableButton1();
            DisableButton2();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        private void hideSubMenu()
        {
            panel5.Visible = false;
            panel6.Visible = false;
        }

        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
               
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

            OpenChildForm(new Forms.Student(), sender);
            showSubMenu(panel5);
            DisableButton1();
            DisableButton2();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            OpenChildForm1(new Forms.AddStudent(), sender);
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            OpenChildForm1(new Forms.ViewStudents(), sender);
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.Book(), sender);
            showSubMenu(panel6);
            DisableButton1();
            DisableButton2();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenChildForm1(new Forms.AddBook(), sender);
        }

        private void button9_Click(object sender, EventArgs e)
        {

            Login l = new Login();
            l.Show();
            this.Hide();


        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenChildForm1(new Forms.ViewBooks(), sender);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OpenChildForm1(new Forms.IssueBook(), sender);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            OpenChildForm1(new Forms.ReturnBook(), sender);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            OpenChildForm1(new Forms.IssuedReturnBooksDetails(), sender);
        }

        private void button13_Click(object sender, EventArgs e)
        {
           
            OpenChildForm1(new Forms.IssuedReturnBooksSearch(), sender);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            DisableButton1();
            DisableButton2();
            OpenChildForm(new Forms.Setting(), sender);
           
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPictureBox1_Click(object sender, EventArgs e)
        {

        }

 
    }
}
