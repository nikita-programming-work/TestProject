namespace BiggestSubstringPalindromeFinder;

public static class PalindromeFinder
{
    public static string FindLongest(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        if (input.Length < 2)
        {
            return input;
        }

        var oddRadii = new int[input.Length];
        var evenRadii = new int[input.Length];
        var bestStart = 0;
        var bestLength = 1;

        FindOddPalindromes(input, oddRadii);
        FindEvenPalindromes(input, evenRadii);

        for (var center = 0; center < input.Length; center++)
        {
            var oddLength = oddRadii[center] * 2 - 1;
            var oddStart = center - oddRadii[center] + 1;
            UpdateBest(oddStart, oddLength, ref bestStart, ref bestLength);

            var evenLength = evenRadii[center] * 2;
            var evenStart = center - evenRadii[center];
            UpdateBest(evenStart, evenLength, ref bestStart, ref bestLength);
        }

        return input.Substring(bestStart, bestLength);
    }

    private static void FindOddPalindromes(string input, int[] radii)
    {
        var left = 0;
        var right = -1;

        for (var center = 0; center < input.Length; center++)
        {
            var radius = center > right
                ? 1
                : Math.Min(radii[left + right - center], right - center + 1);

            while (center - radius >= 0 &&
                   center + radius < input.Length &&
                   input[center - radius] == input[center + radius])
            {
                radius++;
            }

            radii[center] = radius;
            if (center + radius - 1 > right)
            {
                left = center - radius + 1;
                right = center + radius - 1;
            }
        }
    }

    private static void FindEvenPalindromes(string input, int[] radii)
    {
        var left = 0;
        var right = -1;

        for (var center = 0; center < input.Length; center++)
        {
            var radius = center > right
                ? 0
                : Math.Min(radii[left + right - center + 1], right - center + 1);

            while (center - radius - 1 >= 0 &&
                   center + radius < input.Length &&
                   input[center - radius - 1] == input[center + radius])
            {
                radius++;
            }

            radii[center] = radius;
            if (center + radius - 1 > right)
            {
                left = center - radius;
                right = center + radius - 1;
            }
        }
    }

    private static void UpdateBest(
        int start,
        int length,
        ref int bestStart,
        ref int bestLength)
    {
        if (length > bestLength || (length == bestLength && start < bestStart))
        {
            bestStart = start;
            bestLength = length;
        }
    }
}
