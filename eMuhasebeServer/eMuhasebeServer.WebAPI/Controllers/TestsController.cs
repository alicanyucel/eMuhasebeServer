using eMuhasebeServer.WebAPI.Abstractions;
using FluentEmail.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eMuhasebeServer.WebAPI.Controllers;

[AllowAnonymous]
public class TestsController : ApiController
{
    private readonly IFluentEmail _fluentEmail;
    public TestsController(IMediator mediator,IFluentEmail fluentEmail) : base(mediator)
    {
        _fluentEmail = fluentEmail;
    }
    [HttpGet]
    public async Task<IActionResult> SendTestEmail()
    {
        await _fluentEmail.To("yucelalican30@gmail.com").Subject("naber").Body("<h1> mail gonderme testi<h1>",true).SendAsync();
       return NoContent();
    }
}
