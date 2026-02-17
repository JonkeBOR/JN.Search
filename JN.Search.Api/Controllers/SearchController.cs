using System.ComponentModel.DataAnnotations;
using JN.Search.Application.Features.Search.MediatR;
using JN.Search.Application.Features.Search.Models.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JN.Search.Api.Controllers;

[ApiController]
[Route("search")]
public sealed class SearchController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<SearchServicesResponse>> Get([FromQuery] SearchRequest request, CancellationToken cancellationToken)
    {

        var limit = Math.Clamp(request.Limit ?? 20, 1, 100);

        var response = await _mediator.Send(
            new SearchServicesQuery(request.Name!, request.Lat!.Value, request.Lng!.Value, limit),
            cancellationToken);

        return Ok(response);
    }

    public sealed class SearchRequest
    {
        [FromQuery(Name = "name")]
        [Required]
        public string? Name { get; init; }

        [FromQuery(Name = "lat")]
        [Required]
        [Range(-90.0, 90.0)]
        public double? Lat { get; init; }

        [FromQuery(Name = "lng")]
        [Required]
        [Range(-180.0, 180.0)]
        public double? Lng { get; init; }

        [FromQuery(Name = "limit")]
        [Range(1, 100)]
        public int? Limit { get; init; }
    }
}
