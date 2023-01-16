using AutoMapper;
using CSharpFunctionalExtensions;
using FluentValidation;
using Inventory.Data;
using Inventory.Domain;
using Inventory.ViewModel;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Features.Categories
{
    public class Create
    {
        public class Command : CategoryViewModel, IRequest<Result>
        {

            public Command(CategoryViewModel viewModel)
            {

                Name = viewModel.Name;
                Description = viewModel.Description;




            }
        }

        public class Validator : AbstractValidator<Command>
        {
            private readonly MainContext context;
            public Validator(MainContext context)
            {
                this.context = context;
                RuleFor(x => x.Name).NotEmpty();

            }
        }


        public class CommandHandler : IRequestHandler<Command, Result>
        {
            private readonly MainContext context;
            private readonly IMapper mapper;

            public CommandHandler(MainContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }
            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {

                    var category = mapper.Map<Category>(request);

                    await context.Categories.AddAsync(category, cancellationToken);
                    await context.SaveChangesAsync();

                }
                catch (Exception ex)
                {

                    Console.WriteLine("The request could not be send" + ex.Message);
                }

                return Result.Success();

            }
        }
    }
}