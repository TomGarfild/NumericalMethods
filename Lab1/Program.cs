using Lab1;

Execute(() =>
{
    var bisectionRoot = Solver.BisectionMethod(a: -3, b: -2);
    PrintRoot(bisectionRoot, nameof(Solver.BisectionMethod));
});

Execute(() =>
{
    var relaxationRoot = Solver.RelaxationMethod(a: -3, b: -2);
    PrintRoot(relaxationRoot, nameof(Solver.RelaxationMethod));
});

Execute(() =>
{
    var newtonRoot = Solver.NewtonMethod(x: -3);
    PrintRoot(newtonRoot, nameof(Solver.NewtonMethod));
});

void PrintRoot(double d, string methodName)
{
    Console.WriteLine($"The root from {methodName} is : {d}");
}

void Execute(Action action)
{
    try
    {
        action();
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine(ex.Message);
    }
    catch (NullReferenceException)
    {
        Console.WriteLine("Function was null");
    }
}
