using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace ActionDelegate
{
    internal class ActionDelegate
    {
        double res = 0;
        public double Res { get { return res; } }
        public void Mycompute(int a, int b)
        {
            res = a * b;
        }
    }
}
