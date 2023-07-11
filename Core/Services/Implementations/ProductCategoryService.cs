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
    public class ProductCategoryService : BaseCRUD<ProductCategory>, IProductCategoryService
    {
        private readonly IGenericRepository<ProductCategory> _productCatRepository;
        public ProductCategoryService(IGenericRepository<ProductCategory> repository) : base(repository)
        {
            _productCatRepository = repository;
        }

        public async Task<ProductCategory> Create(ProductCategoryDTO entity)
        {
            ProductCategory productCategory = new()
            {
                Title = entity.Title,
                ParentId = entity.ParentId == 0 ? null : entity.ParentId,
                CreatedWhen = DateTime.Now,
                UpdatedWhen = DateTime.Now,
                IsDelete = false,
            };
            await _productCatRepository.AddEntity(productCategory);
            await _productCatRepository.SaveChenges();
            return productCategory;
        }

        public async Task<ProductCategory> Update(ProductCategoryUpdateDTO entity)
        {
            var cat = await _productCatRepository.GetEntitiesAsyncById(entity.Id);
            if (cat != null)
            {
                cat.Title = entity.Title;
                cat.ParentId = entity.ParentId;
                _productCatRepository.UpdateEntity(cat);
                await _productCatRepository.SaveChenges();
                return cat;
            }
            return null;

        }
    }
}
