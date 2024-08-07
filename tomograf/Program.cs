﻿// See https://aka.ms/new-console-template for more information
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


            double[] x1 = { -0.4 };
            double[] y1 = { -0.6 };
            double[] x2 = { 0.6 };
            double[] y2 = { 0.4 };
            double[] material = { 1 };
            Rect_Tomograph tomograf = new Rect_Tomograph(5, x1, y1, x2, y2, material);
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


            double[] x = { 0.1 };
            double[] y = { -0.1 };
            double[] r = { 0.5 };
            Circle_tomograph ct = new Circle_tomograph(x, y, r, 5, material);
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


            Point[] rectangle = {
                new() { x = -0.4, y = -0.6 },
                new() { x = 0.6, y = -0.6 },
                new() { x = 0.6, y = 0.4 },
                new() { x = -0.4, y = 0.4 },
            };
            Point[][] polygons = { rectangle };
            Polygon_Tomograph pt = new (5, polygons, material);
            double[][] polygon_res = pt.Run();
            foreach (var i in polygon_res)
            {
                foreach (var j in i)
                {
                    Console.Write(j + " ");
                }
                Console.WriteLine();
            }
        }
    }
}