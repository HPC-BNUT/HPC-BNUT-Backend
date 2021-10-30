using System.Threading;
using System.Threading.Tasks;
using ApplicationService.QueryHandlers;
using Infrastructure.DataTransferObjects;
using Infrastructure.Mapper;
using Infrastructure.Queries;
using MediatR;

namespace HPC_Endpoints.Handlers.Queries.User
{
    public class GetUserByIdQueryHandler: IRequestHandler<GetUserByEmailQuery, UserDto>
    {
        private readonly GetUserByEmailHandler _getUserByEmailHandler;
        private readonly IQueryMapper _queryMapper;
        private readonly IDtoMapper _dtoMapper;
        public GetUserByIdQueryHandler(GetUserByEmailHandler getUserByEmailHandler, IQueryMapper queryMapper, IDtoMapper dtoMapper)
        {
            _getUserByEmailHandler = getUserByEmailHandler;
            _queryMapper = queryMapper;
            _dtoMapper = dtoMapper;
        }
        
        public async Task<UserDto> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var domainRequest = _queryMapper.MapToGetUserByEmail(request);
            var res = await _getUserByEmailHandler.Handle(domainRequest);
            var dto = _dtoMapper.MapToUserDto(res);
            return dto;
        }
    }
}