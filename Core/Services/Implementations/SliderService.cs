using AngularMyApp.Core.Services.Interfaces;
using AngularMyApp.DataLayer.Entities.Account;
using AngularMyApp.DataLayer.Entities.Site;
using AngularMyApp.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularMyApp.Core.Services.Implementations
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
