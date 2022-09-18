namespace Lab1;

internal class Solver
{
    private const double Epsilon = 1e-6;

    internal static Func<double, double> Func { get; set; } = x => x * x * x + 3 * x * x - 1;
    internal static Func<double, double> DerivativeOfFunc { get; set; } = x => 3 * x * x + 6 * x;

    public static double BisectionMethod(double a, double b)
    {
        if (Func(a) * Func(b) >= 0)
        {
            throw new ArgumentException($"Wrong arguments for bisection method a: {a} and b: {b}");
        }

        var c = a;
        while (Math.Abs(b - a) >= Epsilon)
        {
            c = (a + b) / 2;
            if (Math.Abs(Func(c)) < Epsilon)
            {
                break;
            }

            if (Func(c) * Func(a) < 0)
            {
                b = c;
            }
            else
            {
                a = c;
            }
        }

        return c;
    }

    public static double RelaxationMethod(double a, double b)
    {
        const int n = 1000;
        var points = Linspace(a, b, n);

        // Min derivative of func
        var m1 = points.Select(DerivativeOfFunc).Min();

        // Max derivative of func
        var M1 = points.Select(DerivativeOfFunc).Max();

        var tau = 2 / (M1 + m1);

        var x = (a + b) / 2;
        double xPrev;

        do
        {
            xPrev = x;
            x += (DerivativeOfFunc(x) > 0 ? -1 : 1) * tau * Func(x);
        }
        while (Math.Abs(x - xPrev) >= Epsilon);

        return x;
    }
    
    public static double NewtonMethod(double x)
    {
        var h = Func(x) / DerivativeOfFunc(x);
        while (Math.Abs(h) >= Epsilon)
        {
            h = Func(x) / DerivativeOfFunc(x);

            // x(i+1) = x(i) - f(x) / f'(x)
            x -= h;
        }

        return x;
    }

    private static double[] Linspace(double startValue, double endValue, int steps)
    {
        var interval = endValue / Math.Abs(endValue) * Math.Abs(endValue - startValue) / (steps - 1);

        return Enumerable.Range(0, steps)
                         .Select(value => startValue + value * interval)
                         .ToArray();
    }
}