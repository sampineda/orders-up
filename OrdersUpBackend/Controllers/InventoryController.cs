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
    public class InventoryController : ControllerBase
    {
        private readonly OrdersUpDataContext _database;

        public InventoryController(OrdersUpDataContext context)
        {
            _database = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventory>>> GetInventories()
        {
            return await _database.Inventories.Include(q => q.Product).Include(q => q.Business).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Inventory>> GetInventory(int id)
        {
            var item = await _database.Inventories.Include(q => q.Product).Include(q => q.Business).FirstOrDefaultAsync(q => q.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [Route("~/api/inventory/{id:int}/{productId:int}")]
        public IQueryable<InventoryDto> GetOrderByClientId(int id, int productId)
        {
            return _database.Inventories.Include(q => q.Product).Include(q => q.Business)
                .Where(b => b.ProductId == productId)
                .Select(AsBookDto);
        }

        private static readonly Expression<Func<Inventory, InventoryDto>> AsBookDto =
            x => new InventoryDto
            {
                Id = x.Id,
                ProductId = x.ProductId,
                Color = x.Color,
                Quantity = x.Quantity,
                BusinessId = x.BusinessId,
                Business = x.Business,
                Product = x.Product
            };

        [HttpPost]
        public async Task<ActionResult<Inventory>> PostInventory(Inventory item)
        {
            if (string.IsNullOrEmpty(item.Color))
            {
                return NotFound("Debe de ingresar un color valido");
            }
            if (item.Quantity<=0)
            {
                return NotFound("Debe de ingresar una cantidad valida");
            }
            else
            {
                _database.Inventories.Add(item);
                await _database.SaveChangesAsync();
                return CreatedAtAction(nameof(GetInventory), new { id = item.Id }, item);
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutInventory(int id, Inventory item)
        {
            if (string.IsNullOrEmpty(item.Color))
            {
                return NotFound("Debe de ingresar un color valido");
            }
            if (item.Quantity <= 0)
            {
                return NotFound("Debe de ingresar una cantidad valida");
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
        public async Task<IActionResult> DeleteInventory(int id)
        {
            var item = await _database.Inventories.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            _database.Inventories.Remove(item);
            await _database.SaveChangesAsync();

            return NoContent();
        }
    }
}
