using JN.Search.Application.Features.Search.MediatR;
using static JN.Search.Application.UnitTests.Features.Search.Mediatr.SearchServicesQueryTestHarness;

namespace JN.Search.Application.UnitTests.Features.Search.Mediatr;

public class SearchServicesQuery_Ctor
{
    [Fact]
    public void GivenValidInput_ReturnsObject()
    { 
        var sut = new SearchServicesQuery(DefaultSearchParameterName, DefaultSearchParameterLatitude, DefaultSearchParameterLongitude);

        Assert.True(sut.Name == DefaultSearchParameterName && sut.Lat == DefaultSearchParameterLatitude && sut.Lng == DefaultSearchParameterLongitude);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void GivenNullorWhiteSpaceName_ThrowsException(string? name)
    {
        var act = () => new SearchServicesQuery(name!, DefaultSearchParameterLongitude, DefaultSearchParameterLongitude);
        Assert.Throws<ArgumentException>(act);
    }

    [Theory]
    [InlineData(-90.0001)]
    [InlineData(90.0001)]
    public void GivenOutofRangeLat_ThrowsException(double lat)
    {
        var act = () => new SearchServicesQuery(DefaultSearchParameterName, lat, DefaultSearchParameterLongitude);
        Assert.Throws<ArgumentOutOfRangeException>(act);
    }

    [Theory]
    [InlineData(-180.0001)]
    [InlineData(180.0001)]
    public void GivenOutofRangeLng_ThrowsException(double lng)
    {
        var act = () => new SearchServicesQuery(DefaultSearchParameterName, DefaultSearchParameterLatitude, lng);
        Assert.Throws<ArgumentOutOfRangeException>(act);
    }

    [Theory]
    [InlineData(-90.0)]
    [InlineData(0.0)]
    [InlineData(90.0)]
    public void GivenValidLat_ReturnsObjectWithoutExceptions(double lat)
    {
        var act = () => new SearchServicesQuery(DefaultSearchParameterName, lat, DefaultSearchParameterLongitude);
        Assert.Null(Record.Exception(act));
    }

    [Theory]
    [InlineData(-180.0)]
    [InlineData(0.0)]
    [InlineData(180.0)]
    public void GivenValidLng_ReturnsObjectWithoutExceptions(double lng)
    {
        var act = () => new SearchServicesQuery(DefaultSearchParameterName, DefaultSearchParameterLatitude, lng);
        Assert.Null(Record.Exception(act));
    }
}

public static class SearchServicesQueryTestHarness
{
    public const string DefaultSearchParameterName = "massage";
    public const double DefaultSearchParameterLatitude = 59.0;
    public const double DefaultSearchParameterLongitude = 18.0;
}

