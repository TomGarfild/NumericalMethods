using Lab2.Part1;

namespace Lab2
{
    internal class Program
    {
        private const int N = 4;
        private const double alpha = 0.85;

        private static void Main(string[] args)
        {
            Part2();
        }

        private static void Part1()
        {
            var matrix = MatrixHelper.GenerateMatrix(N, true);
            var v1 = new double[N, 1];


            for (int i = 0; i < N; i++)
            {
                v1[i, 0] = 1;
            }

            var bc = MatrixHelper.Multiply(matrix, v1);
            var b = new double[N];
            for (int i = 0; i < N; i++)
            {
                b[i] = bc[i, 0];
            }
            //matrix = new [,] { { 1.0, -4.0, -5.0 }, { 2.0, 4.0, -2.0 }, { -4.0, 4.0, -6.0 } };
            //b = new[] { 1.0, -22.0, -34.0 };

            //matrix = new[,] { { 76.0, -17, -31, 23 }, { 20, 96, 3, -22 }, { -1, -23, 59, -8, }, { -48, -16, -6, 114 } };
            //b = new[] { -45, 25.0, -35.0, -17 };
            //matrix = new [,] { { 4, 1, 2 }, { 3, 5, 1.0 }, { 1, 1, 3 } };
            //b = new[] { 4, 7, 3.0 };    

            // MatrixHelper.Print(matrix, b);


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

        private static void Part2()
        {
            var adj = GenerateRandomWebGraph(N);
            //adj = new double[,]
            //{
            //    { 0, 1, 1, 0 },
            //    { 1, 0, 0, 0 },
            //    { 0, 1, 0, 1 },
            //    { 1, 1, 1, 0 }
            //};
            MatrixHelper.PrintMatrix(adj);

            Console.WriteLine("A:");
            var a = GetMatrixA(adj);
            MatrixHelper.PrintMatrix(a);

            Console.WriteLine("M:");
            var m = GetMatrixM(adj);
            MatrixHelper.PrintMatrix(m);


            Console.WriteLine("PageRank:");
            var pageRank = PageRank(adj);
            MatrixHelper.PrintVector(pageRank);
        }

        public static double[,] GenerateRandomWebGraph(int n)
        {
            var result = new double[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j) continue;
                    result[i, j] = Random.Shared.Next(0, 2);
                }
            }

            return result;
        }

        public static double[,] GetMatrixA(double[,] adj)
        {
            var n = adj.GetLength(0);
            var copy = adj.Clone() as double[,];
            var sums = new double[n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    sums[i] += adj[i, j];
                }

                if (sums[i] == 0)
                {
                    for (int j = 0; j < n; j++)
                    {
                        copy![i, j] = 1;
                    }

                    sums[i] = n;
                }
            }

            var res = new double[n,n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    res[i, j] = copy![j, i] / sums[j];
                }
            }

            return res;
        }

        public static double[,] GetMatrixM(double[,] adj)
        {
            var n = adj.GetLength(0);
            var a = GetMatrixA(adj);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    a[i, j] = alpha * a[i, j] + (1 - alpha) / n;
                }
            }

            return a;
        }

        public static double[] PageRank(double[,] adj)
        {
            var matrixM = GetMatrixM(adj);

            var n = adj.GetLength(0);

            var res = new double[n];
            for (int i = 0; i < n; i++) res[i] = 1;
            var prevRes = new double[n];
            
            while (true)
            {
                Array.Copy(res, prevRes, n);
                res = MatrixHelper.Multiply(matrixM, res);

                if (MatrixHelper.IsVectorNorm(res.Zip(prevRes, (a, b) => a - b).ToArray()))
                {
                    break;
                }
            }

            return res;
        }
    }
}