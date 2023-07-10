using ShoppingSiteApi.Core.DTOs.Products;
using ShoppingSiteApi.Core.Services.Interfaces;
using ShoppingSiteApi.Core.Utilities.Extentions.PageingExtentions;
using ShoppingSiteApi.Core.Utilities.Paging;
using ShoppingSiteApi.DataAccess.Entities.Account;
using ShoppingSiteApi.DataAccess.Entities.Product;
using ShoppingSiteApi.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSiteApi.Core.Services.Implementations
{
    public class ProductCategoryService : BaseCRUD<ProductCategory> , IProductCategoryService
    {
        private readonly IGenericRepository<ProductCategory> _productCatRepository;
        public ProductCategoryService(IGenericRepository<ProductCategory> repository):base(repository)
        {
            _productCatRepository = repository;
        }

       
    }
}
