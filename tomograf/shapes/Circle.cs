namespace tomograf.shapes
{
    internal class Circle : IShape
    {
        private readonly double x, y, radius, material;

        public Circle(double x, double y, double radius, double material)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
            this.material = material;
        }

        public double CalcEnergyLoss(double laserA, double laserB)
        {
            double d = Math.Abs(laserA * x - y + laserB) / Math.Sqrt(laserA * laserA + 1);

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
