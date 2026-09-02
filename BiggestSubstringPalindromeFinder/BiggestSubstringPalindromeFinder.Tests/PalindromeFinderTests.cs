namespace BiggestSubstringPalindromeFinder.Tests;

public class PalindromeFinderTests
{
    [Theory]
    [InlineData("", "")]
    [InlineData("a", "a")]
    [InlineData("ab", "a")]
    [InlineData("aaba", "aba")]
    [InlineData("abba", "abba")]
    [InlineData("forgeeksskeegfor", "geeksskeeg")]
    [InlineData("abcddcbaxyz", "abcddcba")]
    public void FindLongest_ReturnsExpectedPalindrome(string input, string expected)
    {
        var result = PalindromeFinder.FindLongest(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void FindLongest_ReturnsLeftmostPalindromeWhenLengthsAreEqual()
    {
        var result = PalindromeFinder.FindLongest("racecarlevel");

        Assert.Equal("racecar", result);
    }

    [Fact]
    public void FindLongest_HandlesMaximumInputLength()
    {
        var input = new string('a', 1_000_000);

        var result = PalindromeFinder.FindLongest(input);

        Assert.Equal(input.Length, result.Length);
        Assert.Equal(input, result);
    }

    [Fact]
    public void FindLongest_ThrowsForNullInput()
    {
        Assert.Throws<ArgumentNullException>(() => PalindromeFinder.FindLongest(null!));
    }
}
