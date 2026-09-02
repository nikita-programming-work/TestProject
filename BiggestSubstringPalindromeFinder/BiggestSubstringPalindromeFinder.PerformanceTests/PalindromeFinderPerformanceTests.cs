using System.Diagnostics;
using Xunit.Abstractions;

namespace BiggestSubstringPalindromeFinder.PerformanceTests;

public class PalindromeFinderPerformanceTests
{
    private readonly ITestOutputHelper output;

    public PalindromeFinderPerformanceTests(ITestOutputHelper output)
    {
        this.output = output;
    }

    [Fact]
    public void FindLongest_HandlesDeterministicLargeInputs()
    {
        var cases = PerformanceCaseFactory.CreateCases();
        var totalStopwatch = Stopwatch.StartNew();

        foreach (var testCase in cases)
        {
            var stopwatch = Stopwatch.StartNew();
            var actual = PalindromeFinder.FindLongest(testCase.Input);
            stopwatch.Stop();

            Assert.Equal(testCase.ExpectedPalindrome, actual);
            output.WriteLine(
                $"InputLength={testCase.Input.Length}; " +
                $"ExpectedPalindromeLength={testCase.ExpectedPalindrome.Length}; " +
                $"Elapsed={stopwatch.ElapsedMilliseconds} ms");
        }

        totalStopwatch.Stop();
        output.WriteLine($"TotalElapsed={totalStopwatch.ElapsedMilliseconds} ms");
    }
}
