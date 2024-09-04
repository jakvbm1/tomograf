using tomograf.deterministyczne;
using tomograf.metaheurystyki;
using tomograf.shapes;

namespace tomograf
{
    internal class Program
    {
        private static void deterministicThreeRectTest()
        {
            //            double[] initialValues = {-0.3910361767075816, 0.6631676426247867, 0.4973300497013236, 0.8632698887519723, 1.6990193239774767,
            // -0.606320580734362, -0.4720869725072207, -0.6447315165821815, 0.2027881948228539, 1.2959275104021961, 0.902156050267698, -0.4287120632640748
            //-0.5216839745464533, 0.21428203016708175, 0.7372872055556781};

            //z testu 5 laserow
            //            double[] initialValues = {
            //                0.9335143313662114, -0.8920549736439796, 0.9789922685738834, -0.8989066953838561, 0.9944749777420308, 0.974476649361697,
            //-0.4444453605202384, -0.5638217628085196, 0.21793838065977236, 0.7171158449999561, -0.33357900543650065, 0.5953000038776909,
            //0.41016135324580694, 0.9121346360933908, 1.8239546039808279};


            //z testu 20 laserow
            double[] initialValues = {0.42717909951416977,
-0.4391868353139874,
0.439866916339083,
0.22274503891932024,
0.5157246879869498,
0.7285927676038562,
0.20111529342498694,
-0.556216150739741,
-0.39933260563493067,
0.8487703357173223,
-0.7913023783260724,
0.9571510612543263,
0.8797234958467803,
0.7316545285309463,
0.9821287648665217};
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
                    if(i==j)
                    {
                        values[i + 1][j] = initialValues[j] - 0.1;
                        if( j%5 == 0 )
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

            double[][] results = Tomograph.Run(shapes, 20);

            double[] lower_boundaries = { -1, -1, -1, -1, 0.5, -1, -1, -1, -1, 0.5, -1, -1, -1, -1, 0.5 };
            double[] higher_boundaries = { 1, 1, 1, 1, 2, 1, 1, 1, 1, 2, 1, 1, 1, 1, 2 };
            TestFunction funkcja = new TestFunction(20, results);

            Nelder_Mead nm = new Nelder_Mead(0.0000001f, funkcja.DeployRect, values);
            nm.run();


        }

        private static double[][] referencePoly()
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
            return Tomograph.Run(shapes, 20);
        }

        private static void polyRecreation()
        {
            TestFunction tf = new TestFunction(20, referencePoly());

            double[] lowerBoundaries = { -1, -1, 0, 0.5, -1, -1, 0, 0.5, -1, -1, 0, 0.5 };
            double[] higherBoundaries = { 1, 1, 1, 2, 1, 1, 1, 2, 1, 1, 1, 2 };

            var algorithm = new ABCAlgorithm(20, 20, tf.DeployCircle, lowerBoundaries, higherBoundaries);
            //algorithm.Solve();

            double[] initialValues = {-0.7261476340134215, -0.35448694255683616, 0.1877357453911204, 0.5000002123423901, -0.33911094105443057,
            0.22371888097544382, 0.5275789576916142, 0.6388409751418227, 0.05552402986250782, 0.10536946388707473, 0.1711823585754391, 0.7100358384019819};

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


            Nelder_Mead nm = new Nelder_Mead(0.0000001f, tf.DeployCircle, values);
            nm.run();


        }

        private static void Main(string[] args)
        {
            // One rect test
            //IShape[] shapes = { new Rectangle(x1: -0.8, y1: -0.6, x2: 0.6, y2: 0.4, material: 1) };
            //double[] lowerBoundaries = { -1, -1, -1, -1, 0.5 };
            //double[] higherBoundaries = { 1, 1, 1, 1, 2 };

            // Three rects test
            IShape[] shapes = {
                new Rectangle(x1: -0.6, y1: 0.7, x2: 0.6, y2: 0.9, material: 1.7),
                new Rectangle(x1: -0.6, y1: -0.4, x2: -0.1, y2: 0.2, material: 0.8),
                new Rectangle(x1: 0.1, y1: -0.4, x2: 0.6, y2: 0.2, material: 1.4),
            };
            double[] lowerBoundaries = { -1, -1, -1, -1, 0.5, -1, -1, -1, -1, 0.5, -1, -1, -1, -1, 0.5 };
            double[] higherBoundaries = { 1, 1, 1, 1, 2, 1, 1, 1, 1, 2, 1, 1, 1, 1, 2 };

            // One circle test
            //IShape[] shapes = { new Circle(x: 0, y: 0, radius: 1.2, material: 1.2) };
            //double[] lowerBoundaries = { -1, -1, -1, 0.5 };
            //double[] higherBoundaries = { 1, 1, 1, 2 };

            //Three circles test
            //IShape[] shapes = {
            //     new Circle(x: 0, y: -0.6, radius: 0.7, material: 1.5),
            //     new Circle(x: 0, y: -0.6, radius: 0.7, material: 0.6),
            //     new Circle(x: 0.25, y: 0.3, radius: 0.1, material: 0.95),
            // };
            // double[] lowerBoundaries = { -1, -1, 0, 0.5, -1, -1, 0, 0.5, -1, -1, 0, 0.5 };
            // double[] higherBoundaries = { 1, 1, 1, 2, 1, 1, 1, 2, 1, 1, 1, 2 };

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

            int nLasers = 20;
            int iterations = 200;
            int population = 20;
            int dimensions = lowerBoundaries.Length;
            double[][] results = Tomograph.Run(shapes, nLasers);
            //Func<double[], double> testFunction = new TestFunction(nLasers, results).DeployPolygon()
            TestFunction testFunction = new TestFunction(nLasers, results);
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
            var algorithm = new ABCAlgorithm(population, iterations, testFunction.DeployRect, lowerBoundaries, higherBoundaries);
            //var algorithm = new JellyfishSearchOptimizer(testFunction, population, iterations, dimensions, higherBoundaries, lowerBoundaries);

            //algorithm.Solve();

            //Console.WriteLine($"FBest: {algorithm.FBest}");
            //Console.Write("XBest: ");

            //foreach (var x in algorithm.XBest)
            //{
            //    Console.Write(x + " ");
            //}

            //Console.WriteLine();
            polyRecreation();
            //deterministicThreeRectTest();
        }
    }
}