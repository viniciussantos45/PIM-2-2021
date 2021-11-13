using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    public abstract class BaseController: Controller
    {

        protected int GetUserId()
        {
            return int.Parse(this.User.Claims.First(i => i.Type == "UserId").Value);
        }
        
    }
}