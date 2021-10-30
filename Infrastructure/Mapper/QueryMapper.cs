using Domain.Queries;
using Domain.ValueObjects;
using Infrastructure.Queries;

namespace Infrastructure.Mapper
{
    public class QueryMapper : IQueryMapper
    {
        public GetUserByEmail MapToGetUserByEmail(GetUserByEmailQuery query)
        {
            return new GetUserByEmail(Email.FromString(query.Email));
        }
    }
}