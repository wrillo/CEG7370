using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Employee
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnCompute_Click(object sender, EventArgs e)
        {
            Emp e1 = new Emp();
            e1.FirstName = txtFirstName.Text;
            e1.LastName = txtLastName.Text;
            e1.HoursWorked = double.Parse(txtHoursWorked.Text);
            e1.HourlyPay = double.Parse(txtHourlyPay.Text);

            double pay = e1.Compute();

            lbpay.Text = "$" + pay.ToString();
        }
    }
}
