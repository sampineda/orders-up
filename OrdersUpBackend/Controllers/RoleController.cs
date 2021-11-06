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
    public class RoleController : ControllerBase
    {
        private readonly OrdersUpDataContext _database;

        public RoleController(OrdersUpDataContext context)
        {
            _database = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            return await _database.Roles.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            var item = await _database.Roles.FirstOrDefaultAsync(q => q.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(Role item)
        {
            if (string.IsNullOrEmpty(item.Title))
            {
                return NotFound("Debe de ingresar un rol valido");
            }
            else
            {
                _database.Roles.Add(item);
                await _database.SaveChangesAsync();
                return CreatedAtAction(nameof(GetRole), new { id = item.Id }, item);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutRole(int id, Role item)
        {
            if (string.IsNullOrEmpty(item.Title))
            {
                return NotFound("Debe de ingresar un rol valido");
            }
            else
            {
                _database.Entry(item).State = EntityState.Modified;
                await _database.SaveChangesAsync();

                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var item = await _database.Roles.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            _database.Roles.Remove(item);
            await _database.SaveChangesAsync();

            return NoContent();
        }
    }
}
