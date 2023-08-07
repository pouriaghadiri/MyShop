using ShoppingSiteApi.Core.DTOs.Products;
using ShoppingSiteApi.DataAccess.Entities.Account;
using ShoppingSiteApi.DataAccess.Entities.Products;
using ShoppingSiteApi.DataAccess.Entities.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSiteApi.Core.Services.Interfaces
{
    public interface ICategoryService : IBaseCRUD<Category>
    {
        public Task<Category> Create(CategoryDTO entity);
        public Task<Category> Update(CategoryUpdateDTO entity);
    }
}
