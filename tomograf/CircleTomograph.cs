using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tomograf
{
    internal class Circle_tomograph : ITomograph
    {
        private double[] x, y, radius, materials;
        private int n_circles, n_lasers;

        public Circle_tomograph(double[] x, double[] y, double[] radius,  int n_lasers, double[] materials)
        {
            this.n_circles = x.Length;
            this.x = x;
            this.y = y;
            this.radius = radius;
            this.n_lasers = n_lasers;
            this.materials = materials;
        }

        private double calc_b(double a, double x, double y)
        {
            return y - a * x;
        }

        public double[][] Run()
        {
            double[][] vector = new double[n_lasers][];
            for (int i = 0; i < n_lasers; i++)
            {
                vector[i] = new double[n_lasers];

                for(int j = 0; j < n_lasers; j++)
                {
                    vector[i][j] = 0;
                }
            }
            for (int k = 0; k < n_circles; k++)
            {
                for (int i = 0; i < n_lasers; i++)
                {
                    for (int j = 0; j < n_lasers; j++)
                    {
                        double a = ((1 - j * (2.0 / (n_lasers - 1))) - (1 - i * (2.0 / (n_lasers - 1)))) / 2;
                        double b = calc_b(a, 1, 1 - j * (2.0 / (n_lasers - 1)));

                        double d = Math.Abs(a * x[k] - y[k] + b) / Math.Sqrt(a*a + 1);

                        if (d < radius[k]) //jesli odleglosc jest mniejsza niz promien, to promien przecina kolo
                        {
                            vector[j][i] += 2 * Math.Sqrt(Math.Pow(radius[k], 2) - Math.Pow(d, 2)) * materials[k];
                        }
                    }
                }
            }

            return vector;
        }
    }
}
