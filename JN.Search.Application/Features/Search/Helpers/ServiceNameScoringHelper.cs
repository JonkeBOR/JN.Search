namespace JN.Search.Application.Features.Search.Helpers;

public static class ServiceNameScoringHelper
{
    public static int Score(string normalizedQuery, string normalizedCandidate)
    {
        if (!HasRequiredInput(normalizedQuery, normalizedCandidate))
        {
            return 0;
        }

        if (IsExactMatch(normalizedQuery, normalizedCandidate))
        {
            return 100;
        }

        var baseScore = GetBaseScore(normalizedQuery, normalizedCandidate);
        var tokenBonus = GetTokenOverlapBonus(normalizedQuery, normalizedCandidate);
        var similarityBonus = GetSingleTokenSimilarityBonus(normalizedQuery, normalizedCandidate);

        return Math.Clamp(baseScore + tokenBonus + similarityBonus, 0, 100);
    }

    private static bool HasRequiredInput(string normalizedQuery, string normalizedCandidate)
    {
        return !string.IsNullOrWhiteSpace(normalizedQuery) && !string.IsNullOrWhiteSpace(normalizedCandidate);
    }

    private static bool IsExactMatch(string normalizedQuery, string normalizedCandidate)
    {
        return string.Equals(normalizedQuery, normalizedCandidate, StringComparison.Ordinal);
    }

    private static int GetBaseScore(string normalizedQuery, string normalizedCandidate)
    {
        if (normalizedCandidate.StartsWith(normalizedQuery, StringComparison.Ordinal))
        {
            return 80;
        }

        if (normalizedCandidate.Contains(normalizedQuery, StringComparison.Ordinal))
        {
            return 60;
        }

        return 0;
    }

    private static int GetTokenOverlapBonus(string normalizedQuery, string normalizedCandidate)
    {
        var queryTokens = StringNormalizer.Tokenize(normalizedQuery);
        var candidateTokens = StringNormalizer.Tokenize(normalizedCandidate);

        if (queryTokens.Count == 0 || candidateTokens.Count == 0)
        {
            return 0;
        }

        var shared = queryTokens.Count(q => candidateTokens.Contains(q, StringComparer.Ordinal));
        return shared * 10;
    }

    private static int GetSingleTokenSimilarityBonus(string normalizedQuery, string normalizedCandidate)
    {
        var queryTokenCount = StringNormalizer.Tokenize(normalizedQuery).Count;
        var candidateTokenCount = StringNormalizer.Tokenize(normalizedCandidate).Count;

        if (queryTokenCount != 1 || candidateTokenCount != 1)
        {
            return 0;
        }

        return PrefixSimilarityBonus(normalizedQuery, normalizedCandidate);
    }

    private static int PrefixSimilarityBonus(string a, string b)
    {
        var max = Math.Min(a.Length, b.Length);
        var i = 0;

        while (i < max && a[i] == b[i])
        {
            i++;
        }

        if (i == 0)
        {
            return 0;
        }

        return i >= 4 ? 5 : 2;
    }
}
