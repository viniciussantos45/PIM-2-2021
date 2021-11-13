using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using backend.Models;
using backend.Data;
using System.Linq;

namespace backend.Controllers
{
    [ApiController]
    [Route("bedrooms")]
    public class BedroomController: ControllerBase
    {
        [HttpGet]
        [Route("")]
        [Authorize(Roles = "manager")]

        public async Task<ActionResult<List<Bedroom>>> List([FromServices] DataContext context)
        {
            var bedrooms = await context.Bedrooms.ToListAsync();
            return bedrooms;
        }


        [HttpPost]
        [Route("")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<Bedroom>> Create(
            [FromServices] DataContext context,
            [FromBody] Bedroom model)
        {
            if(ModelState.IsValid)
            {
                var hasApartment = context.Bedrooms.Any(x => x.ApartmentNumber == model.ApartmentNumber);

                if(hasApartment){
                    return BadRequest("Este quarto já existe");
                }
                context.Bedrooms.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        [Route("availables")]
        [Authorize]

        public async Task<ActionResult<dynamic>> Availables(
            [FromServices] DataContext context, 
            [FromBody] AvailableBedroom model )
        {
            var sql = @$"   SELECT * 
                            FROM Bedrooms 
                            WHERE id NOT IN 
                            (
                                SELECT bedroom_id 
                                FROM Reserves
                                where convert(varchar(10), check_in) = '{model.Date}'
                                
                            )";

            var availables = await context.Bedrooms.FromSqlRaw(sql).ToListAsync();
            
            return availables;
        }
        
    }
}