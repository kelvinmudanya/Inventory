
using Inventory.Abstract;
using Inventory.Common.Behaviours.Extensions.Paging;
using Inventory.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Inventory.Features.Items
{
    public class ItemsController:BaseApiController
    {

        [HttpPost("{categoryId}")]
        public async Task<IActionResult> Post(int categoryId, ItemsViewModel viewModel)
        {

            return Feedback(await Mediator.Send(new Create.Command(categoryId, viewModel)), ActionPerformed.Add);
        }


        [HttpGet]
        public async Task<ActionResult<PagedResult<List.ItemsViewModel>>> Get(int? page, string q) => Ok(await Mediator.Send(new List.Query(page, q)));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new Details.Query(id));
            if (result.IsSuccess)
            {
                if (result.Value != null)
                {
                    return Ok(result.Value);
                }

                return NotFound();
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Update.Command command)
        {
            command.Id = id;
            return Feedback(await Mediator.Send(command), ActionPerformed.Update);
        }
    }
}
