using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using carzz.Core.Models;

namespace carzz.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query,ISortingObjects filterObj,Dictionary<string,Expression<Func<T,object>>> columnsMap)
        {
            if (String.IsNullOrWhiteSpace(filterObj.SortBy) ||!columnsMap.ContainsKey(filterObj.SortBy))
            {
                return query;
            }
            if (filterObj.IsSortAscending)
            {
                return query.OrderBy(columnsMap[filterObj.SortBy]);
            }
            else
            {
                return query.OrderByDescending(columnsMap[filterObj.SortBy]);
            }
        }
    }
}
