namespace JN.Search.Application.Features.Search.Helpers;

public static class HaversineDistanceHelper
{
    public static double Km(double lat1, double lng1, double lat2, double lng2)
    {
        const double radiusKm = 6371.0;

        var dLat = DegreesToRadians(lat2 - lat1);
        var dLng = DegreesToRadians(lng2 - lng1);

        var a =
            Math.Pow(Math.Sin(dLat / 2), 2) +
            Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
            Math.Pow(Math.Sin(dLng / 2), 2);

        var c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));
        return radiusKm * c;
    }

    private static double DegreesToRadians(double degrees) => degrees * (Math.PI / 180.0);
}
