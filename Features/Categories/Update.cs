
using AutoMapper;
using CSharpFunctionalExtensions;
using FluentValidation;
using Inventory.Data;
using Inventory.ViewModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Features.Categories
{
    public class Update
    {
        public class Command : CategoryViewModel, IRequest<Result>
        {

            [IgnoreDataMember]
            public int Id { get; set; }
            public Command(int id)
            {

                Id = id;

            }
        }

        public class Validator : AbstractValidator<Command>
        {
            private readonly MainContext context;
            public Validator(MainContext context)
            {
                this.context = context;

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
                var category = await context.Categories
                    .FirstOrDefaultAsync(a => a.Id == request.Id);

                mapper.Map(request, category);

                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return Result.Success();
            }

        }
    }
}

