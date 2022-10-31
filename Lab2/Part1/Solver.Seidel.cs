namespace Lab2.Part1;

public partial class Solver
{
    public static double[] SeidelMethod(double[,] matrix, double[] b)
    {
        var dimension = matrix.GetLength(0);

        var result = Enumerable.Repeat(0.0, dimension).ToArray();
        var prevResult = new double[dimension];

        while (true)
        {
            Array.Copy(result, prevResult, dimension);

            for (var i = 0; i < dimension; i++)
            {
                var delta = b[i];

                for (var j = 0; j < dimension; j++)
                {
                    if (j < i)
                    {
                        delta -= matrix[i, j] * result[j];
                    }
                    else if (j > i)
                    {
                        delta -= matrix[i, j] * prevResult[j];
                    }
                }

                result[i] = delta / matrix[i, i];
            }

            if (MatrixHelper.IsVectorNorm(result.Zip(prevResult, (a, b) => a - b).ToArray()))
            {
                break;
            }
        }

        return result;
    }
}