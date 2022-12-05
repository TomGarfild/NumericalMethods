namespace Lab3;

public class SecondSystem : ISystem
{
    public double[] F(double[] x)
    {
        var n = x.Length;
        var res = new double[n];
        for (var i = 0; i < n; i++)
        {
            res[i] = Fi(x, i);
        }

        return res;
    }

    public double[,] GetJacobiMatrix(double[] x)
    {
        var n = x.Length;
        var res = new double[n, n];

        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
            {
                res[i, j] = dFdxj(x[j], i == j ? 3 : 2);
            }
        }

        return res;
    }

    private static double Fi(double[] x, int idx)
    {
        double res = 0;

        for (int i = 0, n = x.Length; i < n; i++)
        {
            var power = i == idx ? 3 : 2;
            res += Math.Pow(x[i], power) - Math.Pow(i + 1.0, power);
        }

        return res;
    }

    private static double dFdxj(double value, int power)
    {
        return power * Math.Pow(value, power - 1);
    }
}