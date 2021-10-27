using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Framework.Domain.Repositories;
using Infrastructure.Data.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class

    {
        protected PgSqlDbContext _repositoryContext;

        public RepositoryBase(PgSqlDbContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges?
                _repositoryContext.Set<T>()
                    .AsNoTracking()
                : _repositoryContext.Set<T>();


        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            !trackChanges
                ? _repositoryContext.Set<T>()
                    .Where(expression)
                    .AsNoTracking()
                : _repositoryContext.Set<T>()
                    .Where(expression);
        
        
        public async Task CreateAsync(T entity) => await _repositoryContext.Set<T>().AddAsync(entity);
        public void Update(T entity) => _repositoryContext.Set<T>().Update(entity);
        public void Delete(T entity) => _repositoryContext.Set<T>().Remove(entity);
    }

}