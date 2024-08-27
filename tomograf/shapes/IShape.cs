namespace tomograf.shapes
{
    public interface IShape
    {
        double CalcEnergyLoss(double laserA, double laserB);
        string ShapeDescription();
    }
}
