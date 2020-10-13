using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Api.Models
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext appDbContext;
        private readonly DbSet<TEntity> entities;

        public GenericRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
            this.entities = appDbContext.Set<TEntity>();
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            var result = await entities.AddAsync(entity);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<TEntity> Delete(int Id)
        {
            var result = await entities.FindAsync(Id);
            if (result != null)
            {
                entities.Remove(result);
                await appDbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await entities.ToListAsync();
        }

        public async Task<TEntity> GetById(int Id)
        {
            return await entities.FindAsync(Id);
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            appDbContext.Set<TEntity>().Update(entity);
            await appDbContext.SaveChangesAsync();

            return entity;
        }
    }
}
