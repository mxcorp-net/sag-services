using MediatR;
using Microsoft.AspNetCore.Mvc;
using sag.api.Attributes;
using sag.Application.Features.Institutions.Commands;
using sag.Application.Features.Institutions.Models;
using sag.Application.Features.Institutions.Queries;

namespace sag.api.Controllers;

[AuthGuard]
[ApiController, Route("api/institutions")]
public class InstitutionsController : Controller
{
    private readonly IMediator _mediator;

    public InstitutionsController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Get all active Institutions filter by Type
    /// </summary>
    /// <param name="filters"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetInstitutions([FromQuery] InstitutionFilters filters) =>
        Ok(await _mediator.Send(new GetInstitutionsQuery(filters)));

    /// <summary> 
    /// Add new Institution
    /// </summary>
    /// <param name="institution"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> AddInstitution([FromBody] InstitutionRequest institution) =>
        Ok(await _mediator.Send(new AddInstitutionCommand(institution)));
}


// url.com/api/institutions?type=1