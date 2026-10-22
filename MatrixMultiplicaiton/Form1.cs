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

namespace MatrixMultiplicaiton
{
    public partial class Form1 : Form
    {
        double[,] A = null;
        double[,] B = null;
        double[,] C = null;
        double[,] D = null;

        Random rand = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            int size = 1000;
            A = new double[size, size];
            B = new double[size, size];
            C = new double[size, size];
            D = new double[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    A[i,j] = rand.NextDouble() * 100;
                    B[i,j] = rand.NextDouble() * 50;
                }
            }
        }

        public double[,] matrixMultiply(double[,] firstMatrix, double[,] secondMatrix, double[,] thirdMatrix)
        {
            // size check
            if((firstMatrix.GetLength(1) != secondMatrix.GetLength(0)) ||
               (secondMatrix.GetLength(1) != thirdMatrix.GetLength(0)))
            {
                System.Console.WriteLine("Matrix Size Mismatch");
                return null;
            }    

            int resultSize = firstMatrix.GetLength(0);
            double[,] interResult = new double[resultSize, resultSize];
            double[,] result = new double[resultSize,resultSize];

            //for (int i = 0; i < resultSize; i++)

            Parallel.For(0, resultSize, (i) =>
            {
                for (int j = 0; j < resultSize; j++)
                {

                    for (int k = 0; k < resultSize; k++)
                    {
                        interResult[i, j] += firstMatrix[i, k] * secondMatrix[k, j];
                    }
                }
            });

            //for (int i = 0; i < resultSize; i++)
            Parallel.For(0, resultSize, (i) =>
            {
                for (int j = 0; j < resultSize; j++)
                {

                    for (int k = 0; k < resultSize; k++)
                    {
                        result[i, j] += interResult[i, k] * thirdMatrix[k, j];
                    }
                }
            });

            return result;

        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            D = matrixMultiply(A, B, C);
            sw.Stop();
            MessageBox.Show("Time taken = " + sw.ElapsedMilliseconds.ToString());
        }
    }
}
