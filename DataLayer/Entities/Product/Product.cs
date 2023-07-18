using ShoppingSiteApi.DataAccess.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSiteApi.DataAccess.Entities.Product
{
    public class Product:BasicEntity
    {
        #region properties

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string ProductName { get; set; }

        [Display(Name = "قیمت")]
        public int Price { get; set; }

        [Display(Name = "توضیحات کوتاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string ShortDescription { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; }

        [Display(Name = "نام تصویر")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string? ImageName { get; set; }

        [Display(Name = "موجود / به اتمام رسیده")]
        public bool IsExists { get; set; }

        [Display(Name = "ویژه")]
        public bool IsSpecial { get; set; }


        #endregion

        #region Relations

        public ICollection<ProductGallery>? ProductGalleries { get; set; }

        public ICollection<ProductVisit>? ProductVisits { get; set; }

        public ICollection<ProductSelectedCategory>? ProductSelectedCategories { get; set; }
        
        public ICollection<Comment>? Commetns{ get; set; }

        #endregion

    }
}
