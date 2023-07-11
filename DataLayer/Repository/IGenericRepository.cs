using ShoppingSiteApi.DataAccess.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSiteApi.DataAccess.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetEntitiesQuery();
        Task<TEntity?> GetEntitiesAsyncById(int id);
        Task AddEntity(TEntity entity);
        void DeleteEntity(TEntity entity);
        Task<int> DeleteEntityById(int id);
        void UpdateEntity(TEntity entity);
        Task SaveChenges();
    }
}
