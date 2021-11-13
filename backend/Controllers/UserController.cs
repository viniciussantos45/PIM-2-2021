using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using System.Linq;

namespace backend.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController: ControllerBase
    {
        [HttpGet]
        [Route("")]

        public async Task<ActionResult<List<User>>> List([FromServices] DataContext context)
        {
            var users = await context.Users.ToListAsync();
            return users;
        }


        [HttpPost]
        [Route("")]
        public async Task<ActionResult<User>> Create(
            [FromServices] DataContext context,
            [FromBody] User model)
        {
            if(ModelState.IsValid)
            {
                var alreadyExists = context.Users.Any(x => x.Email == model.Email || x.Username == model.Username);

                if(alreadyExists){
                    return BadRequest("Nome de usuário ou email já cadastrado !");
                }

                context.Users.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        
    }
}