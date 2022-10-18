using Etkinlik.Core.IUnitOfWork;
using Etkinlik.Core.Repositories;
using Etkinlik.Core.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Etkinlik.Service.Services
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IGenericRepository<T> repository;
        private readonly IUnitOfWork unitOfWork;

        public Service(IGenericRepository<T> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<T> AddAsync(T entity)
        {
            await repository.AddAsync(entity);
            await unitOfWork.CommitAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await repository.AddRangeAsync(entities);
            await unitOfWork.CommitAsync();
            return entities;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await repository.AnyAsync(expression);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {

            return await repository.GetAll().ToListAsync();
        }
        
        public T GetByIdAsync(object id)
        {
            var hasProduct = repository.GetByIdAsync(id);
            if (hasProduct is null)
            {
                //throw new NotFoundException($"{typeof(T).Name}({id}) not found");
                throw new Exception();
            }
            return hasProduct;
        }       

        public async Task RemoveAsync(T entity)
        {
            repository.Remove(entity);
            await unitOfWork.CommitAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            repository.RemoveRange(entities);
            await unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            repository.Update(entity);
            await unitOfWork.CommitAsync();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return repository.Where(expression);
        }
    }
}
