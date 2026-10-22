using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ActionDelegate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnPress_Click(object sender, EventArgs e)
        {
            /*
            ActionDelegate X1 = new ActionDelegate();
            Action<int, int> act = new Action<int, int>(X1.Mycompute);
            act(5, 7);
            double result = X1.Res;
            */
            Func<int, int, double> FuncCompute = (a, b) => a * b;
            double result = FuncCompute(5, 7);
            MessageBox.Show(result.ToString());            
        }
    }
}
