using FirstWCFClient.FRWCF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirstWCFClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnTestWCFMyMath_Click(object sender, EventArgs e)
        {
            // We can use ChannelFactory to communicate with the Service
            // We do need to know the interface
            IMyMath im = new ChannelFactory<IMyMath>(new BasicHttpBinding(),
           new EndpointAddress("http://localhost:8700/MM")).CreateChannel();
            double result = im.ComputeAvg(7, 12, 14);
            MessageBox.Show("Result = " + result.ToString());
            ((IClientChannel)im).Close();
        }

        private void btnTestWCFViaProxy_Click_1(object sender, EventArgs e)
        {
            // For each endpoint, the proxy creates a class which starts
            // with name of the interface (without I) followed by Client
            FRWCF.MyMathClient mmc = new FRWCF.MyMathClient();
            double res = mmc.ComputeAvg(5, 8, 13);
            MessageBox.Show("Result = " + res.ToString());
        }
    }
}
