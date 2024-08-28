using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace tomograf.deterministyczne

{
    class Nelder_Mead
    {
        private float alpha, beta, gamma, steering_parameter;
        private int dimensions;
        private int number_of_calls = 0, iterations = 0;
        private double[][] simplex;

        public delegate double tested_function(params double[] args);
        private tested_function f;

        private double func(params double[] args)
        {
            number_of_calls++;
            return f(args);
        }

        public Nelder_Mead(float epsilon, tested_function f, params double[][] starting_points)
        {
            this.f = f;
            this.simplex = starting_points;
            this.dimensions = starting_points[0].Length;
            this.alpha = 1f; this.beta = 0.5f; this.gamma = 2f;
            this.steering_parameter = epsilon;
        }

        public double run()
        {
            double[] initial_values = new double[simplex.Length];
            int xh, xl, xg;
            double fh, fl, fg;
            xh = 0; xl = 0; xg = 0;
            fl = 0;

            for (int i = 0; i < initial_values.Length; i++)
            {
                initial_values[i] = func(simplex[i]);
            }

            while (check_condition(initial_values)) {
                iterations++;


                xh = 0; xl = 0; xg = 0;
                fh = initial_values[0]; fl = initial_values[0]; fg = initial_values[0];

                //3
                for (int i = 0; i < simplex.Length; i++)
                {
                    if (initial_values[i] > fh)
                    {
                        xg = xh;
                        fg = fh;

                        xh = i;
                        fh = initial_values[i];
                    }

                    if (initial_values[i] < fl)
                    {
                        xl = i;
                        fl = initial_values[i];
                    }
                }

                //4
                double[] xs = new double[simplex[0].Length];
                for (int i = 0; i < xs.Length; i++)
                {
                    xs[i] = 0;
                }

                for (int j=0; j < simplex.Length; j++)
                {
                    for (int i = 0; i < dimensions; i++)
                    {
                        if (j != xh)
                            xs[i] += simplex[j][i];
                    }
                }

                for (int i = 0; i < xs.Length; i++)
                {
                    xs[i] = xs[i] / (initial_values.Length - 1);
                }


                //5
                double[] xr = new double[dimensions];

                //odbijanie
                for (int i = 0; i < xr.Length; i++)
                {
                    xr[i] = (1 + alpha) * xs[i] - alpha * simplex[xh][i];
                }
                double fr = func(xr);
                //6
                if (fr < fl)
                {
                    //6.1
                    double[] xe = new double[dimensions];
                    for (int i = 0; i < xe.Length; i++)
                    {
                        xe[i] = gamma * xr[i] + (1 - gamma) * xs[i];
                    }
                    double fe = func(xe);

                    //6.1.1
                    if (fe < fl)
                    {
                        for (int i = 0; i < simplex[xh].Length; i++)
                        {
                            simplex[xh][i] = xe[i];
                        }
                        fh = fe;
                        initial_values[xh] = fe;
                    }

                    //6.1.2
                    else
                    {
                        for (int i = 0; i < simplex[xh].Length; i++)
                        {
                            simplex[xh][i] = xr[i];
                        }
                        fh = fr;
                        initial_values[xh] = fr;
                    }

                }
                //6.2
                else if (fr <= fg)
                {
                    for (int i = 0; i < simplex[xh].Length; i++)
                    {
                        simplex[xh][i] = xr[i];
                    }
                    fh = fr;
                    initial_values[xh] = fr;
                }


                //6.3
                else
                {
                    //7.1/7.2
                    if (fr < fh)
                    {
                        for (int i = 0; i < simplex[xh].Length; i++)
                        {
                            simplex[xh][i] = xr[i];
                        }
                        fh = fr;
                        initial_values[xh] = fr;
                    }

                    //8
                    double[] xc = new double[dimensions];
                    for (int i = 0; i < xc.Length; i++)
                    {
                        xc[i] = beta * simplex[xh][i] + (1 - beta) * xs[i];
                    }

                    double fc = func(xc);
                    //9.1
                    if (fc < fh)
                    {
                        for (int i = 0; i < simplex[xh].Length; i++)
                        {
                            simplex[xh][i] = xc[i];
                        }
                        fh = fc;
                        initial_values[xh] = fc;
                    }

                    else
                    {
                        //10
                        double[] tempxl = new double[simplex[xl].Length];

                        for (int i = 0; i < tempxl.Length; i++)
                        {
                            tempxl[i] = simplex[xl][i];
                        }

                        for (int i = 0; i < simplex.Length; i++)
                        {
                            for (int j = 0; j < dimensions; j++)
                            {
                                simplex[i][j] = (simplex[i][j] + tempxl[j]) / 2;
                                
                            }
                            initial_values[i] = func(simplex[i]);
                        }

                    }
                }
            }
            double[] results = new double[simplex[0].Length];
            for (int i = 0; i<results.Length; i++)
            {
                results[i] = simplex[xl][i];
            }
            Console.WriteLine("ilosc iteracji: " + iterations);
            Console.WriteLine("Ilosc wywolan funkcji celu " + number_of_calls);
            Console.WriteLine("pozycja koncowa:");
            foreach(var r in results)
            {
                Console.WriteLine(r);
            }
            Console.WriteLine("wartosc funkcji w pozycji:" + fl);
            return fl;
        }

        private bool check_condition(double[] values)
        {
            double avg_value = values.Sum()/ values.Length;

            //for (int i = 0; i < values.Length; i++)
            //{
            //    values[i] = f(simplex[i]);
            //    avg_value += values[i];
            //}

           // avg_value = avg_value / (values.Length);
            double checked_value = 0;
            for (int i = 0; i < values.Length; i++)
            {
                checked_value += Math.Pow((values[i] - avg_value), 2);
            }
            checked_value = checked_value / values.Length;
            checked_value = Math.Sqrt(checked_value);
            return (checked_value >= steering_parameter);

        }

    }
}
