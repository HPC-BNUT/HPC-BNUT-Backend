using System;
using Domain.Entities;
using MediatR;

namespace Infrastructure.Queries
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