using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;

namespace Inventory.Common.Behaviours.Extensions.Paging
{
    public static class PagedResultExtensions
    {
        public static PagedResult<T> GetPaged<T>(this IQueryable<T> query, int page, int pageSize)
        {
            return new PagedResult<T>(query.Count(), page, pageSize)
            {
                Results = query.Skip((page - 1) * pageSize)
                 .Take(pageSize)
                 .ToList()
            };
        }
        public static PagedResult<U> GetPaged<T, U>(this IQueryable<T> query, int page, int pageSize, IConfigurationProvider provider)
        {
            return new PagedResult<U>(query.Count(), page, pageSize)
            {
                Results = query.Skip((page - 1) * pageSize)
                 .Take(pageSize)
                 .ProjectTo<U>(provider)
                 .ToList()
            };
        }
    }
}