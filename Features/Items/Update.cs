using AutoMapper;
using CSharpFunctionalExtensions;
using Inventory.Data;
using Inventory.ViewModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Features.Items
{
    public partial class Update
    {
        public class Command : ItemsViewModel, IRequest<Result>
        {

            [IgnoreDataMember]
            public int Id { get; set; }
            public Command(int id)
            {

                Id = id;

            }
        }


        public class CommandHandler : IRequestHandler<Command, Result>
        {
            private readonly IMapper mapper;
            private readonly MainContext context;


            public CommandHandler(IMapper mapper, MainContext context)
            {
                this.mapper = mapper;
                this.context = context;
            }
            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var item = await context.Items
                    .FirstOrDefaultAsync(a => a.Id == request.Id);

                mapper.Map(request, item);

                context.Items.Update(item);
                await context.SaveChangesAsync();

                return Result.Success();
            }

        }
    }
}
