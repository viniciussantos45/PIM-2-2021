using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using backend.Services;
using backend.Models;
using backend.Data;
using System.Linq;

namespace backend.Controllers
{
    [ApiController]
    [Route("sessions")]
    [AllowAnonymous]
    public class SessionController: ControllerBase
    {

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<dynamic>> Authenticate(
            [FromServices] DataContext context,
            [FromBody] UserAuthenticate model)
        {            
                var email = model.Email;
                var password = model.Password;

                var user = context.Users.Where(x => x.Email == email && x.Password == password).FirstOrDefault();

                if (user == null)
                    return NotFound(new {message = "Usuário ou senha inválidos"});

                var token = TokenService.GenerateToken(user);
                user.Password = null;

                return new 
                {  
                    user,
                    token,
                };
        }
        
    }
}