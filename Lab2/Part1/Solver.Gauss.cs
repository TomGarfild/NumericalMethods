namespace Lab2.Part1;

public partial class Solver
{
    private static int FindLeadingElement(double[,] matrix, int column)
    {
        var dimension = matrix.GetLength(0);
        var result = column;

        for (var i = column + 1; i < dimension; i++)
        {
            if (Math.Abs(matrix[i, column]) > Math.Abs(matrix[result, column]))
            {
                result = i;
            }
        }

        return result;
    }

    [Obsolete]
    public static double[] GaussMethod(double[,] matrix, double[] b)
    {
        var dimension = matrix.GetLength(0);

        var matrixCopy = matrix.Clone() as double[,] ?? new double[dimension, dimension];
        var bCopy = new double[dimension, 1];
        for (var i = 0; i < dimension; i++)
        {
            bCopy[i, 0] = b[i];
        }

        var result = new double[dimension];

        for (var i = 0; i < dimension; i++)
        {
            var leadingIdx = FindLeadingElement(matrix, i);
            var permutationMatrix = MatrixHelper.GetPermutationMatrix(dimension, i, leadingIdx);
            matrixCopy = MatrixHelper.Multiply(permutationMatrix, matrixCopy);

            var m = MatrixHelper.GetMMatrix(matrixCopy, i);

            matrixCopy = MatrixHelper.Multiply(m, matrixCopy);
            bCopy = MatrixHelper.Multiply(m, MatrixHelper.Multiply(permutationMatrix, bCopy));
        }

        for (var i = dimension - 1; i >= 0; i--)
        {
            result[i] = bCopy[i, 0];
            for (var j = 0; j < i; j++)
            {
                bCopy[j, 0] -= matrixCopy[j, i] * result[i];
            }
        }

        return result;
    }
}