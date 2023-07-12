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
    public class CategoryService : BaseCRUD<Category>, ICategoryService
    {
        private readonly IGenericRepository<Category> _productCatRepository;
        public CategoryService(IGenericRepository<Category> repository) : base(repository)
        {
            _productCatRepository = repository;
        }

        public async Task<Category> Create(CategoryDTO entity)
        {
            Category productCategory = new()
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

        public async Task<Category> Update(CategoryUpdateDTO entity)
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
