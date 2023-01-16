using AutoMapper;
using Inventory.Common.Behaviours.Extensions.Paging;
using Inventory.Data;
using Inventory.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Features.Orders
{
    public class List
    {
        public class Query : IRequest<PagedResult<OrderViewModel>>
        {
            public Query(int? page, string s)
            {
                Page = page;
                Q = s;
            }

            public int? Page { get; set; }
            public string Q { get; set; }
        }


        public class OrderViewModel
        {
            public System.Guid EntityGuid { get; set; }
            public int Id { get; set; }
            public string OrderNumber { get; set; }
            public string SupplierName { get; set; }
            public string SupplierLocation { get; set; }



        }



        public class QueryHandler : IRequestHandler<Query, PagedResult<OrderViewModel>>
        {
            private readonly IConfigurationProvider provider;
            public readonly MainContext context;


            public QueryHandler(IConfigurationProvider provider, MainContext context)
            {
                this.provider = provider;
                this.context = context;
            }

            public async Task<PagedResult<OrderViewModel>> Handle(Query request, CancellationToken cancellationToken)
            {

                var pageNumber = request.Page ?? 1;
                var q = request.Q?.ToLower().Trim();
                var model = context.Orders;
                var results = !string.IsNullOrWhiteSpace(q)
                    ? model
                        .Where(x => x.OrderNumber.ToString() == q
                        || EF.Functions.Like(x.OrderNumber.ToString(), $"%{q}%"))


                        .OrderByDescending(x => x.Id)
                    : model.OrderByDescending(x => x.Id);

                return await results.GetPagedAsync<Order, OrderViewModel>(provider, pageNumber);

            }
        }
    }
}
