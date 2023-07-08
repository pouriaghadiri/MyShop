using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularMyApp.Core.DTOs.Paging
{
    public class BasePaging
    {
        public int PageID { get; set; } = 1;
        public int PageCount { get; set; }
        public int Activepage { get; set; } = 1;
        public int StartPage { get; set; } = 1;
        public int EndPage { get; set; }
        public int TakeEntity { get; set; } = 10; // مجموع رکورد های یک صفحه
        public int SkipEntity { get; set; } // مجموع رک.رد هایی که باید از آنها رد شود تا به رکورد های صفحه فعال برسد


    }
}
