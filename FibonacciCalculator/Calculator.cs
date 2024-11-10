using System.Numerics;

namespace FibonacciCalculator;

public class Calculator
{
    /// <summary>
    /// Calculates n-th Fibonacci sequence member. It's O(log n) complexity. 
    /// </summary>
    /// <param name="n">No. of Fibonacci sequence member.</param>
    /// <returns>Calculated Fibonacci sequence member.</returns>
    public static async Task<BigInteger> Calculate(int n)
    {
        if (n < 0) throw new ArgumentException("Argument n cannot be negative.");
        if (n > 200_000) throw new ArgumentException("Argument n must be 200'000 or less.");
        if (n == 0) return 0;
        if (n == 1) return 1;

        BigInteger[,] matrix = { { 1, 1 }, { 1, 0 } };
        BigInteger[,] result = await Task.Run(() => MatrixPower(matrix, n - 1)).ConfigureAwait(false);

        return result[0, 0];
    }

    private static BigInteger[,] MatrixPower(BigInteger[,] matrix, int power)
    {
        BigInteger[,] result = { { 1, 0 }, { 0, 1 } };
        BigInteger[,] baseMatrix = matrix;

        while (power > 0)
        {
            if ((power & 1) == 1)
                result = MultiplyMatrices(result, baseMatrix);

            baseMatrix = MultiplyMatrices(baseMatrix, baseMatrix);
            power >>= 1;
        }

        return result;
    }

    private static BigInteger[,] MultiplyMatrices(BigInteger[,] a, BigInteger[,] b) => new BigInteger[,]
        {
            {
                a[0, 0] * b[0, 0] + a[0, 1] * b[1, 0],
                a[0, 0] * b[0, 1] + a[0, 1] * b[1, 1]
            },
            {
                a[1, 0] * b[0, 0] + a[1, 1] * b[1, 0],
                a[1, 0] * b[0, 1] + a[1, 1] * b[1, 1]
            }
        };
}
