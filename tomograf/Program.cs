using tomograf.metaheurystyki;
using tomograf.shapes;
using tomograf.tomografy;

namespace tomograf
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            double[] x = { 0.1 };
            double[] y = { -0.1 };
            double[] r = { 0.5 };
            double[] material = { 1 };
            Circle_tomograph ct = new(x, y, r, 5, material);
            double[][] circle_res = ct.Run();
            foreach(var i  in circle_res)
            {
               foreach(var j in i)
               {
                   Console.Write(j + " ");
               }
               Console.WriteLine();
            }
            Console.WriteLine();


            Circle circle = new(x[0], y[0], r[0], material[0]);
            IShape[] circleShapes = { circle };
            double[][] circleShapesResults = Tomograph.Run(circleShapes, 5);
            foreach (var i  in circleShapesResults)
            {
               foreach(var j in i)
               {
                   Console.Write(j + " ");
               }
               Console.WriteLine();
            }
            Console.WriteLine();


            double[] x1 = { -0.4 };
            double[] y1 = { -0.6 };
            double[] x2 = { 0.6 };
            double[] y2 = { 0.4 };
            Rect_Tomograph tomograf = new(5, x1, y1, x2, y2, material);
            double[][] wyniki = tomograf.Run();
            foreach (double[] xx in wyniki)
            {
                foreach (double yy in xx)
                {
                    Console.Write(yy + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();


            Rectangle rect = new(x1[0], y1[0], x2[0], y2[0], material[0]);
            IShape[] rectShapes = { rect };
            double[][] rectShapesResults = Tomograph.Run(rectShapes, 5);
            foreach (double[] xx in rectShapesResults)
            {
                foreach (double yy in xx)
                {
                    Console.Write(yy + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();


            Point[] rectangle = {
                new() { x = -0.4, y = -0.6 },
                new() { x = 0.6, y = -0.6 },
                new() { x = 0.6, y = 0.4 },
                new() { x = -0.4, y = 0.4 },
            };
            Point[][] polygons = { rectangle };
            Polygon_Tomograph pt = new(5, polygons, material);
            double[][] polygon_res = pt.Run();
            foreach (var i in polygon_res)
            {
                foreach (var j in i)
                {
                    Console.Write(j + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();


            Polygon polyRct = new(rectangle, material[0]);
            IShape[] polyRctShape = { polyRct };
            var polyRctShapeResult = Tomograph.Run(polyRctShape, 5);
            foreach (var i in polyRctShapeResult)
            {
                foreach (var j in i)
                {
                    Console.Write(j + " ");
                }
                Console.WriteLine();
            }

            //oneRectTest();
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
            var gtoa = new GTOA(funkcja.DeployRect, lowerBoundaries, higherBoundaries, 5, 20, 75);
            gtoa.Solve();
            //Console.WriteLine(tuna.Solve());
        }

        private static void ThreeRectTest()
        {
            IShape[] shapes = {
                new Rectangle(x1: -0.6, y1: 0.9, x2: 0.6, y2: 0.7, material: 1.7),
                new Rectangle(x1: -0.6, y1: -0.4, x2: -0.1, y2: 0.2, material: 1.8),
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
            Console.WriteLine(tuna.Solve());
        }
    }
}