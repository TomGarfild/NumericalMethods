namespace Lab3;

public class FirstSystem : ISystem
{
    public double[] F(double[] x)
    {
        return new[] { F1(x), F2(x) };
    }

    public double[,] GetJacobiMatrix(double[] x)
    {
        return new[,] { { dF1dx(x), dF1dy(x) }, { dF2dx(x), dF2dy(x) } };
    }

    private static double F1(double[] x)
    {
        return Math.Pow(x[0], 2) / Math.Pow(x[1], 2) - Math.Cos(x[1]) - 2;
    }

    private static double dF1dx(double[] x)
    {
        return 2 * x[0] / Math.Pow(x[1], 2);
    }

    private static double dF1dy(double[] x)
    {
        return -2 * Math.Pow(x[1], 2) / Math.Pow(x[1], 3) + Math.Sin(x[1]);
    }

    private static double F2(double[] x)
    {
        return Math.Pow(x[0], 2) + Math.Pow(x[1], 2) - 6;
    }

    private static double dF2dx(double[] x)
    {
        return 2 * x[0];
    }

    private static double dF2dy(double[] x)
    {
        return 2 * x[1];
    }
}