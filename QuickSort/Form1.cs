using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickSort
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void btnSortSequential_Click(object sender, EventArgs e)
        {
            double[] data = { 3, 9, 15, 7, 8, 4, 11 };

            quickSortAlgo.Quicksort<double>(data, 0, data.Length - 1);
            string out1 = "";
            foreach (int n in data)
                out1 += n.ToString() + " " + "\n";
            MessageBox.Show(out1);

        }

        private void btnSortSequentialLarge_Click(object sender, EventArgs e)
        {
            int size = 10000000;
            double[] data = new double[size];
            InitData(data);

            Stopwatch sw = new Stopwatch();
            sw.Start();
            quickSortAlgo.Quicksort<double>(data, 0, data.Length - 1);
            sw.Stop();
            MessageBox.Show("Sequential Time Taken : " + sw.ElapsedMilliseconds.ToString());
        }

        void InitData(double[] data)
        {
            Random rand = new Random();
            for (int i = 0; i < data.Length; i++)
                data[i] = rand.NextDouble() * 1000 + 5;
        }

        private void btnSortParallelLarge_Click(object sender, EventArgs e)
        {
            int size = 10000000;
            double[] data = new double[size];

            InitData(data);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            quickSortAlgo.QuickSortParallel<double>(data, 0, data.Length - 1);
            sw.Stop();

            MessageBox.Show("Parallel Time Taken : " + sw.ElapsedMilliseconds.ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnLibrarySort_Click(object sender, EventArgs e)
        {
            int size = 10000000;
            double[] data = new double[size];
            InitData(data);

            Stopwatch sw = new Stopwatch();
            sw.Start();
            Array.Sort(data);
            sw.Stop();
            MessageBox.Show("Library Time Taken : " + sw.ElapsedMilliseconds.ToString());
        }
    }
}
