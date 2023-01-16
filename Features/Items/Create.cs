using AutoMapper;
using CSharpFunctionalExtensions;
using FluentValidation;
using Inventory.Data;
using Inventory.Domain;
using Inventory.ViewModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Features.Items
{
    public class Create
    {
        public class Command : ItemsViewModel, IRequest<Result>
        {
            public int CategoryId { get; set; }

            public Command(int categoryId, ItemsViewModel viewModel)
            {
                CategoryId = categoryId;
                Name = viewModel.Name;
                Description = viewModel.Description;
                UnitPrice = viewModel.UnitPrice;
                SellingPrice = viewModel.SellingPrice;

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
                    var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.CategoryId);
                       
                    if(category is null)
                    {
                        return Result.Failure("Could not create the item since the category does not exist");
                    }
                    var items = mapper.Map<Item>(request);
                    items.Category = category;

                    await context.Items.AddAsync(items, cancellationToken);
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