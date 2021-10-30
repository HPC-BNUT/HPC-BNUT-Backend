using System;
using Domain.ValueObjects;
using Framework.Domain.Queries;

namespace Domain.Queries
{
    public class GetUserByEmail : IDomainQuery
    {
        public Email Email { get; }

        public GetUserByEmail(Email email)
        {
            Email = email;
        }
    }
}