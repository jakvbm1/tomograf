namespace tomograf.deterministyczne
{
    internal class HookeJeeves
    {
        private readonly double[] startPoint;
        private readonly Func<double[], double> fitnessFunction;
        private readonly double initialStepSize, stepReductionFactor, precision;
        public double[] XBest;
        public double FBest;
        public int nCalls = 0, iterations = 0;


        public HookeJeeves(double[] startPoint, Func<double[], double> fitnessFunction, double initialStepSize, double stepReductionFactor, double precision)
        {
            this.startPoint = startPoint;
            this.fitnessFunction = fitnessFunction;
            this.initialStepSize = initialStepSize;
            this.stepReductionFactor = stepReductionFactor;
            this.precision = precision;
        }

        private double call(double[] args) { nCalls++; return fitnessFunction(args); } 
        public double[] Run()
        {
            double stepSize = this.initialStepSize;
            int n = startPoint.Length;
            double[] xB0 = new double[n];
            double[] x0 = new double[n];
            Array.Copy(startPoint, xB0, n);
            Array.Copy(startPoint, x0, n);
            double q0 = fitnessFunction(x0);
            double qB0 = fitnessFunction(xB0);

            while (stepSize > precision)
            {
                for (int i = 0; i < n; i++)
                {
                    double t = x0[i];

                    x0[i] = t + stepSize;
                    double q = call(x0);
                    if (q < q0)
                    {
                        q0 = q;
                        continue;
                    }

                    x0[i] = t - stepSize;
                    q = call(x0);
                    if (q < q0)
                    {
                        q0 = q;
                        continue;
                    }

                    x0[i] = t;
                }

                if (q0 < qB0)
                {
                    double[] xB = new double[n];
                    Array.Copy(x0, xB, n);
                    for (int i = 0; i < n; i++)
                    {
                        x0[i] = 2 * xB[i] - xB0[i];
                    }

                    xB0 = xB;
                    qB0 = q0;
                    q0 = call(x0);
                }
                else
                {
                    x0 = xB0;
                    q0 = qB0;
                    stepSize *= stepReductionFactor;
                }
                iterations++;
            }

            XBest = xB0;
            FBest = qB0;

            return xB0;
        }
    }
}
