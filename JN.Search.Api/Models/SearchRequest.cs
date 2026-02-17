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
        public double Lat { get; init; }

        [FromQuery(Name = "lng")]
        [Required]
        public double Lng { get; init; }
    }
}
