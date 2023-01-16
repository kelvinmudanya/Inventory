using AutoMapper;
using Inventory.Common.Behaviours.Extensions.Paging;
using Inventory.Data;
using Inventory.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Features.Categories
{
    public class List
    {
        public class Query : IRequest<PagedResult<CategoryViewModel>>
        {
            public Query(int? page, string s)
            {
                Page = page;
                Q = s;
            }

            public int? Page { get; set; }
            public string Q { get; set; }
        }

        public class CategoryViewModel
        {
            public int Id { get; set; }
            public Guid EntityGuid { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }


        }

        public class QueryHandler : IRequestHandler<Query, PagedResult<CategoryViewModel>>
        {
            private readonly IConfigurationProvider provider;
            public readonly MainContext context;



            public QueryHandler(IConfigurationProvider provider, MainContext context)


            {
                this.provider = provider;
                this.context = context;
            }

            public async Task<PagedResult<CategoryViewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var pageNumber = request.Page ?? 1;
                var q = request.Q?.ToLower().Trim();
                var model = context.Categories.AsNoTracking();

                var results = !string.IsNullOrWhiteSpace(q)
                    ? model
                        .Where(x => x.Name.ToLower() == q
                        || EF.Functions.Like(x.Name.ToLower(), $"%{q}%"))


                        .OrderByDescending(x => x.Id)
                    : model.OrderByDescending(x => x.Id);

                return await results.GetPagedAsync<Category, CategoryViewModel>(provider, pageNumber);

            }
        }
    }
}