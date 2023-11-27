internal class Program
{
    private static void Main(string[] args)
    {
        int length = 0;
        Console.WriteLine("Enter array length:");
        do
        {
            Console.Write(">> ");
        } while (!int.TryParse(Console.ReadLine(), out length));
            
        double[] array = new double[length];

        Random random = new Random();
        for (int i = 0; i < length; i++) {
            array[i] = Math.Round(RandomDoubleBetween(random, -10, 10), 2);
        }
        
        Console.WriteLine("Generated array:\n[" + string.Join(", ", array) + "]");

        double negSum = 0;
        array.AsParallel().ForAll((i) => {
            if (i < 0) negSum += i; 
        });

        double maxByAbs = Math.Abs(array[0]);
        array.AsParallel().ForAll(i => {
            if (Math.Abs(i) > Math.Abs(maxByAbs)) maxByAbs = i; 
        });

        int MaxIndexSum = 0;
        for (int i = 0; i < length; i++) {
            if (array[i] > 0) MaxIndexSum += i;
        }

        Console.WriteLine("Sum of negative elements: {0:F2}", negSum);
        Console.WriteLine("Min element: {0}", array.Min());
        Console.WriteLine("Index of max element: {0}", array.ToList<double>().IndexOf(array.Max()));
        Console.WriteLine("Max element by absolute value: {0}", maxByAbs);
        Console.WriteLine("Indexes sum of positive elements: {0}", MaxIndexSum);
        Console.WriteLine("Number of integers: {0}", array.Count<double>(i => double.IsInteger(i)));
    }
    private static double RandomDoubleBetween(Random random ,double min, double max)
    {
        return min + random.NextDouble() * (max - min);
    }
}