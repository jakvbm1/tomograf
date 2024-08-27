using tomograf.metaheurystyki;
using tomograf.shapes;

namespace tomograf
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // One rect test
            //IShape[] shapes = { new Rectangle(x1: -0.8, y1: -0.6, x2: 0.6, y2: 0.4, material: 1) };
            //double[] lowerBoundaries = { -1, -1, -1, -1, 0.5 };
            //double[] higherBoundaries = { 1, 1, 1, 1, 2 };

            // Three rects test
            //IShape[] shapes = {
            //    new Rectangle(x1: -0.6, y1: 0.7, x2: 0.6, y2: 0.9, material: 1.7),
            //    new Rectangle(x1: -0.6, y1: -0.4, x2: -0.1, y2: 0.2, material: 0.8),
            //    new Rectangle(x1: 0.1, y1: -0.4, x2: 0.6, y2: 0.2, material: 1.4),
            //};
            //double[] lowerBoundaries = { -1, -1, -1, -1, 0.5, -1, -1, -1, -1, 0.5, -1, -1, -1, -1, 0.5 };
            //double[] higherBoundaries = { 1, 1, 1, 1, 2, 1, 1, 1, 1, 2, 1, 1, 1, 1, 2 };

            // One circle test
            //IShape[] shapes = { new Circle(x: 0, y: 0, radius: 1.2, material: 1.2) };
            //double[] lowerBoundaries = { -1, -1, -1, 0.5 };
            //double[] higherBoundaries = { 1, 1, 1, 2 };

            // Three circles test
            IShape[] shapes = {
                new Circle(x: 0, y: -0.6, radius: 0.7, material: 1.5),
                new Circle(x: 0, y: -0.6, radius: 0.7, material: 0.6),
                new Circle(x: 0.25, y: 0.3, radius: 0.1, material: 0.95),
            };
            double[] lowerBoundaries = { -1, -1, 0, 0.5, -1, -1, 0, 0.5, -1, -1, 0, 0.5 };
            double[] higherBoundaries = { 1, 1, 1, 2, 1, 1, 1, 2, 1, 1, 1, 2 };

            int nLasers = 5;
            int iterations = 100;
            int population = 50;
            int dimensions = lowerBoundaries.Length;
            double[][] results = Tomograph.Run(shapes, nLasers);
            Func<double[], double> testFunction = new TestFunction(nLasers, results).DeployCircle;

            foreach (double[] xx in results)
            {
                foreach (double yy in xx)
                {
                    Console.Write(yy + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            //var algorithm = new TSO(iterations, population, dimensions, testFunction, higherBoundaries, lowerBoundaries);
            //var algorithm = new GTOA(testFunction, lowerBoundaries, higherBoundaries, dimensions, population, iterations);
            var algorithm = new ABCAlgorithm(population, iterations, testFunction, lowerBoundaries, higherBoundaries);
            //var algorithm = new JellyfishSearchOptimizer(testFunction, population, iterations, dimensions, higherBoundaries, lowerBoundaries);

            algorithm.Solve();

            Console.WriteLine($"FBest: {algorithm.FBest}");
            Console.Write("XBest: ");

            foreach (var x in algorithm.XBest)
            {
                Console.Write(x + " ");
            }

            Console.WriteLine();
        }
    }
}