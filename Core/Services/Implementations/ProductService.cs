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
    public class ProductService : BaseCRUD<Product> , IProductService
    {
        private readonly IGenericRepository<Product> _productRepository;
        public ProductService(IGenericRepository<Product> repository):base(repository)
        {
            _productRepository = repository;
        }

        public async Task<FilterProductsDTO> FilterProductsDTO(FilterProductsDTO filter)
        {
            var productsQuery = _productRepository.GetEntitiesQuery().AsQueryable();

            if (!string.IsNullOrEmpty(filter.Title))
                productsQuery = productsQuery.Where(s => s.ProductName.Contains(filter.Title));

            productsQuery = productsQuery.Where(s => s.Price >= filter.StartPrice );
            if (filter.EndPrice != 0)
            {
                productsQuery = productsQuery.Where(s => s.Price <= filter.EndPrice);
            }

            var count = (int)Math.Ceiling(productsQuery.Count() / (double)filter.TakeEntity); // بدست آوردن تعداد کل صفحات

            var pager = Pager.Build(count, filter.PageID, filter.TakeEntity);

            var products = await productsQuery.Paging(pager).ToListAsync();

            return filter.SetProducts(products).SetPaging(pager);
        }
    }
}
