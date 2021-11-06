using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdersUpBackend.DataContext;
using OrdersUpBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersUpBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController : ControllerBase
    {
        private readonly OrdersUpDataContext _database;
        public BusinessController(OrdersUpDataContext context)
        {
            _database = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Business>>> GetBusinesses()
        {
            return await _database.Businesses.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Business>> GetBusiness(int id)
        {
            var item = await _database.Businesses.FirstOrDefaultAsync(q => q.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<Business>> PostBusiness(Business item)
        {
            if (string.IsNullOrEmpty(item.Name))
            {
                return NotFound("Debe de ingresar un nombre valido");
            }
            if (string.IsNullOrEmpty(item.RTN))
            {
                return NotFound("Debe de ingresar un RTN valido");
            }
            else
            {
                _database.Businesses.Add(item);
                await _database.SaveChangesAsync();
                return CreatedAtAction(nameof(GetBusiness), new { id = item.Id }, item);
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutBusiness(int id, Business item)
        {
            if (string.IsNullOrEmpty(item.Name))
            {
                return NotFound("Debe de ingresar un nombre valido");
            }
            if (string.IsNullOrEmpty(item.RTN))
            {
                return NotFound("Debe de ingresar un RTN valido");
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
        public async Task<IActionResult> DeleteBusiness(int id)
        {
            var item = await _database.Businesses.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            _database.Businesses.Remove(item);
            await _database.SaveChangesAsync();

            return NoContent();
        }

    }
}
