// See https://aka.ms/new-console-template for more information
using tomograf;

namespace tomograf
{ 
internal class Program
{
    private static void Main(string[] args)
    {
        //double[] x1 = { -0.2, -1, 0.05 };
        //double[] y1 = { -0.5, -0.7, 0.6 };
        //double[] x2 = { 0.1, -0.2, 0.85 };
        //double[] y2 = { 0.4, -0.6, 0.9};
        //double[] material = {3, 2, 1};
        //Tomograf tomograf = new Tomograf(5, 3, x1, y1, x2, y2, material);


        double[] x1 = {-0.4};
        double[] y1 = { -0.6 };
        double[] x2 = { 0.6};
        double[] y2 = { 0.4};
        double[] material = {1};
            Square_Tomograph tomograf = new Square_Tomograph(5, 1, x1, y1, x2, y2, material);

        double[][] wyniki = tomograf.Run();
        foreach (double[] xx in wyniki) 
        {
            foreach (double yy in xx) 
            {
                //Console.Write(yy+" ");
            }
            //Console.WriteLine();
        }

            double[] x = { 0.4, -0.3, 0 };
            double[] y = { -0.6, 0.4, 0 };
            double[] r = { 0.3, 0.1, 0.5 };

            Circle_tomograph ct = new Circle_tomograph(x, y, r, 5);
            double[][] circle_res = ct.Run();
            foreach(var i  in circle_res)
            {
                foreach(var j in i)
                {
                    Console.Write(j+" ");
                }
                Console.WriteLine();
            }
}
}
}