using ShoppingSiteApi.DataAccess.Context;
using ShoppingSiteApi.DataAccess.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSiteApi.DataAccess.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BasicEntity
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> dbSet;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            dbSet = _context.Set<TEntity>();
        }
        public async Task AddEntity(TEntity entity)
        {
            entity.CreatedWhen = DateTime.Now;
            entity.UpdatedWhen = entity.CreatedWhen;
            entity.IsDelete = false;

            await dbSet.AddAsync(entity);
        }
        public void UpdateEntity(TEntity entity)
        {
            entity.UpdatedWhen = DateTime.Now;
            dbSet.Update(entity);
        }
        public async Task<TEntity> GetEntitiesAsyncById(int id)
        {
            var a = await dbSet.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (a != null)
            {
                return await dbSet.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            }
            return null;
        }

        public IQueryable<TEntity> GetEntitiesQuery()
        {
            return dbSet.AsQueryable();
        }
        public void DeleteEntity(TEntity entity)
        {
            if (entity != null)
            {
                entity.IsDelete = true;
                UpdateEntity(entity);
            }
        }

        public async Task<int> DeleteEntityById(int id)
        {
            var entity = await GetEntitiesAsyncById(id);
            if (entity != null)
            {
                DeleteEntity(entity);
                return entity.Id;
            }
            return 0;
        }

        public async Task SaveChenges()
        {
            try
            {
                var temp = await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
