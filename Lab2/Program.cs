namespace Lab2
{
    internal class Program
    {
        private const int N = 10;

        private static void Main(string[] args)
        {
            var matrix = MatrixHelper.GenerateMatrix(N);
            var b = MatrixHelper.GenerateVector(N);
            //matrix = new [,] { { 1.0, -4.0, -5.0 }, { 2.0, 4.0, -2.0 }, { -4.0, 4.0, -6.0 } };
            //b = new[] { 1.0, -22.0, -34.0 };

            //matrix = new[,] { { 76.0, -17, -31, 23 }, { 20, 96, 3, -22 }, { -1, -23, 59, -8, }, { -48, -16, -6, 114 } };
            //b = new[] { -45, 25.0, -35.0, -17 };
            //matrix = new [,] { { 4, 1, 2 }, { 3, 5, 1.0 }, { 1, 1, 3 } };
            //b = new[] { 4, 7, 3.0 };    

            MatrixHelper.Print(matrix, b);


            var res = Solver.GaussEliminationMethod(matrix, b);
            Console.WriteLine("Gauss method");
            MatrixHelper.PrintVector(res);


            res = Solver.JacobiMethod(matrix, b);
            Console.WriteLine("Jacobi method");
            MatrixHelper.PrintVector(res);


            res = Solver.SeidelMethod(matrix, b);
            Console.WriteLine("Seidel method");
            MatrixHelper.PrintVector(res);
        }
    }
}