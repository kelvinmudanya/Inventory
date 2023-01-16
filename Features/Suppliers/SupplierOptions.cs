
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Inventory.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Features.Suppliers
{
    public class SupplierOptions
    {

        public class Query : IRequest<List<SupplierViewModel>>
        {
          
        }
        public class SupplierViewModel
        {
           
            public string Label { get; set; }
            public Guid Value { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, List<SupplierViewModel>>
        {
            private readonly IConfigurationProvider provider;
            private readonly MainContext context;

            public QueryHandler(IConfigurationProvider provider, MainContext context)
            {
                this.provider = provider;
                this.context = context;
            }

            public async Task<List<SupplierViewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var bloodAndBloodProduct = await context.Suppliers
                    .ProjectTo<SupplierViewModel>(provider)
                    .ToListAsync(cancellationToken: cancellationToken);

                return bloodAndBloodProduct;
            }
        }
    }
}