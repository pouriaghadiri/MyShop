using ShoppingSiteApi.Core.Services.Interfaces;
using ShoppingSiteApi.DataAccess.Entities.Account;
using ShoppingSiteApi.DataAccess.Entities.Site;
using ShoppingSiteApi.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSiteApi.Core.Services.Implementations
{
    public class SliderService : BaseCRUD<Slider> , ISliderService
    {
        private readonly IGenericRepository<Slider> _repository;
        public SliderService(IGenericRepository<Slider> repository):base(repository)
        {
            _repository = repository;
        }


        public async Task<List<Slider>> GetActiveSliders()
        {
            return await _repository.GetEntitiesQuery().Where(x => x.IsDelete ==  false).ToListAsync();
        }

    }
}
