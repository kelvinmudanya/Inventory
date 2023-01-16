
using Inventory.Abstract;
using Inventory.Common.Behaviours.Extensions.Paging;
using Inventory.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Inventory.Features.Suppliers
{
    public class SuppliersController:BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Post(SuppliersViewModel viewModel)
        {

            return Feedback(await Mediator.Send(new Create.Command(viewModel)), ActionPerformed.Add);
        }


        [HttpGet]
        public async Task<ActionResult<PagedResult<List.SupplierViewModel>>> Get(int? page, string q) => Ok(await Mediator.Send(new List.Query(page, q)));



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

        [HttpGet("/suppliersOptions")]
        public async Task<ActionResult<SupplierOptions.SupplierViewModel>> GetAllSuppliers()
        {
            return Ok(await Mediator.Send(new SupplierOptions.Query()));
        }
        
    }
}
