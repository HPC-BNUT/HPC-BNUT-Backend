using System;
using System.Threading.Tasks;
using Infrastructure.Commands.User;
using Infrastructure.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HPC_Endpoints.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            var res = await _mediator.Send(command);
            return Ok(res);
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetUserById([FromQuery] GetUserByIdQuery query)
        {
            var res = await _mediator.Send(query);
            return Ok(res);
        }
    }
}