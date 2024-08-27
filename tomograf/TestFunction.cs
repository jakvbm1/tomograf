using tomograf.shapes;

namespace tomograf
{
    class TestFunction
    {
        readonly int nLasers;
        readonly double[][] values;

        public TestFunction(int nLasers , double[][] values)
        {
            this.nLasers = nLasers;
            this.values = values;
        }


        public double DeployRect(params double[] args)
        {
            var shapes = new IShape[args.Length / 5];
            for (int i = 0; i < args.Length / 5; i++)
            {
                shapes[i] = new Rectangle(
                    x1: args[5 * i],
                    y1: args[5 * i + 1],
                    x2: args[5 * i + 2],
                    y2: args[5 * i + 3],
                    material: args[5 * i + 4]
                );
            }
            
            return Deploy(Tomograph.Run(shapes, nLasers));
        }

        public double DeployCircle(params double[] args) 
        {
            var shapes = new IShape[args.Length / 4];
            for (int i = 0; i < args.Length / 4; i++)
            {
                shapes[i] = new Circle(
                    x: args[5 * i],
                    y: args[5 * i + 1],
                    radius: args[5 * i + 2],
                    material: args[5 * i + 3]
                );
            }

            return Deploy(Tomograph.Run(shapes, nLasers));
        }
        private double Deploy(double[][] args)
        {
            double val = 0;
            for (int i = 0; i < nLasers; i++) 
            {
                for (int j = 0; j < nLasers; j++)
                {
                    val += Math.Abs(args[i][j] - values[i][j]);
                }
            }
            return val;
        }
    }
}
