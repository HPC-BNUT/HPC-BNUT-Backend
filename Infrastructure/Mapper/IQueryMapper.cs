using Domain.Queries;
using Infrastructure.Queries;

namespace Infrastructure.Mapper
{
    public interface IQueryMapper
    {
        GetUserByEmail MapToGetUserByEmail(GetUserByEmailQuery query);
    }
}