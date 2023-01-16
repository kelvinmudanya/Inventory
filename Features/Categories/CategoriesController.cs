using Inventory.Abstract;
using Inventory.Common.Behaviours.Extensions.Paging;
using Inventory.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Inventory.Features.Categories
{
    public class CategoriesController:BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Post(CategoryViewModel viewModel)
        {
            
            return Feedback (await Mediator.Send(new Create.Command(viewModel)), ActionPerformed.Add);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Update.Command command)
        {
            command.Id = id;
            return Feedback(await Mediator.Send(command), ActionPerformed.Update);
        }


        [HttpGet]
        public async Task<ActionResult<PagedResult<List.CategoryViewModel>>> Get(int? page, string q) => Ok(await Mediator.Send(new List.Query(page, q)));


        [HttpGet("{categoryId:int}/listofItems")]
        public async Task<IActionResult> GetListOfAllItems(int categoryId, int page, string q) => Process<Details.CategoryViewModel>(await Mediator.Send(new Details.Query(page, q, categoryId)));

    }
}
