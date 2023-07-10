using ShoppingSiteApi.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingSiteApi.WebAPI.Controllers
{
    public class SliderController : SiteBasicController
    {
        #region constructor

        private ISliderService sliderService;

        public SliderController(ISliderService sliderService)
        {
            this.sliderService = sliderService;
        }

        #endregion

        #region Slider Controlles

        [HttpGet("GetActiveSliders")]
        public async Task<IActionResult> GetActiveSliders()
        {
            var sliders = await sliderService.GetActiveSliders();

            return Ok(sliders.ToList());
        }

        #endregion
    }
}
