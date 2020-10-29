using Microsoft.EntityFrameworkCore;
using Skinet.Core.Entities;
using Skinet.Core.Interfaces;
using Skinet.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext context;

        public GenericRepository(StoreContext context)
        {
            this.context = context;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await this.context.Set<T>().FindAsync(id);
        }

        public async Task<IList<T>> ListAllAsync()
        {
            return await this.context.Set<T>().ToListAsync();
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await this.ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IList<T>> ListAsync(ISpecification<T> spec)
        {
            return await this.ApplySpecification(spec).ToListAsync();
        }

        public Task<int> CountAsync(ISpecification<T> spec)
        {
            return this.ApplySpecification(spec).CountAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(this.context.Set<T>().AsQueryable(), spec);
        }
    }
}
