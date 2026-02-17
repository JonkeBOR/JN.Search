using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace JN.Search.Api.Models
{
    public sealed class SearchRequest
    {
        [FromQuery(Name = "name")]
        [Required]
        public string Name { get; init; }

        [FromQuery(Name = "lat")]
        [Required]
        [Range(-90.0, 90.0)]
        public double Lat { get; init; }

        [FromQuery(Name = "lng")]
        [Required]
        [Range(-180.0, 180.0)]
        public double Lng { get; init; }
    }
}
