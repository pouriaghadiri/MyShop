using ShoppingSiteApi.Core.DTOs.Paging;
using ShoppingSiteApi.DataAccess.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSiteApi.Core.DTOs.Products
{
    public class FilterProductsDTO:BasePaging
    {

        public string? Title { get; set; }
        public int StartPrice { get; set; }
        public int EndPrice { get; set; }
        public List<Product>? Products { get; set; }

        public FilterProductsDTO SetPaging(BasePaging paging)
        {
            this.PageID = paging.PageID;
            this.PageCount = paging.PageCount;
            this.StartPage = paging.StartPage;
            this.EndPage = paging.EndPage;
            this.TakeEntity = paging.TakeEntity;
            this.SkipEntity = paging.SkipEntity;
            this.Activepage = paging.Activepage;
            return this;
        }

        public FilterProductsDTO SetProducts(List<Product> products)
        {
            this.Products = products;
            return this;
        }
    }
}
