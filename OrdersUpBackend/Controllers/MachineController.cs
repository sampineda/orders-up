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
    public class MachineController: ControllerBase
    {
        private readonly OrdersUpDataContext _database;

        public MachineController(OrdersUpDataContext context)
        {
            _database = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Machine>>> GetMachines()
        {
            return await _database.Machines.Include(q => q.Business).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Machine>> GetMachine(int id)
        {
            var item = await _database.Machines.Include(q => q.Business).FirstOrDefaultAsync(q => q.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [Route("~/api/machine/{id:int}/{businessId:int}")]
        public IQueryable<MachineDTO> GetMachineByBusinessId(int id, int businessId)
        {
            return _database.Machines.Include(q => q.Business)
                .Where(b => b.BusinessId == businessId)
                .Select(AsBookDto);
        }


        private static readonly Expression<Func<Machine, MachineDTO>> AsBookDto =
            x => new MachineDTO
            {
                Id = x.Id,
                BusinessId = x.BusinessId,
                Heads = x.Heads,
                Business= x.Business
            };

        [HttpPost]
        public async Task<ActionResult<Machine>> PostMachine(Machine item)
        {
            if (item.Heads <= 0)
            {
                return NotFound("Debe de ingresar cantidad de cabezas mayor a 0");
            }
            else
            {
                _database.Machines.Add(item);
                await _database.SaveChangesAsync();
                return CreatedAtAction(nameof(GetMachine), new { id = item.Id }, item);
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutMachine(int id, Machine item)
        {
            if (item.Heads <= 0)
            {
                return NotFound("Debe de ingresar cantidad de cabezas mayor a 0");
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
        public async Task<IActionResult> DeleteMachine(int id)
        {
            var item = await _database.Machines.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            _database.Machines.Remove(item);
            await _database.SaveChangesAsync();

            return NoContent();

        }
    }
}
