using AngularMyApp.Core.DTOs.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularMyApp.Core.Utilities.Paging
{
    public class Pager
    {
        public static BasePaging Build(int pageCount , int pageNumber , int take)
        {
            if(pageNumber <= 1) pageNumber = 1;
            BasePaging basePaging = new BasePaging()
            {
                Activepage = pageNumber,
                PageCount = pageCount,
                PageID = pageNumber,
                StartPage = pageNumber - 3 <= 1 ? 1 : pageNumber - 3,
                EndPage = pageNumber + 3 >= pageCount ? pageCount : pageNumber + 3,
                TakeEntity = take,
                SkipEntity = (pageNumber - 1) * take
            };
            return basePaging;
        }
    }
}
