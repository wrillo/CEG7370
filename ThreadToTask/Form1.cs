/* *****************************************************************
 * William Rillo                                                   *
 * Wright State University                                         *
 * CS7370                                                          *
 * Homework Assignment 3                                           *
 * Transforming a multi-threaded program into a task-split program *
 * *****************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ThreadToTask
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnMyCompute_Click(object sender, EventArgs e)
        {
            // Multi-threading code
            MyCompute mc = new MyCompute();
            mc.Data1 = 24.22f;
            mc.Data2 = 11;
            var threadStopWatch = new Stopwatch();
            threadStopWatch.Start();
            Action fCompute = new Action(mc.Compute3);
            Thread thCompute = new Thread(new ThreadStart(fCompute));
            thCompute.Start(); 
            thCompute.Join();
            threadStopWatch.Stop();
            MessageBox.Show("Result = " + mc.Res.ToString() + "\nTimer = " + threadStopWatch.ElapsedMilliseconds.ToString() + " milliseconds");
        }

        private void btnMyComputeTask_Click(object sender, EventArgs e)
        {
            // Task equivalent of the above threading code
            // Note locks are used in the MyCompute class for both
            // Threading and Task cases
            MyCompute mc = new MyCompute();
            mc.Data1 = 24.22f;
            mc.Data2 = 11;
            var taskStopWatch = new Stopwatch();
            taskStopWatch.Start();
            Action fCompute = new Action(mc.Compute3);
            Task t1 = new Task(fCompute); 
            t1.Start();
            t1.Wait();
            taskStopWatch.Stop();
            MessageBox.Show("Result = " + mc.Res.ToString() + "\nTimer = " + taskStopWatch.ElapsedMilliseconds.ToString() + " milliseconds");
        }
    }
}
