using AngularMyApp.DataLayer.Context;
using AngularMyApp.DataLayer.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularMyApp.DataLayer.Repository
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
            await dbSet.AddAsync(entity);
        }
        public void UpdateEntity(TEntity entity)
        {
            entity.UpdatedWhen = DateTime.Now;
            dbSet.Update(entity);
        }
        public async Task<TEntity> GetEntitiesAsyncById(int id)
        {
            return await dbSet.SingleAsync(x => x.Id == id);
        }

        public IQueryable<TEntity> GetEntitiesQuery()
        {
            return dbSet.AsQueryable();
        }
        public void DeleteEntity(TEntity entity)
        {
            entity.IsDelete = true;
            UpdateEntity(entity);
        }

        public async Task DeleteEntityById(int id)
        {
            var entity = await GetEntitiesAsyncById(id);
            DeleteEntity(entity);
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
