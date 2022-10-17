namespace Lab2;

public partial class Solver
{
    public static double[] GaussEliminationMethod(double[,] mat, double[] b)
    {
        var dimension = mat.GetLength(0);
        var matrix = mat.Clone() as double[,] ?? new double[dimension,dimension];
        var bCopy = b.Clone() as double[] ?? new double[dimension];

        for (var k = 0; k < dimension; k++)
        {
            for (var i = k + 1; i < dimension; i++)
            {
                var m = matrix[i, k] / matrix[k, k];

                for (var j = 0; j < dimension; j++)
                {
                    matrix[i, j] -= matrix[k, j] * m;
                }

                bCopy[i] -= bCopy[k] * m;
            }
        }

        double t = 0;
        double u = 0;
        var x = new double[dimension];

        for (var i = dimension - 1; i >= 0; i--)
        {
            for (var j = dimension - 1; j >= 0; j--)
            {
                if (i == j)
                {
                    u = matrix[i, j];
                    break;
                }

                t += matrix[i, j] * bCopy[j];
            }

            bCopy[i] = (bCopy[i] - t) / u;
            x[i] = bCopy[i];
            t = 0;
        }

        return x;
    }
}