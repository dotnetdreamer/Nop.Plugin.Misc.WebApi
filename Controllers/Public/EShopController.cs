using Nop.Core;
using Nop.Core.Caching;
using Nop.Plugin.Misc.WebApi.Models;
using Nop.Plugin.Widgets.NivoSlider;
using Nop.Plugin.Widgets.NivoSlider.Infrastructure.Cache;
using Nop.Services.Configuration;
using Nop.Services.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Nop.Plugin.Misc.WebApi.Controllers.Public
{
    public class EShopController : ApiController
    {
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;
        private readonly ICacheManager _cacheManager;
        private readonly IPictureService _pictureService;

        public EShopController(ISettingService settingService, ICacheManager cacheManager, 
            IPictureService pictureService, IStoreContext storeContext)
        {
            this._settingService = settingService;
            this._cacheManager = cacheManager;
            this._pictureService = pictureService;
            this._storeContext = storeContext;
        }

        public IHttpActionResult Get()
        {
            return Ok("Works");
        }


        public IHttpActionResult GetNivoSliderSlides()
        {
            try
            {
                var slidesList = new List<NivoSliderResult>();
                var nivoSliderSettings = _settingService.LoadSetting<NivoSliderSettings>(_storeContext.CurrentStore.Id);

                var model = new NivoSliderResult();
                model.PictureUrl = GetPictureUrl(nivoSliderSettings.MobilePicture1Id);
                if (!string.IsNullOrEmpty(model.PictureUrl))
                {
                    model.Text = nivoSliderSettings.Text1;
                    model.Link = nivoSliderSettings.Link1;
                    slidesList.Add(model);
                }

                model = new NivoSliderResult();
                model.PictureUrl = GetPictureUrl(nivoSliderSettings.MobilePicture2Id);
                if (!string.IsNullOrEmpty(model.PictureUrl))
                {
                    model.Text = nivoSliderSettings.Text2;
                    model.Link = nivoSliderSettings.Link2;
                    slidesList.Add(model);
                }

                model = new NivoSliderResult();
                model.PictureUrl = GetPictureUrl(nivoSliderSettings.MobilePicture3Id);
                if (!string.IsNullOrEmpty(model.PictureUrl))
                {
                    model.Text = nivoSliderSettings.Text3;
                    model.Link = nivoSliderSettings.Link3;
                    slidesList.Add(model);
                }

                model = new NivoSliderResult();
                model.PictureUrl = GetPictureUrl(nivoSliderSettings.MobilePicture4Id);
                if (!string.IsNullOrEmpty(model.PictureUrl))
                {
                    model.Text = nivoSliderSettings.Text4;
                    model.Link = nivoSliderSettings.Link4;
                    slidesList.Add(model);
                }

                model = new NivoSliderResult();
                model.PictureUrl = GetPictureUrl(nivoSliderSettings.MobilePicture5Id);
                if (!string.IsNullOrEmpty(model.PictureUrl))
                {
                    model.Text = nivoSliderSettings.Text5;
                    model.Link = nivoSliderSettings.Link5;
                    slidesList.Add(model);
                }

                var result = slidesList;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }


        #region Helpers

        protected string GetPictureUrl(int pictureId)
        {
            string cacheKey = string.Format(ModelCacheEventConsumer.PICTURE_URL_MODEL_KEY, pictureId);
            return _cacheManager.Get(cacheKey, () =>
            {
                var url = _pictureService.GetPictureUrl(pictureId, showDefaultPicture: false, 
                    storeLocation: _storeContext.CurrentStore.Url);
                //little hack here. nulls aren't cacheable so set it to ""
                if (url == null)
                    url = "";

                return url;
            });
        }

        #endregion  
    }
}
