namespace Lab2;

public class MatrixHelper
{
    private const double Eps = 1e-5;
    private const int LowerBound = -100;
    private const int UpperBound = 100;

    private static readonly Random _random = new Random(69);

    public static void Print(double[,] matrix, double[] b)
    {
        var n = matrix.GetLength(0);
        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
            {
                Console.Write($"{matrix[i, j].ToString("F2"),7} ");
            }

            Console.Write($"| {b[i].ToString("F2"),7}");

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    public static void PrintMatrix(double[,] matrix)
    {
        var n = matrix.GetLength(0);
        var m = matrix.GetLength(1);
        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < m; j++)
            {
                Console.Write($"{matrix[i, j].ToString("F2"), 7}");
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }
    public static void PrintVector(double[] vector)
    {
        var n = vector.GetLength(0);
        for (var i = 0; i < n; i++)
        {
            Console.WriteLine($"{vector[i].ToString("F2"),7}");
        }

        Console.WriteLine();
    }


    public static double[,] GenerateMatrix(int n, bool hilbert = false)
    {
        var result = new double[n, n];

        for (var i = 0; i < n; i++)
        {
            var value = 0d;
            for (var j = 0; j < n; j++)
            {
                result[i, j] = hilbert ? 1.0 / (i + j + 1) : _random.Next(LowerBound, UpperBound);
                value += Math.Abs(result[i, j]);
            }

            result[i, i] = value + 1;
        }

        return result;
    }

    public static double[,] Multiply(double[,] a, double[] b)
    {
        var dimension = b.GetLength(0);

        var bNew = new double[dimension, 1];

        for (var i = 0; i < dimension; i++)
        {
            bNew[i, 0] = b[i];
        }

        return Multiply(a, bNew);
    }

    public static double[,] Multiply(double[,] a, double[,] b)
    {
        var L = a.GetLength(1);
        if (L != b.GetLength(0))
        {
            throw new ArgumentException("Wrong sizes of arrays");
        }
        var n = a.GetLength(0);
        var m = b.GetLength(1);
        var result = new double[n,m];

        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < m; j++)
            {
                result[i, j] = 0;
                for (var k = 0; k < L; k++)
                {
                    result[i, j] += a[i, k] * b[k, j];
                }
            }
        }

        return result;
    }

    public static double[,] GetPermutationMatrix(int dimension, int k, int l)
    {
        var result = GetIdentityMatrix(dimension);

        result[k, l] = result[l, k] = 1;
        result[k, k] = result[l, l] = 0;

        return result;
    }

    public static double[,] GetMMatrix(double[,] matrix, int k)
    {
        var dimension = matrix.GetLength(0);

        var result = GetIdentityMatrix(dimension);

        result[k, k] = 1 / matrix[k, k];

        for (var i = k+1; i < dimension; i++)
        {
            result[i, k] = -matrix[i, k] / matrix[k, k];
        }

        return result;
    }

    public static double[,] GetIdentityMatrix(int dimension)
    {
        var result = new double[dimension, dimension];


        for (var i = 0; i < dimension; i++)
        {
            result[i, i] = 1;
        }

        return result;
    }

    public static double[] GenerateVector(int n)
    {
        var result = new double[n];

        for (var i = 0; i < n; i++)
        {
            result[i] = _random.Next(LowerBound, UpperBound);
        }

        return result;
    }

    public static bool IsVectorNorm(double[] vector)
    {
        return vector.Select(Math.Abs).Max() <= Eps;
    }
}