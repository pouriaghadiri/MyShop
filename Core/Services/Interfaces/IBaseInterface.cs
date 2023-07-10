using ShoppingSiteApi.DataAccess.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSiteApi.Core.Services.Interfaces
{
    public interface IBaseCRUD<T> where T : class
    {
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task DeleteByID(int entityID);
        Task<T> GetByID(int entityID);
        Task<List<T>> GetAll();
    }
}
