using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace The_Book_Hub
{
    public partial class ForgotPassword : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int RightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        public ForgotPassword()
        {
            
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
            //bunifuTextBox1.Focus();
        }

        private void ForgotPassword_Load(object sender, EventArgs e)
        {
            this.Text = Application.ProductName + " " + Application.ProductVersion;
            //bunifuTextBox1.Focus();
        }

        private void ForgotPassword_FormClosing(object sender, FormClosingEventArgs e)
        {
            //System.Windows.Forms.Application.Exit();
        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPages1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void tabPage1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void tabPage2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void bunifuButton21_Click_1(object sender, EventArgs e)
        {
            //bunifuPages1.SetPage(1);
        }

        private void bunifuButton23_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuButton24_Click(object sender, EventArgs e)
        {
            
                        
        }

        private void bunifuTextBox2_TextChanged(object sender, EventArgs e)
        {
            //if (bunifuTextBox2.TextLength == 1)
            //{
            //    bunifuTextBox3.Focus();
            //}

        }

        private void bunifuTextBox3_TextChanged(object sender, EventArgs e)
        {
            //if (bunifuTextBox3.TextLength == 1)
            //{
            //    bunifuTextBox4.Focus();
            //}
        }

        private void bunifuTextBox4_TextChanged(object sender, EventArgs e)
        {
            //if (bunifuTextBox4.TextLength == 1)
            //{
            //    bunifuTextBox5.Focus();
            //}
        }

        private void bunifuTextBox5_TextChanged(object sender, EventArgs e)
        {
            //if (bunifuTextBox5.TextLength == 1)
            //{
            //    //bunifuTextBox.Focus();
            //}
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            //bunifuTextBox1.Focus();
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bunifuButton22_Click(this, new EventArgs());
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void bunifuButton22_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton22_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    bunifuTextBox2_TextChanged(this, new EventArgs());
            //    e.Handled = e.SuppressKeyPress = true;
            //}
        }

        private void bunifuTextBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bunifuButton21_Click_1(this, new EventArgs());
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            bunifuTextBox10.Focus();
        }

        private void bunifuPages1_Click(object sender, EventArgs e)
        {
            bunifuTextBox10.Focus();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuTextBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuButton27_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuButton25_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton26_Click(object sender, EventArgs e)
        {
            this.Close();
            Login l = new Login();
            l.Show();
        }
    }
}
