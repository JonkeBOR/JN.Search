using System.Globalization;
using System.Text;

namespace JN.Search.Application.Features.Search.Helpers;

public static class StringNormalizer
{
    public static string Normalize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        var lower = value.Trim().ToLowerInvariant();
        var collapsed = CollapseWhitespace(lower);
        return RemoveDiacritics(collapsed);
    }

    public static IReadOnlyList<string> Tokenize(string normalized)
    {
        if (string.IsNullOrWhiteSpace(normalized))
        {
            return [];
        }

        return normalized
            .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    }

    private static string CollapseWhitespace(string value)
    {
        var sb = new StringBuilder(value.Length);
        var previousWasWhitespace = false;

        foreach (var ch in value)
        {
            var isWhitespace = char.IsWhiteSpace(ch);

            if (isWhitespace)
            {
                if (!previousWasWhitespace)
                {
                    sb.Append(' ');
                }

                previousWasWhitespace = true;
                continue;
            }

            sb.Append(ch);
            previousWasWhitespace = false;
        }

        return sb.ToString();
    }

    private static string RemoveDiacritics(string value)
    {
        var normalized = value.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder(normalized.Length);

        foreach (var ch in normalized)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
            {
                sb.Append(ch);
            }
        }

        return sb.ToString().Normalize(NormalizationForm.FormC);
    }
}
