using AutoMapper;
using AutoMapper.QueryableExtensions;
using CSharpFunctionalExtensions;
using Inventory.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Features.Items
{
    public class Details
    {
        public class Query : IRequest<Result<ItemsViewModel>>
        {
            public int Id { get; set; }

            public Query(int id)
            {
                Id = id;

            }
        }
        public class ItemsViewModel
        {
            public int Id { get; set; }
            public string CategoryName { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal SellingPrice { get; set; }

        }



        public class QueryHandler : IRequestHandler<Query, Result<ItemsViewModel>>
        {
            private readonly IMapper mapper;
            private readonly MainContext context;
            private readonly IConfigurationProvider provider;

            public QueryHandler(IMapper mapper, MainContext context, IConfigurationProvider provider)
            {
                this.mapper = mapper;
                this.context = context;
                this.provider = provider;
            }

            public async Task<Result<ItemsViewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var recipient = await context.Items.Include(x=>x.Category)

                .ProjectTo<ItemsViewModel>(provider)
                .FirstOrDefaultAsync(a => a.Id == request.Id
                    ,cancellationToken: cancellationToken);


                return Result.Success(recipient);
            }
        }
    }
}
