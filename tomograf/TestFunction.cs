using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tomograf.tomografy;

namespace tomograf
{
    class TestFunction
    {
        int n_lasers;
        double[][] values;

        public TestFunction(int n_lasers , double[][] values)
        {
            this.n_lasers = n_lasers;
            this.values = values;
        }


        public double deploy_rect(params double[] args)
        {
            double[] x1 = new double[args.Length / 5];
            double[] x2 = new double[args.Length / 5];
            double[] y1 = new double[args.Length / 5];
            double[] y2 = new double[args.Length / 5];
            double[] m = new double[args.Length / 5];

            for (int i = 0; i < args.Length / 5; i++)
            {
                x1[i] = args[5 * i];
                y1[i] = args[5 * i + 1];
                x2[i] = args[5 * i + 2];
                y2[i] = args[5 * i + 3];
                m[i] = args[5 * i + 4];
            }

            var rect_tom = new Rect_Tomograph(n_lasers, x1, y1, x2, y2, m);
            return deploy(rect_tom.Run());
        }

        public double deploy_circle(params double[] args) 
        {
            double[] x = new double[args.Length / 4];
            double[] y = new double[args.Length / 4];
            double[] r = new double[args.Length / 4];
            double[] m = new double[args.Length/4];

            for (int i = 0; i < args.Length / 4; i++)
            {
                x[i] = args[4 * i];
                y[i] = args[4 * i + 1];
                r[i] = args[4 * i + 2];
                m[i] = args[4 * i + 3];
            }

            var circle_tom = new Circle_tomograph(x, y, r, n_lasers, m);
            return deploy(circle_tom.Run());
        }
        double deploy(double[][] args)
        {
            double val = 0;
            for (int i = 0; i < n_lasers; i++) 
            {
                for (int j = 0; j < n_lasers; j++)
                {
                    val += Math.Abs(args[i][j] - values[i][j]);
                }
            }
            return val;
        }
    }
}
