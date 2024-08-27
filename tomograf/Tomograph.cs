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
        public static double[][] Run(IList<IShape> shapes, int nLasers)
        {
            double distanceBetweenLasers = 2.0 / (nLasers - 1);
            double[][] energyLoss = new double[nLasers][];

            for (int i = 0; i < nLasers; i++)
            {
                double laserEndY = 1 - i * distanceBetweenLasers;
                energyLoss[i] = new double[nLasers];
                for (int j = 0; j < nLasers; j++)
                {
                    double laserStartY = 1 - j * distanceBetweenLasers;
                    double laserA = CalcA(-1, laserStartY, 1, laserEndY);
                    double laserB = CalcB(laserA, -1, laserStartY);
                    energyLoss[i][j] = ShapesEnergyLoss(shapes, laserA, laserB);
                }
            }

            return energyLoss;
        }

        private static double CalcA(double x1, double y1, double x2, double y2)
        {
            return (y2 - y1) / (x2 - x1);
        }

        private static double CalcB(double a, double x, double y)
        {
            return y - a * x;
        }

        private static double ShapesEnergyLoss(IList<IShape> shapes, double laserA, double laserB)
        {
            double energyLoss = 0;

            foreach (var shape in shapes)
            {
                energyLoss += shape.CalcEnergyLoss(laserA, laserB);
            }

            return energyLoss;
        }

        public static void SaveToTxt(double[][] energyLoss, IList<IShape> shapes, int nLasers)
        {
            string filename = "results.txt";
            using (StreamWriter writer = new(filename))
            {
                writer.WriteLine("Lasers number: " + nLasers);
                writer.WriteLine("Shapes number: " + shapes.Count);

                foreach (var shape in shapes)
                {
                    writer.WriteLine(shape.ShapeDescription());
                }

                writer.WriteLine("Energy losses:");
                foreach (double[] line in energyLoss)
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
