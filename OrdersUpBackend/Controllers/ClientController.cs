using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdersUpBackend.DataContext;
using OrdersUpBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OrdersUpBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly OrdersUpDataContext _database;
        public ClientController(OrdersUpDataContext context)
        {
            _database = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            return await _database.Clients.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var item = await _database.Clients.FirstOrDefaultAsync(q => q.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client item)
        {

            if (string.IsNullOrEmpty(item.Name) || item.Name.All(char.IsDigit))
            {
                return NotFound("Debe de ingresar un nombre de cliente valido.");
            }
            if (string.IsNullOrEmpty(item.PhoneNumber))
            {
                return NotFound("Debe de ingresar un numero valido");
            }
            else
            {
                _database.Clients.Add(item);
                await _database.SaveChangesAsync();
                return CreatedAtAction(nameof(GetClient), new { id = item.Id }, item);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutClient(int id, Client item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }
            if (string.IsNullOrEmpty(item.Name) || item.Name.All(char.IsDigit))
            {
                return NotFound("Debe de ingresar un nombre de cliente valido.");
            }
            if (string.IsNullOrEmpty(item.PhoneNumber))
            {
                return NotFound("Debe de ingresar un numero valido");
            }
            else
            {
                _database.Entry(item).State = EntityState.Modified;
                await _database.SaveChangesAsync();

                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var item = await _database.Clients.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            _database.Clients.Remove(item);
            await _database.SaveChangesAsync();

            return NoContent();
        }
    }
}
