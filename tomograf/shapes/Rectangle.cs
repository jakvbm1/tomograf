using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//na ten moment program nie będzie działał dla jednego lasera (dojdzie do dzielenia przez 0), na dniach dorobię obsługe jednego lasera


namespace tomograf.shapes
{
    class Rectangle : IShape
    {
        private double x1;
        private double y1;
        private double x2;
        private double y2;
        private double material;

        public Rectangle(double x1, double y1, double x2, double y2, double material)
        {
            // dodalem ta linijke zeby sie upewnic zawsze ze (x1, y1) to lewy dolny, laser_a (x2, y2) to prawy gorny
            this.x1 = Math.Min(x1, x2);
            this.y1 = Math.Min(y1, y2);
            this.x2 = Math.Max(x1, x2);
            this.y2 = Math.Max(y1, y2);
            this.material = material;
        }

        public double CalcEnergyLoss(double laser_a, double laser_b)
        {
            // Sprawdzanie czy laser idzie na wprost
            if (laser_a == 0 && laser_b > y1 && laser_b < y2)
            {
                return material * (x2 - x1);
            }

            if (laser_a < 0)
            {
                // sprawdzanie czy promien "wchodzi" od lewej sciany
                double y_check = laser_a * x1 + laser_b;
                double entry_x = 1;
                double entry_y = 1;
                if (y_check >= y1 && y_check <= y2)
                {
                    entry_x = x1;
                    entry_y = y_check;
                }
                else
                {
                    // sprawdzanie czy promien wchodzi od gory
                    double x_check = (y2 - laser_b) / laser_a;
                    if (x_check >= x1 && x_check <= x2)
                    {
                        entry_x = x_check;
                        entry_y = y2;
                    }
                }

                if (entry_x != 1 || entry_y != 1)
                {
                    //sprawdzanie czy promien wychodzi od prawej sciany
                    y_check = laser_a * x2 + laser_b;
                    double exit_x = 0;
                    double exit_y = 0;
                    if (y_check >= y1 && y_check <= y2)
                    {
                        exit_x = x2;
                        exit_y = y_check;
                    }
                    else
                    {
                        //sprawdzanie czy promien wychodzi od dolu
                        double x_check = (y1 - laser_b) / laser_a;
                        if (x_check >= x1 && x_check <= x2)
                        {
                            exit_x = x_check;
                            exit_y = y1;
                        }
                    }

                    double distance = Math.Sqrt(Math.Pow(exit_x - entry_x, 2) + Math.Pow(exit_y - entry_y, 2));
                    return material * distance;
                }
            }

            if (laser_a > 0)
            {
                // sprawdzanie czy promien "wchodzi" od lewej sciany
                double y_check = laser_a * x1 + laser_b;
                double entry_x = 1;
                double entry_y = 1;
                if (y_check >= y1 && y_check <= y2)
                {
                    entry_x = x1;
                    entry_y = y_check;
                }
                else
                {
                    // sprawdzanie czy promien wchodzi od dolu
                    double x_check = (y1 - laser_b) / laser_a;
                    if (x_check >= x1 && x_check <= x2)
                    {
                        entry_x = x_check;
                        entry_y = y1;
                    }
                }

                if (entry_x != 1 || entry_y != 1)
                {
                    //sprawdzanie czy promien wychodzi od prawej sciany
                    y_check = laser_a * x2 + laser_b;
                    double exit_x = 0;
                    double exit_y = 0;
                    if (y_check >= y1 && y_check <= y2)
                    {
                        exit_x = x2;
                        exit_y = y_check;
                    }
                    else
                    {
                        // sprawdzanie czy promien wychodzi od gory
                        double x_check = (y2 - laser_b) / laser_a;
                        if (x_check >= x1 && x_check <= x2)
                        {
                            exit_x = x_check;
                            exit_y = y2;
                        }
                    }
                    double distance = Math.Sqrt(Math.Pow(exit_x - entry_x, 2) + Math.Pow(exit_y - entry_y, 2));
                    return material * distance;
                }
            }

            return 0;
        }

        public string ShapeDescription()
        {
            return $"Rectangle material: {material}, x1: {x1}, y1: {y1}, x2: {x2}, y2: {y2}";
        }
    }
}

