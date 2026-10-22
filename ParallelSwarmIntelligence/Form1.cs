using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParallelSwarmIntelligence
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private async void btnParallelSwarms_Click(object sender, EventArgs e)
        {
            int numSwarmsToLaunch = 10;
            Task<SwarmResult>[] TaskArr =
            new Task<SwarmResult>[numSwarmsToLaunch];
            for (int i = 0; i < TaskArr.Length; i++)
            {
                TaskArr[i] = Task.Factory.StartNew<SwarmResult>(
                (obj) =>
                {
                    SwarmSystem ss = new SwarmSystem((int)obj);
                    ss.Initialize();
                    SwarmResult sr = ss.DoPSO();
                    return sr;
                }, i);
            }
            Task.WaitAll(TaskArr); // wait for all tasks to finish
            List<SwarmResult> RList = new List<SwarmResult>();
            await Task.Factory.ContinueWhenAll(TaskArr,
            (tsks) =>
            {
                Console.WriteLine(tsks.Length.ToString() + " tasks");
                for (int i = 0; i < tsks.Length; i++)
                    RList.Add(tsks[i].Result);
            });

            RList.Sort();
            dg1.DataSource = RList;
            dg1.Refresh();
            lblResult.Text = RList[0].ToString();
        }
        private void btnSequentialSwarm_Click(object sender, EventArgs e)
        {
            SwarmSystem ss = new SwarmSystem(0);
            ss.Initialize();
            SwarmResult sr = ss.DoPSO();
            lblResult.Text = sr.ToString();
        }
    }
}
