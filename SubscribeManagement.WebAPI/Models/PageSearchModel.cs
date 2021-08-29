using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubscribeManagement.WebAPI.Models
{
    public class PageSearchModel<T>
    {
        public int PageSize { get; set; } = 10;
        public int Page { get; set; } = 1;
        public T Search { get; set; }
        public int SkipCount
        {
            get
            {
                return (Page - 1) * PageSize;
            }
        }
    }
}
