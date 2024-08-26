using System;

namespace tomograf.shapes
{
    public interface IShape
    {
        double CalcEnergyLoss(double laser_a, double laser_b);
        string ShapeDescription();
    }
}
