using tomograf.metaheurystyki;
using tomograf.shapes;

namespace tomograf
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            OneRectTest();
            //threeRectTest();
        }

        

        private static void OneRectTest()
        {
            IShape[] shapes = { new Rectangle(x1: -0.8, y1: -0.6, x2: 0.6, y2: 0.4, material: 1) };
            int nLasers = 5;
            double[][] results = Tomograph.Run(shapes, nLasers);

            foreach (double[] xx in results)
            {
                foreach (double yy in xx)
                {
                    Console.Write(yy + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();


            double[] lowerBoundaries = { -1, -1, -1, -1, 0.5 };
            double[] higherBoundaries = { 1, 1, 1, 1, 2 };
            var funkcja = new TestFunction(nLasers, results);

            var tuna = new TSO(100, 50, 5, funkcja.DeployRect,higherBoundaries, lowerBoundaries);
            var gtoa = new GTOA(funkcja.DeployRect, lowerBoundaries, higherBoundaries, 5, 20, 50);
            gtoa.Solve();
            //Console.WriteLine(tuna.Solve());
        }

        private static void ThreeRectTest()
        {
            IShape[] shapes = {
                new Rectangle(x1: -0.6, y1: 0.7, x2: 0.6, y2: 0.9, material: 1.7),
                new Rectangle(x1: -0.6, y1: -0.4, x2: -0.1, y2: 0.2, material: 0.8),
                new Rectangle(x1: 0.1, y1: -0.4, x2: 0.6, y2: 0.2, material: 1.4),
            };
            int nLasers = 5;
            double[][] results = Tomograph.Run(shapes, nLasers);

            foreach (double[] xx in results)
            {
                foreach (double yy in xx)
                {
                    Console.Write(yy + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();


            double[] lowerBoundaries = { -1, -1, -1, -1, 0.5, -1, -1, -1, -1, 0.5, -1, -1, -1, -1, 0.5 };
            double[] higherBoundaries = { 1, 1, 1, 1, 2, 1, 1, 1, 1, 2, 1, 1, 1, 1, 2 };
            var funkcja = new TestFunction(nLasers, results);

            var tuna = new TSO(100, 50, 15, funkcja.DeployRect, higherBoundaries, lowerBoundaries);
            var gtoa = new GTOA(funkcja.DeployRect, lowerBoundaries, higherBoundaries, 15, 20, 50);
            var abc = new ABCAlgorithm(6, 6, funkcja.DeployRect, lowerBoundaries, higherBoundaries);
            abc.Solve();
            Console.WriteLine("done");
        }

        private static void OneCircleTest()
        {
            IShape[] shapes = { new Circle(x: 0, y: 0, radius: 1.2, material: 1.2) };
            int nLasers = 5;
            double[][] results = Tomograph.Run(shapes, nLasers);

            foreach (double[] xx in results)
            {
                foreach (double yy in xx)
                {
                    Console.Write(yy + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();


            double[] lowerBoundaries = { -1, -1, -1, 0.5 };
            double[] higherBoundaries = { 1, 1, 1, 2 };
            var funkcja = new TestFunction(5, results);

            var tuna = new TSO(100, 50, 4, funkcja.DeployCircle, higherBoundaries, lowerBoundaries);
            var gtoa = new GTOA(funkcja.DeployCircle, lowerBoundaries, higherBoundaries, 4, 20, 50);
            gtoa.Solve();
            //Console.WriteLine(tuna.Solve());
        }

        private static void threeCircleTest()
        {
            IShape[] shapes = {
                new Circle(x: 0, y: -0.6, radius: 0.7, material: 1.5),
                new Circle(x: 0, y: -0.6, radius: 0.7, material: 0.6),
                new Circle(x: 0.25, y: 0.3, radius: 0.1, material: 0.95),
            };
            int nLasers = 5;
            double[][] results = Tomograph.Run(shapes, nLasers);

            foreach (double[] xx in results)
            {
                foreach (double yy in xx)
                {
                    Console.Write(yy + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();


            double[] lower_boundaries = { -1, -1, 0, 0.5, -1, -1, 0, 0.5, -1, -1, 0, 0.5 };
            double[] higher_boundaries = { 1, 1, 1, 2, 1, 1, 1, 2, 1, 1, 1, 2 };
            TestFunction funkcja = new TestFunction(5, results);

            var tuna = new TSO(100, 50, 4, funkcja.DeployCircle, higher_boundaries, lower_boundaries);
            var gtoa = new GTOA(funkcja.DeployCircle, lower_boundaries, higher_boundaries, 12, 20, 50);
            gtoa.Solve();
        }
    }
}