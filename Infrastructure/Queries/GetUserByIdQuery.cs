using System;
using Domain.Entities;
using MediatR;

namespace HPC_Endpoints.Queries
{
    public class GetUserByIdQuery : IRequest<User>
    {
        public Guid UserId { get; }

        public GetUserByIdQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}