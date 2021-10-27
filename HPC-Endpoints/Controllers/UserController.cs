using System;
using System.Threading.Tasks;
using HPC_Endpoints.Queries;
using Infrastructure.Commands.User;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        {
            var res = await _mediator.Send(new GetUserByIdQuery(id));
            return Ok(res);
        }
    }
}