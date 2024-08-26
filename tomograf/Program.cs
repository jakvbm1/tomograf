// See https://aka.ms/new-console-template for more information
using tomograf.metaheurystyki;
using tomograf.tomografy;

namespace tomograf
{
    internal class Program
    {
        private static void oneRectTest()
        {
            double[] x1 = { -0.8 };
            double[] y1 = { -0.6 };
            double[] x2 = { 0.6 };
            double[] y2 = { 0.4 };
            double[] material = { 1 };
            var referenceRect = new Rect_Tomograph(5, x1, y1, x2, y2, material);

            double[][] results = referenceRect.Run();

            foreach (double[] xx in results)
            {
                foreach (double yy in xx)
                {
                    Console.Write(yy + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();


            double[] lower_boundaries = { -1, -1, -1, -1, 0.5 };
            double[] higher_boundaries = { 1, 1, 1, 1, 2 };
            TestFunction funkcja = new TestFunction(5, results);

            TSO tuna = new TSO(100, 50, 5, funkcja.deploy_rect,higher_boundaries, lower_boundaries);
            GTOA gtoa = new GTOA(funkcja.deploy_rect, lower_boundaries, higher_boundaries, 5, 20, 50);
            gtoa.Solve();
            //Console.WriteLine(tuna.Solve());
        }

        private static void threeRectTest()
        {
            double[] x1 = { -0.6, -0.6 , 0.1};
            double[] y1 = { 0.7, -0.4, -0.4 };
            double[] x2 = { 0.6, -0.1, 0.6 };
            double[] y2 = { 0.9, 0.2, 0.2};
            double[] material = { 1.7, 0.8, 1.4 };
            var referenceRect = new Rect_Tomograph(5, x1, y1, x2, y2, material);

            double[][] results = referenceRect.Run();

            foreach (double[] xx in results)
            {
                foreach (double yy in xx)
                {
                    Console.Write(yy + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();


            double[] lower_boundaries = { -1, -1, -1, -1, 0.5, -1, -1, -1, -1, 0.5, -1, -1, -1, -1, 0.5 };
            double[] higher_boundaries = { 1, 1, 1, 1, 2, 1, 1, 1, 1, 2, 1, 1, 1, 1, 2 };
            TestFunction funkcja = new TestFunction(5, results);

            // TSO tuna = new TSO(100, 50, 15, funkcja.deploy_rect, higher_boundaries, lower_boundaries);
            //Console.WriteLine(tuna.Solve());
            GTOA gtoa = new GTOA(funkcja.deploy_rect, lower_boundaries, higher_boundaries, 15, 20, 50);
            ABCAlgorithm abc = new ABCAlgorithm(6, 6, funkcja.deploy_rect, lower_boundaries, higher_boundaries);
            abc.Solve();
            Console.WriteLine("done");
            //gtoa.Solve();
        }

        private static void oneCircleTest()
        {
            double[] x = { 0 };
            double[] y = { 0};
            double[] r = { 0.6 };

            double[] material = { 1.2 };
            var referenceCircle = new Circle_tomograph(x, y, r, 5 , material);

            double[][] results = referenceCircle.Run();

            foreach (double[] xx in results)
            {
                foreach (double yy in xx)
                {
                    Console.Write(yy + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();


            double[] lower_boundaries = { -1, -1, -1, 0.5 };
            double[] higher_boundaries = { 1, 1, 1, 2 };
            TestFunction funkcja = new TestFunction(5, results);

            TSO tuna = new TSO(100, 50, 4, funkcja.deploy_circle, higher_boundaries, lower_boundaries);
            GTOA gtoa = new GTOA(funkcja.deploy_circle, lower_boundaries, higher_boundaries, 4, 20, 50);
            gtoa.Solve();
            //Console.WriteLine(tuna.Solve());
        }

        private static void threeCircleTest()
        {
            double[] x = { 0, -0.6, 0.7 };
            double[] y = { 0, -0.6, 0.7 };
            double[] r = { 0.25, 0.3, 0.1 };

            double[] material = { 1.5, 0.6, 0.95 };
            var referenceCircle = new Circle_tomograph(x, y, r, 5, material);

            double[][] results = referenceCircle.Run();

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

            TSO tuna = new TSO(100, 50, 4, funkcja.deploy_circle, higher_boundaries, lower_boundaries);
            GTOA gtoa = new GTOA(funkcja.deploy_circle, lower_boundaries, higher_boundaries, 12, 20, 50);
            gtoa.Solve();
            //Console.WriteLine(tuna.Solve());
        }


        private static void Main(string[] args)
        {
            //double[] x1 = { -0.2, -1, 0.05 };
            //double[] y1 = { -0.5, -0.7, 0.6 };
            //double[] x2 = { 0.1, -0.2, 0.85 };
            //double[] y2 = { 0.4, -0.6, 0.9};
            //double[] material = {3, 2, 1};
            //Tomograf tomograf = new Tomograf(5, 3, x1, y1, x2, y2, material);


            //double[] x1 = { -0.4 };
            //double[] y1 = { -0.6 };
            //double[] x2 = { 0.6 };
            //double[] y2 = { 0.4 };
            //double[] material = { 1 };
            //Rect_Tomograph tomograf = new Rect_Tomograph(5, x1, y1, x2, y2, material);
            //double[][] wyniki = tomograf.Run();
            //foreach (double[] xx in wyniki)
            //{
            //    foreach (double yy in xx)
            //    {
            //        Console.Write(yy + " ");
            //    }
            //    Console.WriteLine();
            //}
            //Console.WriteLine();


            //double[] x = { 0.1 };
            //double[] y = { -0.1 };
            //double[] r = { 0.5 };
            //Circle_tomograph ct = new Circle_tomograph(x, y, r, 5, material);
            //double[][] circle_res = ct.Run();
            //foreach(var i  in circle_res)
            //{
            //    foreach(var j in i)
            //    {
            //        Console.Write(j + " ");
            //    }
            //    Console.WriteLine();
            //}
            //Console.WriteLine();


            //Point[] rectangle = {
            //    new() { x = -0.4, y = -0.6 },
            //    new() { x = 0.6, y = -0.6 },
            //    new() { x = 0.6, y = 0.4 },
            //    new() { x = -0.4, y = 0.4 },
            //};
            //Point[][] polygons = { rectangle };
            //Polygon_Tomograph pt = new (5, polygons, material);
            //double[][] polygon_res = pt.Run();
            //foreach (var i in polygon_res)
            //{
            //    foreach (var j in i)
            //    {
            //        Console.Write(j + " ");
            //    }
            //    Console.WriteLine();
            //}

            //oneRectTest();
            threeRectTest();
            //oneCircleTest();
            //threeCircleTest();
        }
    }
}