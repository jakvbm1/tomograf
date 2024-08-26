using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tomograf.shapes
{
    internal class Circle : IShape
    {
        private double x, y, radius, material;

        public Circle(double x, double y, double radius, double material)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
            this.material = material;
        }

        public double CalcEnergyLoss(double laser_a, double laser_b)
        {
            double d = Math.Abs(laser_a * x - y + laser_b) / Math.Sqrt(laser_a * laser_a + 1);

            // jesli odleglosc jest mniejsza niz promien, to promien przecina kolo
            if (d < radius) 
            {
                return 2 * Math.Sqrt(Math.Pow(radius, 2) - Math.Pow(d, 2)) * material;
            }

            return 0;
        }

        public string ShapeDescription()
        {
            return $"Circle material: {material}, x: {x}, y: {y}, radius: {radius}";
        }
    }
}
