using System.Drawing;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Schema;
using ScottPlot;
using ScottPlot.Palettes;

namespace Lab4
{
    public struct Point
    {
        public double X;
        public double Y;

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
    }

    public class Program
    {
        public static double Func(double x)
        {
            return Math.Pow(x, 3) * Math.Cos(x);
        }

        public static void Main(string[] args)
        {
            const double xStart = -10d;
            const double xStop = 10d;
            const int count = 1000;
            const int pointsCount = 10;
            var x = DataGen.Range(xStart, xStop, Math.Abs(xStop - xStart) / count, true);
            var y = x.Select(Func).ToArray();
            var points = DataGen.Range(xStart, xStop, Math.Abs(xStop - xStart) / pointsCount, true).Select(x => new Point(x, Func(x))).ToArray();

            var lagrange = x.Select(k => LagrangeInterpolation(points, k)).ToArray();
            var newton = x.Select(k => NewtonInterpolation(points, k)).ToArray();

            var plt = new Plot();

            plt.AddScatter(x, y, lineWidth:2F, markerShape:MarkerShape.none);

            plt.AddScatter(x, lagrange, Color.Red, lineWidth:4F, markerShape:MarkerShape.none);
            plt.AddScatter(x, newton, Color.Green, lineWidth: 2F, markerShape: MarkerShape.none);
            plt.AddScatterPoints(points.Select(p => p.X).ToArray(), points.Select(p => p.Y).ToArray(), Color.Black, markerSize: 10F);

            plt.SaveFig("plot.png");
        }

        public static double LagrangeInterpolation(Point[] points, double xi)
        {
            var n = points.Length;
            var result = 0d;

            for (var i = 0; i < n; i++)
            {
                var t = points[i].Y;

                for (var j = 0; j < n; j++)
                {
                    if (j != i)
                    {
                        t = t * (xi - points[j].X) / (points[i].X - points[j].X);
                    }
                }

                result += t;
            }

            return result;
        }

        private static double[,] DivideDiffTable(double[] x, double[] y)
        {
            var n = x.Length;
            var table = new double[n, n];
            for (var i = 0; i < n; i++)
            {
                table[i, 0] = y[i];
            }
            for (var i = 1; i < n; i++)
            {
                for (var j = 0; j < n - i; j++)
                {
                    table[j, i] = (table[j, i - 1] - table[j + 1, i - 1]) / (x[j] - x[i + j]);
                }
            }

            return table;
        }

        public static double NewtonInterpolation(Point[] points, double xi)
        {
            var n = points.Length;
            var x = points.Select(p => p.X).ToArray();
            var table = DivideDiffTable(x, points.Select(p => p.Y).ToArray());

            var result = table[0,0];

            for (var i = 1; i < n; i++)
            {
                var product = 1d;
                for (var j = 0; j < i; j++)
                {
                    product *= xi - x[j];
                }

                result += product * table[0, i];
            }

            return result;
        }

        private static void SolveTriDiagonal(double[] sub, double[] diagonal, double[] sup, ref double[] b, int n)
        {
            for (var i = 2; i <= n; i++)
            {
                sub[i] /= diagonal[i - 1];
                diagonal[i] -= sub[i] * sup[i - 1];
                b[i] -= sub[i] * b[i - 1];
            }

            b[n] /= diagonal[n];

            for (var i = n - 1; i > 0; i--)
            {
                b[i] = (b[i] - sup[i] * b[i + 1]) / diagonal[i];
            }
        }

        public static double SplineInterpolation(Point[] points, double xi)
        {
            var n = points.Length;
            var x = points.Select(p => p.X).ToArray();
            var y = points.Select(p => p.X).ToArray();

            var _a = new double[n];
            var _h = new double[n];

            for (var i = 1; i < n; i++)
            {
                _h[i] = x[i] - x[i - 1];
            }

            if (n > 2)
            {
                var sub = new double[n - 1];
                var diagonal = new double[n - 1];
                var sup = new double[n - 1];

                for (var i = 1; i <= n - 2; i++)
                {
                    diagonal[i] = (_h[i] + _h[i + 1]) / 3;
                    sup[i] = _h[i + 1] / 6;
                    sub[i] = _h[i] / 6;
                    _a[i] = (y[i + 1] - y[i]) / _h[i + 1] - (y[i] - y[i - 1]) / _h[i];
                }

                SolveTriDiagonal(sub, diagonal, sup, ref _a, n - 2);
            }

            var index = 0;
            var value = double.MinValue;
            for (var i = 0; i < n; i++)
            {
                if (value < x[i] && x[i] < xi)
                {
                    value = x[i];
                    index = i + 1;
                }
            }
            index = Math.Max(index, 1);
            index = Math.Min(index, _h.Length - 1);

            var x1 = xi - value;
            var x2 = _h[index] - x1;

            return ((-_a[index - 1] / 6 * (x2 + _h[index]) * x1 + y[index - 1]) * x2 +
                    (-_a[index] / 6 * (x1 + _h[index]) * x2 + y[index]) * x1) / _h[index];
        }
    }
}