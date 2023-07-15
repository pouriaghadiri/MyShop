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
    public class ProductCategoryService : BaseCRUD<ProductSelectedCategory>, IProductCategoryService
    {
        private readonly IGenericRepository<ProductSelectedCategory> _productCatRepository;
        private readonly IGenericRepository<Product> _productRepository;
        public ProductCategoryService(IGenericRepository<ProductSelectedCategory> repository , IGenericRepository<Product> productRepository) : base(repository)
        {
            _productCatRepository = repository;
            _productRepository = productRepository;
        }

        public async Task<ProductSelectedCategory> Create(ProductCategoryDTO entity)
        {
            ProductSelectedCategory productCategory = new()
            {
                CategoryId = entity.CategoryId,
                ProductId = entity.ProductId,
            };
            var product = await _productRepository.GetEntitiesAsyncById(entity.ProductId);
            await _productCatRepository.AddEntity(productCategory);
            await _productCatRepository.SaveChenges();
            var createdProductCat = await _productCatRepository.GetEntitiesQuery().SingleOrDefaultAsync(x => x.ProductId == entity.ProductId && x.CategoryId == entity.CategoryId);
            if (createdProductCat != null)
            {
                product.ProductSelectedCategories.Add(createdProductCat);
            }
            return productCategory;
        }

        public async Task<ProductSelectedCategory> Update(ProductCategoryDTO entity)
        {
            var cat = await _productCatRepository.GetEntitiesQuery().SingleOrDefaultAsync(x => x.ProductId == entity.ProductId && x.CategoryId == entity.CategoryId);
            if (cat != null)
            {
                cat.ProductId = entity.ProductId;
                cat.CategoryId = entity.CategoryId;

                _productCatRepository.UpdateEntity(cat);
                await _productCatRepository.SaveChenges();
                return cat;
            }
            return null;

        }
    }
}
