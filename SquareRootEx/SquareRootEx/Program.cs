using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathClass;

namespace MathClass
{
    delegate double Mydel(double x);
    public class Class1
    {
        double value1;
        double value2;
        public double Square(double x)
        {
            value1 = x;
            return value1 * value1;
        }
        public double SquareRoot(double x)
        {
            value2 = x;
            return Math.Sqrt(value2);
        }
    }
}

namespace L1
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Class1 C = new Class1();
            Mydel d1 = new Mydel(C.Square);
            Mydel d2 = new Mydel(C.SquareRoot);
            double x = d1(5);
            double y = d2(49);
            MessageBox.Show("The square funcation is called and the value is " + "" + x);
            MessageBox.Show("The Squareroot is callaed and the value is " + "" + y);
        }
    }
}