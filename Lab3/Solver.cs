using MathNet.Numerics.LinearAlgebra;

namespace Lab3;

public class Solver
{
    private const double Eps = 1e-6;
    
    public static double[] NewtonMethod(double[] x0, ISystem system, bool isModified = false)
    {
        var res = Vector<double>.Build.Dense(x0);
        var jacobiMatrix = system.GetJacobiMatrix(res.Storage.AsArray());
        var count = 0;
        while (true)
        {
            count++;
            if (!isModified)
            {
                jacobiMatrix = system.GetJacobiMatrix(res.Storage.AsArray());
            }
            
            var A = Matrix<double>.Build.DenseOfArray(jacobiMatrix);
            var b = Vector<double>.Build.Dense(system.F(res.Storage.AsArray()));
            var z = A.Solve(b);

            res -= z;

            if (z.Norm(1) <= Eps)
            {
                Console.WriteLine(count);
                break;
            }
        }

        return res.Storage.AsArray();
    }

    public static double[] RelaxationMethod(double[] x0, ISystem system, double tau)
    {
        var res = Vector<double>.Build.Dense(x0);

        while (true)
        {
            var prevRes = Vector<double>.Build.DenseOfVector(res);
            res -= Vector<double>.Build.Dense(system.F(res.Storage.AsArray())) * tau;

            if ((res - prevRes).Norm(1) <= Eps)
            {
                break;
            }
        }

        return res.Storage.AsArray();
    }
}