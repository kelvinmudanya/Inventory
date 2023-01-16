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

namespace Inventory.Features.Sales
{
    public class Create
    {
        public class Command : SaleViewModel, IRequest<Result>
        {
            [IgnoreDataMember]
            public int FacilityStorageId { get; set; }
            public Command(SaleViewModel viewModel)
            {
                Items = viewModel.Items;
                Customer = viewModel.Customer;

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
                try
                {
                   

                    var sale = new Sale()
                    {
                        CreatedBy = "Azenga Albert",
                        CreatedOnUtc = DateTime.UtcNow,
                        Customer = new Customer()
                        {
                            Name = request.Customer.Name,
                            Address = request.Customer.Address,
                            EmailAddress = request.Customer.EmailAddress,
                            PhoneNumber = request.Customer.PhoneNumber
                        }
                    };

                    foreach (var item1 in request.Items)
                    {
                        var item = await context.Items.FirstOrDefaultAsync(x => x.EntityGuid == item1.EntityGuid);
                        sale.Items.Add(item);
                    }


                    long requestId = 0;
                    string fmtValue = "000";
                    string fmtMonth = "0000";
                    var saleNumbers = await context.SalesNumbers.AsNoTracking().OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                    requestId = saleNumbers?.Id ?? 0;

                    var saleNumber = $"SLS{DateTime.Now.Year}{(DateTime.Now.Month).ToString(fmtMonth)}{(requestId + 1).ToString(fmtValue)}";

                    await context.OrderNumbers.AddAsync(new OrderNumber(saleNumber));
                    sale.SalesNumber = saleNumber;


                    await context.Sales.AddAsync(sale, cancellationToken);
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