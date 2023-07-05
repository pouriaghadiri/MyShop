using AngularMyApp.Core.Services.Interfaces;
using AngularMyApp.DataLayer.Entities.Common;
using AngularMyApp.DataLayer.Entities.Site;
using AngularMyApp.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularMyApp.Core.Services.Implementations
{
    public class BaseCRUD<T> : IBaseCRUD<T> where T : BasicEntity
    {
        private readonly IGenericRepository<T> _repository;
        public BaseCRUD(IGenericRepository<T> repository)
        {
            _repository = repository;
        }
        public async Task Create(T entity)
        {
            await _repository.AddEntity(entity);
            await _repository.SaveChenges();
        }

        public async Task Delete(T entity)
        {
            _repository.DeleteEntity(entity);
            await _repository.SaveChenges();
        }

        public async Task DeleteByID(int entityID)
        {
            await _repository.DeleteEntityById(entityID);
            await _repository.SaveChenges();
        }

        public async Task Update(T entity)
        {
            _repository.UpdateEntity(entity);
            await _repository.SaveChenges();
        }

        public async Task<List<T>> GetAll()
        {
            return await _repository.GetEntitiesQuery().Where(x => x.IsDelete == false).ToListAsync();
        }

        public async Task<T> GetByID(int entityID)
        {
            return await _repository.GetEntitiesAsyncById(entityID);
        }
    }
}
