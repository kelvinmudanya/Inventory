

using Inventory.Abstract;
using Inventory.Common.Behaviours.Extensions.Paging;
using Inventory.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Inventory.Features.Orders
{
    public class OrdersController:BaseApiController
    {

        [HttpPost]
        public async Task<IActionResult> Post(OrderViewModel viewModel)
        {
            Console.WriteLine("Am here 0001");
            return Feedback(await Mediator.Send(new Create.Command(viewModel)), ActionPerformed.Add);
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<List.OrderViewModel>>> Get(int? page, string q) => Ok(await Mediator.Send(new List.Query(page, q)));


        [HttpGet("{orderId:int}/listofItems")]
        public async Task<IActionResult> GetListOfAllItems(int orderId, int page, string q) => Process<Details.OrderViewModel>(await Mediator.Send(new Details.Query(page, q, orderId)));


    }
}
