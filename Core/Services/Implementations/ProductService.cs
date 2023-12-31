﻿using ShoppingSiteApi.Core.DTOs.Products;
using ShoppingSiteApi.Core.Services.Interfaces;
using ShoppingSiteApi.Core.Utilities.Extentions.PageingExtentions;
using ShoppingSiteApi.Core.Utilities.Paging;
using ShoppingSiteApi.DataAccess.Entities.Account;
using ShoppingSiteApi.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Http.HttpResults;
using ShoppingSiteApi.DataAccess.Entities.Products;

namespace ShoppingSiteApi.Core.Services.Implementations
{
    public class ProductService : BaseCRUD<Product> , IProductService
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductSelectedCategory> _productCategoryRepository;
        public ProductService(IGenericRepository<Product> repository , IGenericRepository<ProductSelectedCategory> productCategoryRepository) :base(repository)
        {
            _productRepository = repository;
            _productCategoryRepository = productCategoryRepository;
        }
        public override async Task<List<Product>> GetAll()
        {
            return await _productRepository.GetEntitiesQuery()
                                    .Where(x => x.IsDelete == false)
                                    .Include(x => x.ProductSelectedCategories)
                                    .ToListAsync();
        }
        public async Task<Product> Create(ProductDTO entity)
        {
            try
            {

                Product productDetail = new()
                {
                    ProductName = entity.ProductName,
                    Price = entity.Price,
                    ShortDescription = entity.ShortDescription,
                    Description = entity.Description,
                    IsSpecial = entity.IsSpecial,
                    IsExists = true,
                    IsDelete = false
                };
                await _productRepository.AddEntity(productDetail);
                await _productRepository.SaveChenges();
                return productDetail;
            }
            catch (Exception ex )
            {
                return null;
            }
        }

        public async Task<Product> Update(ProductUpdateDTO entity)
        {
            var cat = await _productRepository.GetEntitiesAsyncById(entity.Id);
            if (cat != null)
            {
                cat.ProductName = entity.ProductName;
                cat.Price = entity.Price;
                cat.ShortDescription = entity.ShortDescription;
                cat.Description = entity.Description;
                cat.IsSpecial = entity.IsSpecial;
                cat.IsExists = entity.IsExists;

                _productRepository.UpdateEntity(cat);
                await _productRepository.SaveChenges();
                return cat;
            }
            return null;

        }

        public async Task<FilterProductsDTO> FilterProductsDTO(FilterProductsDTO filter)
        {
            var productsQuery = _productRepository.GetEntitiesQuery().AsQueryable()
                ;

            if (!string.IsNullOrEmpty(filter.Title))
                productsQuery = productsQuery.Where(s => s.ProductName.Contains(filter.Title));

            productsQuery = productsQuery.Where(s => s.Price >= filter.StartPrice );
            if (filter.EndPrice != 0)
            {
                productsQuery = productsQuery.Where(s => s.Price <= filter.EndPrice);
            }

            productsQuery = productsQuery.Include(x => x.ProductSelectedCategories);

            var count = (int)Math.Ceiling(productsQuery.Count() / (double)filter.TakeEntity); // بدست آوردن تعداد کل صفحات

            var pager = Pager.Build(count, filter.PageID, filter.TakeEntity);

            var products = await productsQuery.Paging(pager).ToListAsync();

            return filter.SetProducts(products).SetPaging(pager);
        }

        public async Task<List<Product>> GetRelatedProducts(int productId)
        {
            var currentproduct = await _productRepository.GetEntitiesAsyncById(productId);
            
            var currentCats = await _productCategoryRepository.GetEntitiesQuery()
                                                        .Where(x => x.ProductId == productId)
                                                        .Select(s => s.CategoryId)
                                                        .ToListAsync();
            
            var relatedproducts = await _productRepository.GetEntitiesQuery()
                                                            .SelectMany(s => s.ProductSelectedCategories.Where(x => currentCats.Contains(x.CategoryId))
                                                             .Select(s => s.Product))
                                                            .Where( x => x.Id !=  productId)
                                                            .OrderByDescending(o => o.CreatedWhen)
                                                            .Take(4)
                                                            .Distinct()
                                                            .ToListAsync();
            return relatedproducts;
        }
    }
}
