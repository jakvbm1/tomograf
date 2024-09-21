using tomograf.deterministyczne;
using tomograf.metaheurystyki;
using tomograf.shapes;

namespace tomograf
{
    internal class Program
    {
        

        private static void Main(string[] args)
        {
            // One rect test
            //double[] lowerBoundaries = { -1, -1, -1, -1, 0.5 };
            //double[] higherBoundaries = { 1, 1, 1, 1, 2 };


            double[] lowerBoundaries = { -1, -1, -1, -1, 0.5, -1, -1, -1, -1, 0.5, -1, -1, -1, -1, 0.5 };
            double[] higherBoundaries = { 1, 1, 1, 1, 2, 1, 1, 1, 1, 2, 1, 1, 1, 1, 2 };

            // One circle test
            //double[] lowerBoundaries = { -1, -1, -1, 0.5 };
            //double[] higherBoundaries = { 1, 1, 1, 2 };

            // Three circles test
            //double[] lowerBoundaries = { -1, -1, 0, 0.5, -1, -1, 0, 0.5, -1, -1, 0, 0.5 };
            //double[] higherBoundaries = { 1, 1, 1, 2, 1, 1, 1, 2, 1, 1, 1, 2 };

            // Polygon test
            //Point[] vertices = {
            //    new() { x = -0.5, y = 0.5 },
            //    new() { x = 0, y = 0.1 },
            //    new() { x = -0.5, y = -0.5 },
            //    new() { x = 1, y = 0 },
            //};
            //IShape[] shapes = { new Polygon(vertices: vertices, material: 1) };
            //double[] lowerBoundaries = { -1, -1, -1, -1, -1, -1, -1, -1 };
            //double[] higherBoundaries = { 1, 1, 1, 1, 1, 1, 1, 1 };

            int nLasers = 50;
            int iterations = 50;
            int population = 50;
            int dimensions = lowerBoundaries.Length;
            //double[][] results = Tomograph.Run(shapes, nLasers);
            //Func<double[], double> testFunction = new TestFunction(nLasers, results).DeployPolygon(vertices.Length);

            //Console.WriteLine("Energy losses from tomograph:");
            //foreach (double[] xx in results)
            //{
            //    foreach (double yy in xx)
            //    {
            //        Console.Write(yy + " ");
            //    }
            //    Console.WriteLine();
            //}
            //Console.WriteLine();
            var testFunction = new TestFunction(nLasers, Tests.referenceVert3R(nLasers));
            //var algorithm = new TSO(iterations, population, dimensions, testFunction, higherBoundaries, lowerBoundaries);
            //var algorithm = new GTOA(testFunction, lowerBoundaries, higherBoundaries, dimensions, population, iterations);
            var algorithm = new ABCAlgorithm(population, iterations, testFunction.DeployRect, lowerBoundaries, higherBoundaries);
            //var algorithm = new JellyfishSearchOptimizer(testFunction, population, iterations, dimensions, higherBoundaries, lowerBoundaries);

            //algorithm.Solve();

            Console.WriteLine("Results of metaheuristic algorithm:");
            Console.WriteLine($"FBest: {algorithm.FBest}");
            Console.Write("XBest: ");

            foreach (var x in algorithm.XBest)
            {
                Console.Write(x + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
            Tests.polyRecreation(70);
            //Tests.threeRectRecreation(50);

            //var deterministicAlgorithm = new HookeJeeves(algorithm.XBest, testFunction, initialStepSize: 0.2, stepReductionFactor: 0.5, precision: 0.0001);

            //deterministicAlgorithm.Run();

            //Console.WriteLine("Results of deterministic algorithm:");
            //Console.WriteLine($"FBest: {deterministicAlgorithm.FBest}");
            //Console.Write("XBest: ");

            //foreach (var x in deterministicAlgorithm.XBest)
            //{
            //    Console.Write(x + " ");
            //}
            //Console.WriteLine();

            //deterministicThreeRectTest();
        }
    }
}