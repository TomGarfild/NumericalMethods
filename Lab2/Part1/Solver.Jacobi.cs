namespace Lab2.Part1;

public partial class Solver
{
    public static double[] JacobiMethod(double[,] matrix, double[] b)
    {
        int count = 0;
        var dimension = matrix.GetLength(0);

        var result = Enumerable.Repeat(1.0, dimension).ToArray();
        var prevResult = new double[dimension];

        while (true)
        {
            count++;
            Array.Copy(result, prevResult, dimension);

            for (var i = 0; i < dimension; i++)
            {
                var delta = b[i];

                for (var j = 0; j < dimension; j++)
                {
                    if (j != i)
                    {
                        delta -= matrix[i, j] * prevResult[j];
                    }
                }

                result[i] = delta / matrix[i, i];

            }


            if (count ==25)
            {
                break;
            }

            if (MatrixHelper.IsVectorNorm(result.Zip(prevResult, (a, b) => a - b).ToArray()))
            {
                break;
            }
        }

        return result;
    }
}