using System;
using Framework.Domain.Queries;

namespace Domain.Queries
{
    public class GetUserById : IDomainQuery
    {
        public Guid UserId { get; }

        public GetUserById(Guid userId)
        {
            UserId = userId;
        }
    }
}