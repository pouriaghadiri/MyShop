using ShoppingSiteApi.Core.DTOs.Products;
using ShoppingSiteApi.DataAccess.Entities.Account;
using ShoppingSiteApi.DataAccess.Entities.Product;
using ShoppingSiteApi.DataAccess.Entities.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSiteApi.Core.Services.Interfaces
{
    public interface IProductCategoryService : IBaseCRUD<ProductCategory>
    {
        public Task<ProductCategory> Create(ProductCategoryDTO entity);
        public Task<ProductCategory> Update(ProductCategoryUpdateDTO entity);
    }
}
