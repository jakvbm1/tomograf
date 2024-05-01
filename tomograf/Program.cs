// See https://aka.ms/new-console-template for more information
using tomograf;

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
        Tomograf tomograf = new Tomograf(5, 1, x1, y1, x2, y2, material);

        double[][] wyniki = tomograf.Run();
        foreach (double[] x in wyniki) 
        {
            foreach (double y in x) 
            {
                Console.Write(y+" ");
            }
            Console.WriteLine();
        }
}
}