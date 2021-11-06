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
    public class DetailController : ControllerBase
    {
        private readonly OrdersUpDataContext _database;

        public DetailController(OrdersUpDataContext context)
        {
            _database = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Detail>>> GetDetails()
        {
            return await _database.Details.Include(q => q.Logo).Include(q => q.Order).Include(q => q.Inventory).ThenInclude(q => q.Product).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Detail>> GetDetail(int id)
        {
            var item = await _database.Details.Include(q => q.Logo).Include(q => q.Order).Include(q => q.Inventory).ThenInclude(q => q.Product).FirstOrDefaultAsync(q => q.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [Route("~/api/detail/{id:int}/{orderId:int}")]
        public IQueryable<DetailDto> GetOrderByClientId(int id, int orderId)
        {
            return _database.Details.Include(q => q.Logo).Include(q => q.Order).Include(q => q.Inventory).ThenInclude(q => q.Product)
                .Where(b => b.OrderId == orderId)
                .Select(AsBookDto);
        }


        private static readonly Expression<Func<Detail, DetailDto>> AsBookDto =
            x => new DetailDto
            {
                Id= x.Id,
                OrderId= x.OrderId,
                InventoryId = x.InventoryId,
                Quantity = x.Quantity,
                Price = x.Price,
                LogoId = x.LogoId,
                Logo = x.Logo,
                Stitches = x.Stitches,
                Inventory = x.Inventory,
                Order = x.Order
            };

        [HttpPost]
        public async Task<ActionResult<Detail>> PostDetail(Detail item)
        {
            var logo = await _database.Logos.FirstOrDefaultAsync(q => q.Id == item.LogoId);
            var inventory = await _database.Inventories.FirstOrDefaultAsync(q => q.Id == item.InventoryId);
            var order = await _database.Orders.FirstOrDefaultAsync(q => q.Id == item.OrderId);

            if (logo == null)
            {
                return NotFound("Debe de ingresar un logo valido");
            }
            if (inventory == null)
            {
                return NotFound("Debe de ingresar un producto valido");
            }
            if (order == null)
            {
                return NotFound("Debe de ingresar una orden valida");
            }
            if (item.Stitches==0)
            {
                return NotFound("Debe de existir una cantidad de puntadas");
            }
            else
            {
                _database.Details.Add(item);
                await _database.SaveChangesAsync();
                return CreatedAtAction(nameof(GetDetail), new { id = item.Id }, item);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutDetail(int id, Detail item)
        {
            var logo = await _database.Details.FirstOrDefaultAsync(q => q.Id == item.LogoId);
            var inventory = await _database.Details.FirstOrDefaultAsync(q => q.Id == item.InventoryId);
            var order = await _database.Details.FirstOrDefaultAsync(q => q.Id == item.OrderId);

            if (logo == null)
            {
                return NotFound("Debe de ingresar un logo valido");
            }
            if (inventory == null)
            {
                return NotFound("Debe de ingresar un producto valido");
            }
            if (order == null)
            {
                return NotFound("Debe de ingresar una orden valida");
            }
            else
            {
                _database.Entry(item).State = EntityState.Modified;
                await _database.SaveChangesAsync();

                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetail(int id)
        {
            var item = await _database.Details.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            _database.Details.Remove(item);
            await _database.SaveChangesAsync();

            return NoContent();
        }
    }
}
