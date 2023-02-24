using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace The_Book_Hub
{
    public partial class StudentSearch : Form
    {
        public StudentSearch()
        {
            InitializeComponent();
        }

        private void Student_Load(object sender, EventArgs e)
        {
            this.Text = Application.ProductName + " " + Application.ProductVersion;
        }

        private void Student_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login l = new Login();
            l.Show();
            this.Hide();
        }
    }
}
