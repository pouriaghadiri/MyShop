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
    public interface IProductService: IBaseCRUD<Product>
    {
        public Task<FilterProductsDTO> FilterProductsDTO(FilterProductsDTO filterProductsDTO);
        public Task<Product> Create(ProductDTO entity);
        public Task<Product> Update(ProductUpdateDTO entity);
        public Task<List<Product>> GetRelatedProducts(int productId);
    }
}
