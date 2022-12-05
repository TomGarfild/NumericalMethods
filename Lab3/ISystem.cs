namespace Lab3;

public interface ISystem
{
    public double[] F(double[] x);
    public double[,] GetJacobiMatrix(double[] x);
}