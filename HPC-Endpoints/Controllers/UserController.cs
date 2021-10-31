using System.Threading.Tasks;
using ApplicationService._Shared.Services;
using Infrastructure.Commands.User;
using Infrastructure.DataTransferObjects;
using Infrastructure.Queries;
using Infrastructure.StandardResult;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HPC_Endpoints.Controllers
{
    [ApiController]
    [ApiResultFilter]
    [Route("api/users/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ApiResult<PairToken>> Register([FromBody] RegisterUserCommand command)
            => await _mediator.Send(command);
        
        [HttpGet, Authorize]
        public async Task<ApiResult<UserDto>> GetUserInfo()
            => await _mediator.Send(new GetUserByEmailQuery(HttpContext.User.Identity.Name));
        
        [HttpPost]
        public async Task<ApiResult<PairToken>> Login([FromBody] LoginUserCommand command)
            => await _mediator.Send(command);
        
        [HttpPost]
        public async Task<ApiResult<PairToken>> Refresh([FromBody] RefreshUserCommand command)
            => await _mediator.Send(command);
    }
}