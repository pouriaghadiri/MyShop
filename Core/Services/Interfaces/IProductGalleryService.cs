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
    public interface IProductGalleryService : IBaseCRUD<ProductGallery>
    {
        //public Task<ProductGallery> Create(ProductGallery entity);
        //public Task<ProductGallery> Update(ProductGallery entity);
        public Task<List<ProductGallery>> GetActiveProductGallery(int id);
    }
}
