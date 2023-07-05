using AngularMyApp.Core.Services.Interfaces;
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
    public class ProductService : BaseCRUD<Product> , IProductService
    {
        private readonly IGenericRepository<Product> _repository;
        public ProductService(IGenericRepository<Product> repository):base(repository)
        {
            _repository = repository;
        }

    }
}
