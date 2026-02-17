using System.ComponentModel.DataAnnotations;
using JN.Search.Api.Models;
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
        var query = new SearchServicesQuery(request.Name, request.Lat, request.Lng);
        var response = await _mediator.Send(query,cancellationToken);
        return Ok(response);
    }
}
