using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using tomograf.deterministyczne;
using tomograf.metaheurystyki;
using tomograf.shapes;
using static System.Net.Mime.MediaTypeNames;

namespace tomograf
{
    static class Tests
    {
        public static double[][] referencePoly(int nLasers)
        {
            Point[] vertices =
            {
                new() {x = -0.6, y= 0.7},
                new() {x = 0, y = 0.7},
                new() {x = 0.2, y = 0.9},
                new() {x = -0.1, y = 0.5},
                new() {x = 0.3, y = 0},
                new() {x = -0.2, y = -0.1},
                new() {x = 0, y = -0.4},
                new() {x = -0.4, y = -0.2},
                new() {x = -0.7, y = -0.7},
            };
            var refPoly = new Polygon(vertices, 1);
            List<IShape> shapes = new List<IShape>();
            shapes.Add(refPoly);
            return Tomograph.Run(shapes, nLasers);
        }

        public static double[][] referencePoly2(int nLasers) 
        {
            Point[] vertices =
            {
                new() {x=-0.8, y=0.5},
                new() {x=0.5, y=0.6},
                new() {x=0.6, y=0.5},
                new() {x=0.6, y=0.4},
                new() {x=0.5, y=0.3},
                new() {x=0.1, y=-0.4},
                new() {x =-0.4, y=-0.3},
                new() {x=-0.8, y=0}
            };
            var refPoly = new Polygon(vertices, 1);
            List<IShape> shapes = new List<IShape>();
            shapes.Add(refPoly);
            return Tomograph.Run(shapes, nLasers);
        }

        public static double[][] referenceThreeRect(int nLasers)
        {
            
            IShape[] shapes = {
                new Rectangle(x1: -0.6, y1: 0.7, x2: 0.6, y2: 0.9, material: 1.7),
                new Rectangle(x1: -0.6, y1: -0.4, x2: -0.1, y2: 0.2, material: 0.8),
                new Rectangle(x1: 0.1, y1: -0.4, x2: 0.6, y2: 0.2, material: 1.4),
            };
            return Tomograph.Run(shapes, nLasers);

        }

        public static double[][] referenceVert3R(int nLasers)
        {
            IShape[] shapes = {
                new Rectangle(x1: -0.6, y1: -0.6, x2: -0.2, y2: 0.25, material: 1.3),
                new Rectangle(x1: 0, y1: -0.95, x2: 0.2, y2: 0.15, material: 0.8),
                new Rectangle(x1: 0.4, y1: 0.4, x2: 0.7, y2: 0.8, material: 1.4),
            };
            return Tomograph.Run(shapes, nLasers);
        }

        public static double[][] referenceOneRect(int nLasers) 
        {
            IShape[] shapes = { new Rectangle(x1: -0.8, y1: -0.6, x2: 0.6, y2: 0.4, material: 1) };
            return Tomograph.Run(shapes, nLasers);
        }

        public static double[][] referenceOneCircle(int nLasers) 
        {
            IShape[] shapes = { new Circle(x: 0, y: 0, radius: 1.2, material: 1.2) };
            return Tomograph.Run(shapes, nLasers);
        }

        public static double[][] referenceThreeCircle(int nLasers)
        {
            IShape[] shapes = {
                new Circle(x: 0, y: -0.6, radius: 0.7, material: 1.5),
                new Circle(x: 0, y: -0.6, radius: 0.7, material: 0.6),
                new Circle(x: 0.25, y: 0.3, radius: 0.1, material: 0.95),
            };
            return Tomograph.Run(shapes,nLasers);
        }

        public static void threeRectRecreation(int nLasers) 
        {
            //double[] initialValues = {-0.5966739044510116, 0.19676699642593085, 0.6441532843563083, - 0.3890690721586308, 0.8809504807118331,
            //    - 0.31380620337418924, 0.6254295883092844, - 0.11678306315032078, 0.9332192566052868, 0.8960693524223609, - 0.6190457930208307,
            //    0.6988870934201498, 0.661783290093477, 0.90655520763752, 1.4313323856696827};

            //            double[] initialValues = {
            //-0.5971109687365675, 0.8999723674167804, 0.599855649543582, 0.70014220387507, 1.7007496147120618,
            //0.39131006659227685, -0.3869661903855316, 0.48476108220669994, -0.26462713833701923, 0.5454137880681823,
            //-0.5764153393779837, 0.20089522309917093, 0.7402241760229957, -0.3995628518062178, 0.8344782618413868};

            double[] initialValues = { 0.394956219525378, 0.39932327012785435, 0.7080254724251563, 0.7984965569890956, 1.3417956378428502,
-0.17109617502875307, -0.6010789064762135, -0.6377789436803729, 0.24976340713632234, 1.1117431734056, -0.054777392793878305,
0.14678073881961906, 0.2198470602103694, -0.9950345492404679, 0.5855602828929947};

            //TestFunction tf = new TestFunction(nLasers, referenceThreeRect(nLasers));
            TestFunction tf = new TestFunction(nLasers, referenceVert3R(nLasers));
            double[] lowerBoundaries = { -1, -1, -1, -1, 0.5, -1, -1, -1, -1, 0.5, -1, -1, -1, -1, 0.5 };
            double[] higherBoundaries = { 1, 1, 1, 1, 2, 1, 1, 1, 1, 2, 1, 1, 1, 1, 2 };

            double[][] values = new double[initialValues.Length + 1][];
            for (int i = 0; i < initialValues.Length + 1; i++)
            {
                values[i] = new double[initialValues.Length];
            }


            for (int i = 0; i < initialValues.Length; i++)
            {
                values[0][i] = initialValues[i];
            }

            for (int i = 0; i < initialValues.Length; i++)
            {
                for (int j = 0; j < initialValues.Length; j++)
                {
                    if (i == j)
                    {
                        values[i + 1][j] = initialValues[j] - 0.15;
                        if (j % 5 == 0)
                        {
                            values[i + 1][j] += 0.2;
                        }
                    }
                    else
                    {
                        values[i + 1][j] = initialValues[j];
                    }
                }
            }

            var nm = new Nelder_Mead(0.00001f, tf.DeployRect, values);
            nm.run();
        }

        public static void polyRecreation(int nLasers)
        {
            //TestFunction tf = new TestFunction(nLasers, referencePoly(nLasers));
            TestFunction tf = new TestFunction(nLasers, referencePoly2(nLasers));

            //double[] lowerBoundaries = { -1, -1, 0, 0.5};
            //double[] lowerBoundaries = { -1, -1, 0, 0.5, -1, -1, 0, 0.5 };
            //double[] lowerBoundaries = { -1, -1, 0, 0.5, -1, -1, 0, 0.5, -1, -1, 0, 0.5 };
            //double[] lowerBoundaries = { -1, -1, 0, 0.5, -1, -1, 0, 0.5, -1, -1, 0, 0.5, -1, -1, 0, 0.5 };
            double[] lowerBoundaries = { -1, -1, 0, 0.5, -1, -1, 0, 0.5, -1, -1, 0, 0.5, -1, -1, 0, 0.5, -1, -1, 0, 0.5 };

            //double[] higherBoundaries = { 1, 1, 1, 2};
            //double[] higherBoundaries = { 1, 1, 1, 2, 1, 1, 1, 2 };
            //double[] higherBoundaries = { 1, 1, 1, 2, 1, 1, 1, 2, 1, 1, 1, 2 };
            //double[] higherBoundaries = { 1, 1, 1, 2, 1, 1, 1, 2, 1, 1, 1, 2, 1, 1, 1, 2 };
            double[] higherBoundaries = { 1, 1, 1, 2, 1, 1, 1, 2, 1, 1, 1, 2, 1, 1, 1, 2, 1, 1, 1, 2 };


            var algorithm = new ABCAlgorithm(10, 10, tf.DeployCircle, lowerBoundaries, higherBoundaries);
            //var algorithm = new JellyfishSearchOptimizer(tf.DeployCircle, 4800, 30, lowerBoundaries.Length, higherBoundaries, lowerBoundaries);
            var watch = System.Diagnostics.Stopwatch.StartNew();
            algorithm.Solve();
            Console.WriteLine();
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("czas: " + elapsedMs.ToString());

            Console.WriteLine($"FBest: {algorithm.FBest}");
            Console.Write("XBest: ");

            foreach (var x in algorithm.XBest)
            {
                Console.Write(x + " ");
            }
            Console.WriteLine();

            double[][] values = new double[algorithm.XBest.Length + 1][];
            for (int i = 0; i < algorithm.XBest.Length + 1; i++)
            {
                values[i] = new double[algorithm.XBest.Length];
            }


            for (int i = 0; i < algorithm.XBest.Length; i++)
            {
                values[0][i] = algorithm.XBest[i];
            }

            for (int i = 0; i < algorithm.XBest.Length; i++)
            {
                for (int j = 0; j < algorithm.XBest.Length; j++)
                {
                    if (i == j)
                    {
                        values[i + 1][j] = algorithm.XBest[j] + 0.1;
                        if (j % 5 == 0)
                        {
                            values[i + 1][j] += 0.1;
                        }
                    }
                    else
                    {
                        values[i + 1][j] = algorithm.XBest[j];
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("NELDER MEAD");
            var watch2 = System.Diagnostics.Stopwatch.StartNew();
            Nelder_Mead nm = new Nelder_Mead(0.0000001f, tf.DeployCircle, values);
            nm.run();
            watch2.Stop();
            var elapsedMs2 = watch2.ElapsedMilliseconds;
            Console.WriteLine("Czas dla Nelder-Meada " +elapsedMs2);
            Console.WriteLine("Laczny czas obliczen " + (elapsedMs + elapsedMs2));

            Console.WriteLine();
            Console.WriteLine("HOOK JAVES");
            var watch3 = System.Diagnostics.Stopwatch.StartNew();
            var hj = new HookeJeeves(algorithm.XBest, tf.DeployCircle, 0.2, 0.5, 0.0000001);
            hj.Run();
            watch3.Stop();
            Console.WriteLine("ilosc iteracji: " + hj.iterations);
            Console.WriteLine("Ilosc wywolan funkcji celu " + hj.nCalls);
            Console.WriteLine($"FBest: {hj.FBest}");
            Console.Write("XBest: ");

            foreach (var x in hj.XBest)
            {
                Console.Write(x + " ");
            }
            var elapsedMs3=watch3.ElapsedMilliseconds;
            Console.WriteLine("Czas dla Hooka-Javesa " + elapsedMs3);
            Console.WriteLine("Laczny czas obliczen " + (elapsedMs + elapsedMs3));
        }


        public static void deterministicThreeRectTest()
        {
            double[] initialValues = {-0.3910361767075816, 0.6631676426247867, 0.4973300497013236, 0.8632698887519723, 1.6990193239774767,
 -0.606320580734362, -0.4720869725072207, -0.6447315165821815, 0.2027881948228539, 1.2959275104021961, 0.902156050267698, -0.4287120632640748
-0.5216839745464533, 0.21428203016708175, 0.7372872055556781};

            double[][] values = new double[initialValues.Length + 1][];
            for (int i = 0; i < initialValues.Length + 1; i++)
            {
                values[i] = new double[initialValues.Length];
            }


            for (int i = 0; i < initialValues.Length; i++)
            {
                values[0][i] = initialValues[i];
            }

            for (int i = 0; i < initialValues.Length; i++)
            {
                for (int j = 0; j < initialValues.Length; j++)
                {
                    if (i == j)
                    {
                        values[i + 1][j] = initialValues[j] + 0.14;
                        if (j % 5 == 0)
                        {
                            values[i + 1][j] += 0.2;
                        }
                    }
                    else
                    {
                        values[i + 1][j] = initialValues[j];
                    }
                }
            }

            IShape[] shapes = {
                new Rectangle(x1: -0.6, y1: 0.7, x2: 0.6, y2: 0.9, material: 1.7),
                new Rectangle(x1: -0.6, y1: -0.4, x2: -0.1, y2: 0.2, material: 0.8),
                new Rectangle(x1: 0.1, y1: -0.4, x2: 0.6, y2: 0.2, material: 1.4),
            };

            double[][] results = Tomograph.Run(shapes, 5);

            double[] lower_boundaries = { -1, -1, -1, -1, 0.5, -1, -1, -1, -1, 0.5, -1, -1, -1, -1, 0.5 };
            double[] higher_boundaries = { 1, 1, 1, 1, 2, 1, 1, 1, 1, 2, 1, 1, 1, 1, 2 };
            TestFunction funkcja = new TestFunction(5, results);

            Nelder_Mead nm = new Nelder_Mead(0.0000001f, funkcja.DeployRect, values);
            nm.run();


        }
    }
}
