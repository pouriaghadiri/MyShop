using ShoppingSiteApi.DataAccess.Entities.Account;
using ShoppingSiteApi.DataAccess.Entities.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSiteApi.Core.Services.Interfaces
{
    public interface ISliderService : IBaseCRUD<Slider>
    {
        public Task<List<Slider>> GetActiveSliders();
    }
}
