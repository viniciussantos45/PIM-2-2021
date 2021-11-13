using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System;

namespace backend.Controllers
{
    [ApiController]
    [Route("manager")]
    [Authorize(Roles = "manager")]
    public class ManagerController: ControllerBase
    {
        [HttpGet]
        [Route("reserves")]

        public async Task<ActionResult<dynamic>> List(
            [FromServices] DataContext context, 
            [FromBody] AvailableBedroom model )
        {
            var availableRooms = @$" SELECT * 
                                FROM Bedrooms 
                                WHERE id NOT IN 
                                (
                                    SELECT bedroom_id 
                                    FROM Reserves
                                    where convert(varchar(10), check_in) = {model.Date}
                                    
                                )";

            var availables = await context.Bedrooms.FromSqlRaw(availableRooms).ToListAsync();
            
            return availables;
        }
        
        [HttpPost]
        [Route("add_manager/{userId}")]
        public async Task<ActionResult<dynamic>> AddManager(
            [FromServices] DataContext context,
            int userId)
        {
            var user = context.Users.SingleOrDefault(u => u.Id == userId);
            if(user != null){
                user.Manager = true;
                context.SaveChanges();

            }else{
                return BadRequest("Usuário não encontrado");
            }

            return user;
            
        }
        
        [HttpPost]
        [Route("remove_manager/{userId}")]
        public async Task<ActionResult<dynamic>> RemoveManager(
            [FromServices] DataContext context,
            int userId)
        {
            var user = context.Users.SingleOrDefault(u => u.Id == userId);
            if(user != null){
                user.Manager = false;
                context.SaveChanges();

            }else{
                return BadRequest("Usuário não encontrado");
            }

            return user;
            
        }

        [HttpPost]
        [Route("reserves/check_out/{reserveId}")]
        public async Task<ActionResult<dynamic>> CheckOutReserve(
            [FromServices] DataContext context,
            int reserveId)
        {
            var reserve = context.Reserves.SingleOrDefault(r => r.Id == reserveId);
            if(reserve != null){
                var date = DateTime.UtcNow.Date;
                reserve.CheckOut = date;
                context.SaveChanges();

            }else{
                return BadRequest("Reserva não encontrada");
            }

            return reserve;
            
        }



    }

}