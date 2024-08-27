namespace tomograf.shapes
{
    class Rectangle : IShape
    {
        private readonly double x1, y1, x2, y2, material;

        public Rectangle(double x1, double y1, double x2, double y2, double material)
        {
            // dodalem ta linijke zeby sie upewnic zawsze ze (x1, y1) to lewy dolny, laser_a (x2, y2) to prawy gorny
            this.x1 = Math.Min(x1, x2);
            this.y1 = Math.Min(y1, y2);
            this.x2 = Math.Max(x1, x2);
            this.y2 = Math.Max(y1, y2);
            this.material = material;
        }

        public double CalcEnergyLoss(double laserA, double laserB)
        {
            // Sprawdzanie czy laser idzie na wprost
            if (laserA == 0 && laserB > y1 && laserB < y2)
            {
                return material * (x2 - x1);
            }

            if (laserA < 0)
            {
                // sprawdzanie czy promien "wchodzi" od lewej sciany
                double yCheck = laserA * x1 + laserB;
                double entryX = 1;
                double entryY = 1;
                if (yCheck >= y1 && yCheck <= y2)
                {
                    entryX = x1;
                    entryY = yCheck;
                }
                else
                {
                    // sprawdzanie czy promien wchodzi od gory
                    double xCheck = (y2 - laserB) / laserA;
                    if (xCheck >= x1 && xCheck <= x2)
                    {
                        entryX = xCheck;
                        entryY = y2;
                    }
                }

                if (entryX != 1 || entryY != 1)
                {
                    //sprawdzanie czy promien wychodzi od prawej sciany
                    yCheck = laserA * x2 + laserB;
                    double exitX = 0;
                    double exitY = 0;
                    if (yCheck >= y1 && yCheck <= y2)
                    {
                        exitX = x2;
                        exitY = yCheck;
                    }
                    else
                    {
                        //sprawdzanie czy promien wychodzi od dolu
                        double xCheck = (y1 - laserB) / laserA;
                        if (xCheck >= x1 && xCheck <= x2)
                        {
                            exitX = xCheck;
                            exitY = y1;
                        }
                    }

                    double distance = Math.Sqrt(Math.Pow(exitX - entryX, 2) + Math.Pow(exitY - entryY, 2));
                    return material * distance;
                }
            }

            if (laserA > 0)
            {
                // sprawdzanie czy promien "wchodzi" od lewej sciany
                double yCheck = laserA * x1 + laserB;
                double entryX = 1;
                double entryY = 1;
                if (yCheck >= y1 && yCheck <= y2)
                {
                    entryX = x1;
                    entryY = yCheck;
                }
                else
                {
                    // sprawdzanie czy promien wchodzi od dolu
                    double xCheck = (y1 - laserB) / laserA;
                    if (xCheck >= x1 && xCheck <= x2)
                    {
                        entryX = xCheck;
                        entryY = y1;
                    }
                }

                if (entryX != 1 || entryY != 1)
                {
                    //sprawdzanie czy promien wychodzi od prawej sciany
                    yCheck = laserA * x2 + laserB;
                    double exitX = 0;
                    double exitY = 0;
                    if (yCheck >= y1 && yCheck <= y2)
                    {
                        exitX = x2;
                        exitY = yCheck;
                    }
                    else
                    {
                        // sprawdzanie czy promien wychodzi od gory
                        double xCheck = (y2 - laserB) / laserA;
                        if (xCheck >= x1 && xCheck <= x2)
                        {
                            exitX = xCheck;
                            exitY = y2;
                        }
                    }
                    double distance = Math.Sqrt(Math.Pow(exitX - entryX, 2) + Math.Pow(exitY - entryY, 2));
                    return material * distance;
                }
            }

            return 0;
        }

        public string ShapeDescription()
        {
            return $"Rectangle material: {material}, x1: {x1}, y1: {y1}, x2: {x2}, y2: {y2}";
        }
    }
}

