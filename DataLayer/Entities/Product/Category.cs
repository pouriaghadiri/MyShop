using ShoppingSiteApi.DataAccess.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShoppingSiteApi.DataAccess.Entities.Product
{
    public class Category:BasicEntity
    {
        #region Properties

        public string Title { get; set; }

        public int? ParentId { get; set; }


        #endregion
        #region Relations

        [ForeignKey("ParentId")]
        [JsonIgnore] // ignore this property during serialization
        public Category CategoryDetail { get; set; }
        public ICollection<Category> ProductSelectedCategories { get; set; }

        #endregion
    }
}
