using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TreesHandler = Application.Trees;

namespace API.Controllers
{
    public class TreeController : BaseController<TreeController>
    {
        [HttpPost("create")]
        public async Task<Unit> Create ([FromBody]TreesHandler.Create.Command command)
        {
            return await Mediator.Send (command);
        }
    }
}