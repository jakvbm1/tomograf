using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tomograf.shapes;

namespace tomograf
{
    internal class Tomograph
    {
        public static double[][] Run(IList<IShape> shapes, int n_lasers)
        {
            double distance_between_lasers = 2.0 / (n_lasers - 1);
            double[][] energy_loss = new double[n_lasers][];
            for (int i = 0; i < n_lasers; i++)
            {
                double laser_end_y = 1 - i * distance_between_lasers;
                energy_loss[i] = new double[n_lasers];
                for (int j = 0; j < n_lasers; j++)
                {
                    double laser_start_y = 1 - j * distance_between_lasers;
                    double laser_a = calc_a(-1, laser_start_y, 1, laser_end_y);
                    double laser_b = calc_b(laser_a, -1, laser_start_y);
                    energy_loss[i][j] = shapes_energy_loss(shapes, laser_a, laser_b);
                }
            }

            save_to_txt(energy_loss, shapes, n_lasers);
            return energy_loss;
        }

        private static double calc_a(double x1, double y1, double x2, double y2)
        {
            return (y2 - y1) / (x2 - x1);
        }

        private static double calc_b(double a, double x, double y)
        {
            return y - a * x;
        }

        private static double shapes_energy_loss(IList<IShape> shapes, double laser_a, double laser_b)
        {
            double energy_loss = 0;

            foreach (var shape in shapes)
            {
                energy_loss += shape.CalcEnergyLoss(laser_a, laser_b);
            }

            return energy_loss;
        }

        private static void save_to_txt(double[][] energy_loss, IList<IShape> shapes, int n_lasers)
        {
            string filename = "results.txt";
            using (StreamWriter writer = new(filename))
            {
                writer.WriteLine("Lasers number: " + n_lasers);
                writer.WriteLine("Shapes number: " + shapes.Count);

                foreach (var shape in shapes)
                {
                    writer.WriteLine(shape.ShapeDescription());
                }

                writer.WriteLine("Energy losses:");
                foreach (double[] line in energy_loss)
                {
                    foreach (double y in line)
                    {
                        writer.Write(y + " ");
                    }
                    writer.WriteLine();
                }
                writer.Close();
            }
            Console.WriteLine("Saved results to: " + filename);
        }
    }
}
