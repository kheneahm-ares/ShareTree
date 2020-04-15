using System;
using System.Threading.Tasks;
using Application.DTOs;
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

        [HttpDelete("{id}")]
        public async Task<Unit> Delete (Guid id)
        {
           return await Mediator.Send(new TreesHandler.Delete.Command {Id = id}); 
        }

        [HttpGet("{id}")]
        public async Task<TreeDTO> Details(Guid id)
        {
           return await Mediator.Send(new TreesHandler.Details.Query {Id = id}); 
        }

    }
}