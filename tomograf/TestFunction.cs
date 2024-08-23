using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tomograf
{
    internal class TestFunction
    {
        int n_lasers;
        double[][] values;

        TestFunction(int n_lasers , double[][] values)
        {
            this.n_lasers = n_lasers;
            this.values = values;
        }

        double deploy(double[][] args)
        {
            double val = 0;
            for (int i = 0; i < n_lasers; i++) 
            {
                for (int j = 0; j < n_lasers; j++)
                {
                    val += Math.Abs(args[i][j] - values[i][j]);
                }
            }
            return val;
        }
    }
}
