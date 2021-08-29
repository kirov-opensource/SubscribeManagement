using SubscribeManagement.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubscribeManagement.WebAPI.Extensions
{
    public static class PageSearchModelExtensions
    {
        public static PageCollection<T> Done<T, E>(this PageSearchModel<E> pageSearchModel, int totalCount, IEnumerable<T> datas)
        {
            return new PageCollection<T>
            {
                Page = pageSearchModel.Page,
                Collection = datas,
                PageSize = pageSearchModel.PageSize,
                TotalCount = totalCount
            };
        }
    }
}
