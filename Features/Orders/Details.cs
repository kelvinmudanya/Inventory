using AutoMapper;
using CSharpFunctionalExtensions;
using Inventory.Common.Behaviours.Extensions.Paging;
using Inventory.Data;
using Inventory.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Features.Orders
{
    public class Details
    {
        public class Query : IRequest<Result<OrderViewModel>>
        {
            public Query(int? page, string s, int orderId)
            {
                Page = page;
                Q = s;
                OrderId = orderId;
            }

            public int? Page { get; set; }
            public string Q { get; set; }
            public int OrderId { get; set; }
        }
        public class OrderViewModel
        {
            public int Id { get; set; }
            public Guid EntityGuid { get; set; }
            public string OrderNumber { get; set; }
            public string SupplierName { get; set; }
            public string SupplierLocation { get; set; }
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


        public class QueryHandler : IRequestHandler<Query, Result<OrderViewModel>>
        {
            private readonly IConfigurationProvider provider;
            public readonly MainContext context;


            public QueryHandler(IConfigurationProvider provider, MainContext context)
            {
                this.provider = provider;
                this.context = context;
            }

            public async Task<Result<OrderViewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                Console.WriteLine("Here 1");
                var pageNumber = request.Page ?? 1;
                var q = request.Q?.ToLower();
                Console.WriteLine("Here 2");

                var order = await context.Orders
                    .AsNoTracking().Include(x => x.Items).Include(x=>x.Supplier)
                    .FirstOrDefaultAsync(x => x.Id == request.OrderId);
                Console.WriteLine("Here 3");

                var results = !string.IsNullOrWhiteSpace(q)
                    ? order.Items
                        .Where(x => x.EntityGuid.ToString().Contains(q))
                    : order.Items.OrderBy(x => x.Id);
                Console.WriteLine("Here 4");

                if (order is not null)
                {
                    PagedResult<ItemsViewModel> pagedResult = results.AsQueryable().GetPaged<Item, ItemsViewModel>(pageNumber, 25, provider);
                    Console.WriteLine("Here 5");
                    return Result.Success(new OrderViewModel
                    {
                        Id = order.Id,
                        OrderNumber= order.Supplier.Location,
                        SupplierName = order.Supplier.Name,
                        SupplierLocation = order.Supplier.Location,
                        Children = pagedResult,

                    });
                }
                else
                {
                    Console.WriteLine("Here 6");
                    return Result.Failure<OrderViewModel>("No Data Found");
                }

            }
        }
    }
}
