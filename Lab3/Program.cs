using MathNet.Numerics.LinearAlgebra;

namespace Lab3
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Run(Enumerable.Repeat(1.0, 2).ToArray(), new FirstSystem(), 0.001);
            Run(Enumerable.Repeat(1.0, 10).ToArray(), new SecondSystem(), 0.001, new[] { 1, 1.8, 2.5, 4, 5, 5.5, 6.5, 7.8, 9, 10 });
        }

        private static void Run(double[] x0, ISystem system, double tau, double[]? x0Modified = null)
        {

            Console.WriteLine("Newton Method");
            Print(Solver.NewtonMethod(x0, system), system.F);

            Console.WriteLine("Modified Newton Method");
            Print(Solver.NewtonMethod(x0Modified ?? x0, system, true), system.F);

            Console.WriteLine("Relaxation Method");
            Print(Solver.RelaxationMethod(x0, system, tau), system.F);
        }

        private static void Print(double[] res, Func<double[], double[]> func)
        {
            foreach (var v in res)
            {
                Console.Write($"{v} ");
            }

            Console.WriteLine();

            Console.WriteLine(Vector<double>.Build.Dense(func(res)).Norm(1));
            Console.WriteLine();
        }
    }
}