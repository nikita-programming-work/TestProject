using System.Text;

namespace BiggestSubstringPalindromeFinder.PerformanceTests;

public sealed record PerformanceCase(string Input, string ExpectedPalindrome);

public static class PerformanceCaseFactory
{
    private const int CaseCount = 8;
    private const int MinimumInputLength = 7_500_000;
    private const int MaximumInputLength = 10_000_000;
    private const int MinimumPalindromeLength = 2_000_000;
    private const int MaximumPalindromeLength = 5_000_000;
    private const int Seed = 20260902;
    private const string FillerAlphabet = "bcdefghijklmnopqrstuvwxyz";

    public static IReadOnlyList<PerformanceCase> CreateCases()
    {
        var random = new Random(Seed);
        var cases = new List<PerformanceCase>(CaseCount);

        for (var index = 0; index < CaseCount; index++)
        {
            var inputLength = random.Next(MinimumInputLength, MaximumInputLength + 1);
            var palindromeLength = random.Next(
                MinimumPalindromeLength,
                Math.Min(MaximumPalindromeLength, inputLength - 2) + 1);
            var fillerLength = inputLength - palindromeLength - 2;
            var prefixLength = random.Next(0, fillerLength + 1);
            var suffixLength = fillerLength - prefixLength;

            var inputBuilder = new StringBuilder(inputLength);
            AppendFiller(inputBuilder, prefixLength, random);
            inputBuilder.Append('b');
            inputBuilder.Append('a', palindromeLength);
            inputBuilder.Append('c');
            AppendFiller(inputBuilder, suffixLength, random);

            cases.Add(new PerformanceCase(
                inputBuilder.ToString(),
                new string('a', palindromeLength)));
        }

        return cases;
    }

    private static void AppendFiller(StringBuilder builder, int length, Random random)
    {
        for (var index = 0; index < length; index++)
        {
            builder.Append(FillerAlphabet[random.Next(FillerAlphabet.Length)]);
        }
    }
}
