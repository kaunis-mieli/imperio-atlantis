using FibonacciCalculator;
using System.Numerics;

namespace FibonacciCalculator.Tests;

public class FibonacciCalculatorTests
{
    [Fact]
    public async Task ShouldFailOnNegativeN()
    {
        // Arrange
        int n = -5;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async () => await Calculator.Calculate(n));
    }

    [Fact]
    public async Task ShouldFailOnLargeN()
    {
        // Arrange
        int n = 1_000_001;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async () => await Calculator.Calculate(n));
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(2, 1)]
    [InlineData(3, 2)]
    [InlineData(4, 3)]
    [InlineData(10, 55)]
    [InlineData(19, 4181)]
    [InlineData(83, 99194853094755497)]
    public async Task ShouldReturnValidResultForSampleN(int n, BigInteger expectedResult)
    {
        // Act 
        var actualResult = await Calculator.Calculate(n);

        // Assert
        Assert.Equal(expectedResult, actualResult);
    }
}