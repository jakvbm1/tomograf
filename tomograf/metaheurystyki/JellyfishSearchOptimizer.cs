// Based on https://www.mathworks.com/matlabcentral/fileexchange/78961-jellyfish-search-optimizer-js

namespace tomograf.metaheurystyki
{
    class JellyfishSearchOptimizer : IOptimizationAlgorithm
    {
        private readonly int population;
        private readonly Jellyfish[] jellies;
        private readonly int dimensions;
        private readonly double[] tabUp;
        private readonly double[] tabDown;
        private readonly Random rnd = new();
        private readonly int testNumber;
        private readonly Func<double[], double> fitnessFunction;
        private readonly int targetIterations;
        private int currentIteration;
        public int NumberOfEvaluationFitnessFunction { get; private set; }
        public long Time { get; private set; }


        public double[] XBest
        {
            get
            {
                double bestResult = jellies[0].fitness;
                int bestIndex = 0;

                for (int i = 1; i < population; i++)
                {
                    double result = jellies[i].fitness;

                    if (result < bestResult)
                    {
                        bestResult = result;
                        bestIndex = i;
                    }
                }

                return jellies[bestIndex].x;
            }
        }

        public double FBest
        {
            get
            {
                double bestResult = jellies[0].fitness;

                for (int i = 1; i < population; i++)
                {
                    double result = jellies[0].fitness;

                    if (result < bestResult)
                    {
                        bestResult = result;
                    }
                }

                return bestResult;
            }
        }

        // Create new instance of object from scratch
        public JellyfishSearchOptimizer(Func<double[], double> fitnessFunction, int population, int targetIterations, int dimensions, double[] tabUp, double[] tabDown)
        {
            this.fitnessFunction = fitnessFunction;
            this.population = population;
            this.targetIterations = targetIterations;
            this.Time = 0;
            this.currentIteration = 0;
            this.NumberOfEvaluationFitnessFunction = 0;
            //this.testNumber = Utils.findTestNumber(Acronym);
            this.jellies = new Jellyfish[this.population];
            this.tabUp = tabUp;
            this.tabDown = tabDown;
            this.dimensions = dimensions;

            jellies[0].x = new double[dimensions];
            for (int i = 0; i < dimensions; i++)
            {
                jellies[0].x[i] = rnd.NextDouble();
            }

            double a = 4;

            for (int i = 1; i < this.population; i++)
            {
                jellies[i].x = new double[dimensions];

                for (int j = 0; j < dimensions; j++)
                {
                    jellies[i].x[j] = a * jellies[i - 1].x[j] * (1 - jellies[i - 1].x[j]);
                }
            }

            for (int k = 0; k < dimensions; k++)
            {
                for (int i = 0; i < this.population; i++)
                {
                    jellies[i].x[k] = tabDown[k] + jellies[i].x[k] * (tabUp[k] - tabDown[k]);
                    jellies[i].fitness = CalculateFitnessFunction(jellies[i].x);
                }
            }

            Array.Sort(jellies, (a, b) => a.fitness.CompareTo(b.fitness));
        }

        // Create new instance of object based on state file
        //public JellyfishSearchOptimizer(int testNumber)
        //{
        //    var file = File.OpenText(Utils.getStateFilePath(Acronym, testNumber));
        //    file.ReadLine();    // Metadata headers
        //    var metadata = file.ReadLine().Split(';');

        //    var functionName = metadata[0].Trim();
        //    var dimensions = int.Parse(metadata[1]);
        //    this.FitnessFunction = AlgoBenchmark.FitnessFunctionType.FromParameters(functionName, dimensions);
        //    this.Population = int.Parse(metadata[2]);
        //    this.TargetIterations = int.Parse(metadata[3]);
        //    this.CurrentIteration = int.Parse(metadata[4]);
        //    this.NumberOfEvaluationFitnessFunction = int.Parse(metadata[5]);
        //    this.Time = long.Parse(metadata[6]);
        //    this.jellies = new Jellyfish[Population];

        //    file.ReadLine();    // Empty line
        //    file.ReadLine();    // X headers

        //    for (int i = 0; i < Population; i++)
        //    {
        //        var line = file.ReadLine().Split(';');

        //        for (int j = 0; j < dimensions; j++)
        //        {
        //            jellies[i].x[j] = double.Parse(line[j]);
        //        }
        //    }
        //}

        //private void SaveLoadableState()
        //{
        //    Directory.CreateDirectory("state");
        //    var file = File.CreateText(Utils.getStateFilePath(Acronym, testNumber));

        //    file.WriteLine("testNumber; fitnessFunction Name; fitnessFunction Dimensions; Population; TargetIterations; CurrentIteration; NumberOfEvaluationFitnessFunction; Time;");
        //    file.WriteLine($"{testNumber}; {FitnessFunction.Name}; {dimensions}; {Population}; {TargetIterations}; {CurrentIteration}; {NumberOfEvaluationFitnessFunction}; {Time};");

        //    file.WriteLine();

        //    for (int i = 0; i < dimensions; i++)
        //    {
        //        file.Write($"x{i}; ");
        //    }

        //    file.WriteLine();

        //    for (int i = 0; i < Population; i++)
        //    {
        //        foreach (var x in jellies[i].x)
        //        {
        //            file.Write($"{x}; ");
        //        }
        //        file.WriteLine();
        //    }

        //    file.Close();
        //}

        //private void SaveIterationState()
        //{
        //    string iterationStateDirectory = Utils.getTestDirectory(Acronym, testNumber);
        //    Directory.CreateDirectory(iterationStateDirectory);

        //    var file = File.CreateText($"{iterationStateDirectory}/iteration_{CurrentIteration}.txt");

        //    file.WriteLine($"{CurrentIteration} [numer iteracji]");
        //    file.WriteLine($"{NumberOfEvaluationFitnessFunction} [liczba wywołań funkcji celu]");

        //    for (int i = 0; i < Population; i++)
        //    {
        //        foreach (var x in jellies[i].x)
        //        {
        //            file.Write($"{x} ");
        //        }
        //        file.WriteLine();
        //    }

        //    file.Close();
        //}

        public void SaveToFileStateOfAlghoritm()
        {
            //bool shouldExit = false;

            //ConsoleCancelEventHandler preventExit = (sender, e) =>
            //{
            //    shouldExit = true;
            //    e.Cancel = true;
            //};

            //Console.CancelKeyPress += preventExit;

            //SaveIterationState();
            //SaveLoadableState();

            //if (shouldExit) System.Environment.Exit(0);
            //Console.CancelKeyPress -= preventExit;
        }

        public void LoadFromFileStateOfAlghoritm() { }

        public double Solve()
        {
            for (; currentIteration < targetIterations; currentIteration++)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();

                double[] Meanvl = Mean();
                Jellyfish BestSol = jellies[0];
                for (int i = 0; i < population; i++)
                {
                    // Calculate time control c(t) using Eq. (17);
                    double Ar = (1 - currentIteration * ((1) / targetIterations)) * (2 * rnd.NextDouble() - 1);
                    if (Math.Abs(Ar) >= 0.5)
                    {
                        //// Folowing to ocean current using Eq. (11)
                        Jellyfish newsol = new()
                        {
                            x = new double[dimensions]
                        };

                        for (int j = 0; j < dimensions; j++)
                        {
                            newsol.x[j] = jellies[i].x[j] + rnd.NextDouble() * (BestSol.x[j] - 3.0 * rnd.NextDouble() * Meanvl[j]);
                        }

                        // Check the boundary using Eq. (19)
                        newsol = Simplebounds(newsol);
                        // Evaluation
                        newsol.fitness = CalculateFitnessFunction(newsol.x);
                        // Comparison
                        if (newsol.fitness < jellies[i].fitness)
                        {
                            jellies[i] = newsol;
                            if (jellies[i].fitness < BestSol.fitness)
                            {
                                BestSol = jellies[i];
                            }
                        }
                    }
                    else
                    {
                        Jellyfish newsol = new()
                        {
                            x = new double[dimensions]
                        };

                        //// Moving inside swarm
                        if (rnd.NextDouble() <= (1d - Ar))
                        {
                            // Determine direction of jellyfish by Eq. (15)
                            int j = i;
                            while (j == i)
                            {
                                j = rnd.Next() % population;
                            }


                            var Step = new double[dimensions];
                            for (int k = 0; k < dimensions; k++)
                            {
                                Step[k] = jellies[i].x[k] - jellies[j].x[k];
                            }

                            if (jellies[j].fitness < jellies[i].fitness)
                            {
                                for (int k = 0; k < dimensions; k++)
                                {
                                    Step[k] *= -1;
                                }
                            }

                            // Active motions (Type B) using Eq. (16)
                            for (int k = 0; k < dimensions; k++)
                            {
                                newsol.x[k] = jellies[i].x[k] + rnd.NextDouble() * Step[k];
                            }

                        }
                        else
                        {
                            // Passive motions (Type A) using Eq. (12)
                            for (int j = 0; j < dimensions; j++)
                            {
                                newsol.x[j] = jellies[i].x[j] + 0.1 * (tabUp[j] - tabDown[j]) * rnd.NextDouble();
                            }
                        }
                        // Check the boundary using Eq. (19)
                        newsol = Simplebounds(newsol);
                        // Evaluation
                        newsol.fitness = CalculateFitnessFunction(newsol.x);
                        // Comparison
                        if (newsol.fitness < jellies[i].fitness)
                        {
                            jellies[i] = newsol;
                            if (jellies[i].fitness < BestSol.fitness)
                            {
                                BestSol = jellies[i];
                            }
                        }
                    }
                }

                watch.Stop();
                this.Time += watch.ElapsedMilliseconds;
                Array.Sort(jellies, (a, b) => a.fitness.CompareTo(b.fitness));
                SaveToFileStateOfAlghoritm();
                Console.WriteLine(FBest);
            }

            // Getting FBest in return statement requires calling FitnessFunction for each wolf
            NumberOfEvaluationFitnessFunction += population;
            //File.Delete(Utils.getStateFilePath(Acronym, testNumber));
            return FBest;
        }

        public void SaveResult()
        {
            //var resultFile = File.CreateText(Utils.getResultFilePath(Acronym, testNumber));
            //resultFile.WriteLine($"{NumberOfEvaluationFitnessFunction} [liczba wywołań funkcji celu]");
            //resultFile.Write($"{FBest} ");

            //foreach (var x in XBest)
            //{
            //    resultFile.Write($"{x} ");
            //}

            //resultFile.WriteLine("[najlepszy osobnik wraz z wartością funkcji celu]");
            //resultFile.WriteLine($"{Population} [populacja]");
            //resultFile.WriteLine($"{TargetIterations} [liczba iteracji]");

            //resultFile.Close();
        }

        private double[] Mean()
        {
            var m = new double[dimensions];

            for (int i = 0; i < dimensions; i++)
            {
                for (int j = 0; j < population; j++)
                {
                    m[i] += jellies[j].x[i];
                }

                m[i] /= population;
            }

            return m;
        }

        private Jellyfish Simplebounds(Jellyfish s)
        {
            Jellyfish ns_tmp = s;

            // Apply to the lower bound
            for (int i = 0; i < dimensions; i++)
            {
                while (ns_tmp.x[i] < tabDown[i])
                {
                    ns_tmp.x[i] = tabUp[i] + (ns_tmp.x[i] - tabDown[i]);
                }
            }

            // Apply to the upper bound
            for (int i = 0; i < dimensions; i++)
            {
                while (ns_tmp.x[i] > tabUp[i])
                {
                    ns_tmp.x[i] = tabDown[i] + (ns_tmp.x[i] - tabUp[i]);
                }
            }

            return ns_tmp;
        }

        private double CalculateFitnessFunction(double[] args)
        {
            NumberOfEvaluationFitnessFunction++;
            return fitnessFunction(args);
        }

        struct Jellyfish
        {
            public double[] x;
            public double fitness;
        }
    }
}