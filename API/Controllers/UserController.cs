using System.Threading.Tasks;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserHandler = Application.Users;

namespace API.Controllers
{
    public class UserController : BaseController<UserController>
    {
        [HttpGet("hi")]
        public string Hi()
        {
            return "hi";
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(UserHandler.Login.Query query)
        {
            return await Mediator.Send(query);
        }
        
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserHandler.Register.Command command)
        {
            return await Mediator.Send(command);
        }
    }
}