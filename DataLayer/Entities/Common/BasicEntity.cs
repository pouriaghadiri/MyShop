using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSiteApi.DataAccess.Entities.Common
{
    public class BasicEntity
    {
        [Key]
        public int Id { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedWhen { get; set; }
        public DateTime UpdatedWhen { get; set;}
    }
}
