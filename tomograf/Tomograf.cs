using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//nie jestem jeszcze pewny co ma zwracac ta funkcja, na razie zrobilem wyswietlanie w konsoli dlugosci promienia "wewnatrz" prostakatu i jaki jest spadek energii


namespace tomograf
{
    internal class Tomograf
    {
        private int n_lasers;
        private int n_squares;
        private double[] x1;
        private double[] y1;
        private double[] x2;
        private double[] y2;
        private double[] materials;

        public Tomograf(int n_lasers, int n_squares, double[] x1, double[] y1, double[] x2, double[] y2, double[] materials)
        {
            this.n_lasers = n_lasers;
            this.n_squares = n_squares;
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
            this.materials = materials;
        }

        private double calc_b(double a, double x, double y)
        {
            return y - a * x;
        }

        public void Run()
        {
            for (int k = 0; k < n_squares; k++)
            {
                Console.WriteLine('\n');
                Console.WriteLine("Kwadrat: " + k);
                for (int i = 0; i < n_lasers; i++)
                {
                    for (int j = 0; j < n_lasers; j++)
                    {

                        double a = ((1 - j * (2.0 / n_lasers)) - (1 - i * (2.0 / n_lasers))) / 2;
                        double b = calc_b(a, 1, 1 - j * (2.0 / n_lasers));

                        //Console.WriteLine(a + "x" + " + " + b);

                        if (i == j)
                        {
                            if (y2[k] >= (1 - j * (2.0 / n_lasers)) && (1 - j * (2.0 / n_lasers)) >= y1[k])
                            {
                                Console.WriteLine("wejscie: " + i + " wyjscie: " + j + " dlugosc promienia: " + (x2[k] - x1[k]) + " strata energii: " + materials[k] * (x2[k] - x1[k]));
                            }
                        }

                        else if (i > j)
                        {
                            //sprawdzanie czy promien "wchodzi" od lewej sciany
                            double y_check = a * x1[k] + b;
                            double entry_x = 1;
                            double entry_y = 1;
                            if (y_check >= y1[k] && y_check <= y2[k])
                            {
                                entry_x = x1[k];
                                entry_y = y_check;
                            }

                            else
                            {
                                //sprawdzanie czy promien wchodzi od gory
                                double x_check = (y2[k] - b) / a;
                                // Console.WriteLine(x_check);
                                if (x_check >= x1[k] && x_check <= x2[k])
                                {
                                    entry_x = x_check;
                                    entry_y = y2[k];
                                }
                            }
                            //Console.WriteLine(entry_x + " " + entry_y);
                            if (entry_x != 1 || entry_y != 1)
                            {
                                //sprawdzanie czy promien wychodzi od prawej sciany
                                y_check = a * x2[k] + b;
                                double exit_x = 0;
                                double exit_y = 0;
                                if (y_check >= y1[k] && y_check <= y2[k])
                                {
                                    exit_x = x2[k];
                                    exit_y = y_check;
                                }

                                else
                                {
                                    //sprawdzanie czy promien wychodzi od dolu
                                    double x_check = (y1[k] - b) / a;
                                    if (x_check >= x1[k] && x_check <= x2[k])
                                    {
                                        exit_x = x_check;
                                        exit_y = x1[k];
                                    }
                                }

                                double distance = Math.Sqrt(Math.Pow((exit_x - entry_x), 2) + Math.Pow((exit_y - entry_y), 2));
                                Console.WriteLine("wejscie: " + i + " wyjscie: " + j + " dlugosc promienia: " + distance + " strata energii: " + materials[k] * (distance));
                            }
                        }

                        else if (i < j)
                        {
                            //sprawdzanie czy promien "wchodzi" od lewej sciany
                            double y_check = a * x1[k] + b;
                            double entry_x = 1;
                            double entry_y = 1;
                            if (y_check >= y1[k] && y_check <= y2[k])
                            {
                                entry_x = x1[k];
                                entry_y = y_check;
                            }

                            else
                            {
                                //sprawdzanie czy promien wchodzi od dolu
                                double x_check = (y1[k] - b) / a;
                                if (x_check >= x1[k] && x_check <= x2[k])
                                {
                                    entry_x = x_check;
                                    entry_y = y1[k];
                                }
                            }

                            if (entry_x != 1 || entry_y != 1)
                            {
                                //sprawdzanie czy promien wychodzi od prawej sciany
                                y_check = a * x2[k] + b;
                                double exit_x = 0;
                                double exit_y = 0;
                                if (y_check >= y1[k] && y_check <= y2[k])
                                {
                                    exit_x = x2[k];
                                    exit_y = y_check;
                                }

                                else
                                {
                                    //sprawdzanie czy promien wychodzi od gory
                                    double x_check = (y2[k] - b) / a;
                                    if (x_check >= x1[k] && x_check <= x2[k])
                                    {
                                        exit_x = x_check;
                                        exit_y = x1[k];
                                    }
                                }

                                double distance = Math.Sqrt(Math.Pow((exit_x - entry_x), 2) + Math.Pow((exit_y - entry_y), 2));
                                Console.WriteLine("wejscie: " + i + " wyjscie: " + j + " dlugosc promienia: " + distance + " strata energii: " + materials[k] * (distance));

                            }

                        }
                        }
                    }
                }
            }
        }
    }

