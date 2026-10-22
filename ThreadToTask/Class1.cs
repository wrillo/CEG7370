using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadToTask
{
    internal class MyCompute
    {
        object oLock = new object();
        double res; public double Res
        {
            get
            {
                double ress = 0;
                lock (oLock) { ress = res; }
                return ress;
            }
        }
        float data1;
        public float Data1
        {
            get
            {
                float d1 = 0; lock (oLock) { d1 = data1; }
                return d1;
            }
            set { lock (oLock) { data1 = value; } }
        }
        int data2; public int Data2
        {
            get { int d2 = 0; lock (oLock) { d2 = data2; } return d2; }
            set { lock (oLock) { data2 = value; } }
        }
        public void Compute3()
        {
            double result = 0; lock (oLock) { result = data1 * data2 - 23.33; }
            Thread.Sleep(1000);
            lock (oLock) { res = result; }
        }
    }
}
