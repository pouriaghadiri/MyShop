using AngularMyApp.Core.DTOs.Products;
using AngularMyApp.Core.Services.Interfaces;
using AngularMyApp.Core.Utilities.Extentions.PageingExtentions;
using AngularMyApp.Core.Utilities.Paging;
using AngularMyApp.DataLayer.Entities.Account;
using AngularMyApp.DataLayer.Entities.Product;
using AngularMyApp.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularMyApp.Core.Services.Implementations
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
