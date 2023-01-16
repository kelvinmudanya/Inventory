using CSharpFunctionalExtensions;
using FluentValidation;
using Inventory.Data;
using Inventory.Domain;
using Inventory.ViewModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Features.Orders
{
    public class Create
    {
        public class Command : OrderViewModel, IRequest<Result>
        {
            public Command(OrderViewModel viewModel)
            {
                Console.WriteLine("Am here 0002");
                Item = viewModel.Item;
                SupplierGuid = viewModel.SupplierGuid;
                
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
            private readonly MainContext context;



            public CommandHandler(MainContext context)
            {
                this.context = context;
            }
            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                Console.WriteLine("00011");

                try
                {
                    var supplier = await context.Suppliers.FirstOrDefaultAsync(x => x.EntityGuid == request.SupplierGuid);
                   
                    if(supplier is null)
                    {
                        return Result.Failure("Could not create the order since the supplier does not exist");
                    }
                    var order = new Order()
                    {
                        CreatedBy = "Azenga Albert",
                        CreatedOnUtc = DateTime.UtcNow,
                        
                        
                    };
                    Console.WriteLine("00022");
                    order.Supplier = supplier;
                    long requestId = 0;
                    string fmtValue = "000";
                    string fmtMonth = "0000";
                    var orderNumbers = await context.OrderNumbers.AsNoTracking().OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                    Console.WriteLine("00033");
                    requestId = orderNumbers?.Id ?? 0;
                    Console.WriteLine("000333");
                    var orderNumber = $"ORD{DateTime.Now.Year}{(DateTime.Now.Month).ToString(fmtMonth)}{(requestId + 1).ToString(fmtValue)}";
                    Console.WriteLine("000444");
                    await context.OrderNumbers.AddAsync(new OrderNumber(orderNumber));
                    order.OrderNumber = orderNumber;

                    await context.Orders.AddAsync(order);

                    Console.WriteLine("00044");
                    Console.WriteLine(request.Item);

                    foreach (var item1 in request.Item)
                    {
                        Console.WriteLine(item1.EntityGuid);
                        var item = await context.Items.FirstOrDefaultAsync(x => x.EntityGuid == item1.EntityGuid);
                        order.Items.Add(item);
                    }

                    Console.WriteLine("0005");

                    await context.SaveChangesAsync();




                }
                catch (Exception ex)
                {

                    Console.WriteLine("The message could not be send" + ex.Message);
                }

                return Result.Success();
            }
        }
    }
}