using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using backend.Models;
using backend.Data;
using System.Linq;
using System;

namespace backend.Controllers
{
    [ApiController]
    [Route("reserves")]
    [Authorize]
    public class ReserveController: BaseController
    {
        [HttpGet]
        [Route("")]

        public async Task<ActionResult<List<Reserve>>> List([FromServices] DataContext context)
        {
            
            var reserves = await context.Reserves.Where(r => r.HotelGuestId.Equals(this.GetUserId())).ToListAsync();
            return reserves;
        }


        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Reserve>> Create(
            [FromServices] DataContext context,
            [FromBody] Reserve model)
        {
            if(ModelState.IsValid)
            {

                var hasPendencyCheckOut = context.Reserves.Any(r => r.CheckOut == null && r.HotelGuestId == this.GetUserId());

                if(hasPendencyCheckOut){
                    return BadRequest("Você ainda não terminou sua ultima estadia");
                }

                var sql = @$"   SELECT * 
                                FROM Bedrooms 
                                WHERE id NOT IN 
                                (
                                    SELECT bedroom_id 
                                    FROM Reserves
                                    where convert(varchar(10), check_in) = '{model.CheckIn.Date.ToString("yyyy-MM-dd")}'
                                )
                                and id = {model.BedroomId}";
                

                var availables = await context.Bedrooms.FromSqlRaw(sql).ToListAsync();

                if(availables.Count() == 0){
                    return BadRequest("Quarto indisponível");
                }

                model.CheckOut = null;
                model.HotelGuestId = this.GetUserId();

                context.Reserves.Add(model);
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