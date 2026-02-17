namespace JN.Search.Application.Features.Search.Helpers;

public static class DistanceStringHelper
{
    public static string Format(double distanceKm)
    {
        if (distanceKm < 1.0)
        {
            var meters = (int)Math.Round(distanceKm * 1000.0, MidpointRounding.AwayFromZero);
            return $"{meters}m";
        }

        return $"{distanceKm:0.00}km";
    }
}
