using System;
using Domain.Entities;
using Infrastructure.DataTransferObjects;
using MediatR;

namespace Infrastructure.Queries
{
    public class GetUserByEmailQuery : IRequest<UserDto>
    {
        public string Email { get; }

        public GetUserByEmailQuery(string email)
        {
            Email = email;
        }
    }
}