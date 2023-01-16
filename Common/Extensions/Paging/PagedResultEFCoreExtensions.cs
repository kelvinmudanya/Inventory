using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Common.Behaviours.Extensions.Paging
{
    public static class PagedResultEfCoreExtensions
    {
        public static async Task<PagedResult<T>> GetPagedAsync<T>(this IQueryable<T> query, int page, int pageSize)
        {
            return new PagedResult<T>(await query.CountAsync(), page, pageSize)
            {
                Results = await query.Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync()
            };
        }

        public static async Task<PagedResult<T>> GetPagedAsync<T>(this IOrderedQueryable<T> query, int page, int pageSize = 10)
        {
            return new PagedResult<T>(await query.CountAsync(), page, pageSize)
            {
                Results = await query.Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync()
            };
        }

        public static async Task<PagedResult<TU>> GetPagedAsync<T, TU>(this IQueryable<T> query, IConfigurationProvider provider, int page, int pageSize = 10) where TU : class
        {
            return new PagedResult<TU>(await query.CountAsync(), page, pageSize)
            {
                Results = await query.Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ProjectTo<TU>(provider)
                    .ToListAsync()
            };
        }

        public static async Task<PagedResult<TU>> GetPagedAsync<T, TU>(this IOrderedQueryable<T> query, IConfigurationProvider provider, int page, int pageSize = 10) where TU : class
        {
            return new PagedResult<TU>(await query.CountAsync(), page, pageSize)
            {
                Results = await query.Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ProjectTo<TU>(provider)
                    .ToListAsync()
            };
        }
    }
}