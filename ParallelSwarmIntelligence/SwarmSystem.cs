using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelSwarmIntelligence
{
    class SwarmSystem
    {
        public SwarmSystem(int snum)
        {
            this.swarmNum = snum;
        }
        int swarmNum;
        public int SwarmNum
        {
            get { return swarmNum; }
        }
        public List<Particle> PList = new List<Particle>();
        public double Px { get; set; }
        public double Py { get; set; }
        public double Gx { get; set; }
        public double Gy { get; set; }
        public void Initialize()
        {
            Random ran = new Random();
            for (int i = 0; i < 50; i++) // 50 particles in swarm
            {
                Particle p = new Particle();
                p.W = 0.73;
                p.C1 = 1.4;
                p.C2 = 1.5;
                p.Xx = ran.NextDouble() * 20;
                p.Xy = ran.NextDouble() * 20;
                double num = ran.NextDouble();
                if (num > 0.5)
                {
                    p.Xx = -1 * p.Xx;
                    p.Xy = -1 * p.Xy;
                }
                p.Vx = ran.NextDouble() * 5;
                p.Vy = ran.NextDouble() * 5;
                num = ran.NextDouble();
                if (num > 0.5)
                {
                    p.Vx = -1 * p.Vx;
                    p.Vy = -1 * p.Vy;
                }
                PList.Add(p);
            }
        }
        public double FunctionToSolve(double x, double y)
        {
            // Rosenbrock function
            double res = ((x*x)+y-11)*((x * x) + y - 11) + (x+(y*y)-7)* (x + (y * y) - 7);
            return res;
        }
        public SwarmResult DoPSO() // Particle movement to achieve
        { // for particle swarm optimization
            Gx = PList[0].Xx;
            Gy = PList[0].Xy;
            for (int i = 0; i < 1000; i++) // ietrations
            {
                // find best position in the swarm
                Px = PList[0].Xx;
                Py = PList[0].Xy;
                foreach (Particle pt in PList)
                {
                    if (Math.Abs(FunctionToSolve(pt.Xx, pt.Xy)) <
                    Math.Abs(FunctionToSolve(Px, Py)))
                    {
                        Px = pt.Xx;
                        Py = pt.Xy;
                    }
                }
                if (Math.Abs(FunctionToSolve(Px, Py)) <
                Math.Abs(FunctionToSolve(Gx, Gy)))
                {
                    Gx = Px;
                    Gy = Py;
                }
                foreach (Particle pt in PList)
                {
                    pt.UpdateVelocity(Px, Py, Gx, Gy);
                    pt.UpdatePosition();
                }
            }
            SwarmResult sr = new SwarmResult
            {
                SwarmId = swarmNum,
                X = Gx,
                Y = Gy,
                FunctionValue = FunctionToSolve(Gx, Gy)
            };
            return sr;
        }
    }
}
