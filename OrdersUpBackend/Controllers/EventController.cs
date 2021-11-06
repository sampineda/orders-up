using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdersUpBackend.DataContext;
using OrdersUpBackend.DTOs;
using OrdersUpBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OrdersUpBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController: ControllerBase
    {
        private readonly OrdersUpDataContext _database;

        public EventController(OrdersUpDataContext context)
        {
            _database = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            return await _database.Events.Include(q => q.Order).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var item = await _database.Events.Include(q => q.Order).FirstOrDefaultAsync(q => q.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<Event>> PostProduct(Event item)
        {
            if (string.IsNullOrEmpty(item.Title))
            {
                return NotFound("Debe de ingresar un titulo valido");
            }
            else
            {
                _database.Events.Add(item);
                await _database.SaveChangesAsync();
                return CreatedAtAction(nameof(GetEvent), new { id = item.Id }, item);
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutEvent(int id, Event item)
        {
            if (string.IsNullOrEmpty(item.Title))
            {
                return NotFound("Debe de ingresar un titulo valido");
            }

            if (id != item.Id)
            {
                return BadRequest();
            }
            else
            {
                _database.Entry(item).State = EntityState.Modified;
                await _database.SaveChangesAsync();

                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var item = await _database.Events.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            _database.Events.Remove(item);
            await _database.SaveChangesAsync();

            return NoContent();
        }

    }
}
