
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CSharpFunctionalExtensions;
using Inventory.Data;
using Inventory.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Features.Suppliers
{
    public class Details
    {
       public class Query: IRequest<Result<SupplierViewModel>>
        {
            public int Id { get; set; }

        public Query(int id)
        {
            Id = id;

        }
    }
    public class SupplierViewModel
    {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }
            public string Description { get; set; }
            public string Location { get; set; }
            public string Address { get; set; }
            public string EmailAddress { get; set; }
            public string PhoneNumber { get; set; }
            public List<ContactPerson> ContactPersons { get; set; }

        }



    public class QueryHandler : IRequestHandler<Query, Result<SupplierViewModel>>
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

        public async Task<Result<SupplierViewModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            var recipient = await context.Suppliers.Include(x => x.ContactPersons)

            .ProjectTo<SupplierViewModel>(provider)
            .FirstOrDefaultAsync(a => a.Id == request.Id
                , cancellationToken: cancellationToken);

            return Result.Success(recipient);
        }
    }
}
}
