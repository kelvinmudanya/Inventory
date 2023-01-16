using AutoMapper;
using CSharpFunctionalExtensions;
using Inventory.Common.Behaviours.Extensions.Paging;
using Inventory.Data;
using Inventory.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Features.Categories
{
    public class Details
    {
        public class Query : IRequest<Result<CategoryViewModel>>
        {
            public Query(int? page, string s, int categoryId)
            {
                Page = page;
                Q = s;
                CategoryId = categoryId;
            }

            public int? Page { get; set; }
            public string Q { get; set; }
            public int CategoryId { get; set; }
        }
        public class CategoryViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public PagedResult<ItemsViewModel> Children { get; set; } = new PagedResult<ItemsViewModel>(0);
        }


        public class ItemsViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal SellingPrice { get; set; }
        }


        public class QueryHandler : IRequestHandler<Query, Result<CategoryViewModel>>
        {
            private readonly IConfigurationProvider provider;
            public readonly MainContext context;


            public QueryHandler(IConfigurationProvider provider, MainContext context)
            {
                this.provider = provider;
                this.context = context;
            }

            public async Task<Result<CategoryViewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var pageNumber = request.Page ?? 1;
                var q = request.Q?.ToLower();

                var category = await context.Categories
                    .AsNoTracking().Include(x => x.Items)
                    .FirstOrDefaultAsync(x => x.Id == request.CategoryId);

                var results = !string.IsNullOrWhiteSpace(q)
                    ? category.Items
                        .Where(x => x.EntityGuid.ToString().Contains(q))
                    : category.Items.OrderBy(x => x.Id);

                if (category is not null)
                {
                    PagedResult<ItemsViewModel> pagedResult = results.AsQueryable().GetPaged<Item, ItemsViewModel>(pageNumber, 25, provider);
                    return Result.Success(new CategoryViewModel
                    {
                        Id = category.Id,
                        Name = category.Name,
                        Description = category.Description,
                        Children = pagedResult,

                    });
                }

                else
                {
                    return Result.Failure<CategoryViewModel>("No Data Found");
                }

            }
        }
    }
}
