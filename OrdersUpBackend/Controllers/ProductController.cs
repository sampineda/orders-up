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
    [Route ("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly OrdersUpDataContext _database;

        public ProductController(OrdersUpDataContext context)
        {
            _database = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _database.Products.Include(q => q.Business).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var item = await _database.Products.Include(q => q.Business).FirstOrDefaultAsync(q => q.Id == id);
            if(item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product item)
        {
            if (string.IsNullOrEmpty(item.Name))
            {
                return NotFound("Debe de ingresar un nombre valido");
            }
            if (string.IsNullOrEmpty(item.Code))
            {
                return NotFound("Debe de ingresar un codigo valido");
            }
            else
            {
                _database.Products.Add(item);
                await _database.SaveChangesAsync();
                return CreatedAtAction(nameof(GetProduct), new { id = item.Id }, item);
            }
           
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutProduct(int id, Product item)
        {
            if (string.IsNullOrEmpty(item.Name))
            {
                return NotFound("Debe de ingresar un nombre valido");
            }
            if (string.IsNullOrEmpty(item.Code))
            {
                return NotFound("Debe de ingresar un codigo valido");
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
        public async Task<IActionResult> DeleteProduct (int id)
        {
            var item = await _database.Products.FindAsync(id);
            if(item == null)
            {
                return NotFound();
            }
            _database.Products.Remove(item);
            await _database.SaveChangesAsync();

            return NoContent();
        }
    }
}
