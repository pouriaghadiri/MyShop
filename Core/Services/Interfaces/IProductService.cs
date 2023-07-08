using AngularMyApp.Core.DTOs.Products;
using AngularMyApp.DataLayer.Entities.Account;
using AngularMyApp.DataLayer.Entities.Product;
using AngularMyApp.DataLayer.Entities.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularMyApp.Core.Services.Interfaces
{
    public interface IProductService: IBaseCRUD<Product>
    {
        public Task<FilterProductsDTO> FilterProductsDTO(FilterProductsDTO filterProductsDTO);
    }
}
