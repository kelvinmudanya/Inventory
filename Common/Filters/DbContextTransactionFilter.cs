using Inventory.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace Inventory.Common.Filters
{
    public class DbContextTransactionFilter : IAsyncActionFilter
    {
        private readonly MainContext _dbContext;

        public DbContextTransactionFilter(MainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                await _dbContext.BeginTransactionAsync();

                var actionExecuted = await next();
                if (actionExecuted.Exception != null && !actionExecuted.ExceptionHandled)
                {
                    _dbContext.RollbackTransaction();

                }
                else
                {
                    await _dbContext.CommitTransactionAsync();

                }
            }
            catch (Exception)
            {
                _dbContext.RollbackTransaction();
                throw;
            }
        }
    }
}
