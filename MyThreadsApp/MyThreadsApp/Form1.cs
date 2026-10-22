using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyThreadsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            btnTest.Click += new EventHandler(myHandler);
            btnRight.Click += new EventHandler(myHandler);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Test Button Pressed");
            btnTest.Text = "Test is done";
            btnTest.Enabled = false;
        }

        private void myHandler(object sender, EventArgs e)
        {
            if(sender.Equals(btnTest))
            {
                MessageBox.Show("Test will now be disabled");
            }
            else
                MessageBox.Show("Handle my nuts");
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Don't touch my bottom");
        }
    }
}
