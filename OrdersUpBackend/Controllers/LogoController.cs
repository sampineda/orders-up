using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdersUpBackend.DataContext;
using OrdersUpBackend.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersUpBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogoController : ControllerBase
    {
        private readonly OrdersUpDataContext _database;

        public LogoController(OrdersUpDataContext context)
        {
            _database = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Logo>>> GetLogos()
        {
            return await _database.Logos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Logo>> GetLogo(int id)
        {
            var item = await _database.Logos.FirstOrDefaultAsync(q => q.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<Logo>> PostLogo(Logo item)
        {
            if (string.IsNullOrEmpty(item.Name))
            {
                return NotFound("Debe de ingresar un nombre valido");
            }
            if (string.IsNullOrEmpty(item.Location))
            {
                return NotFound("Debe de ingresar una ubicacion valida");
            }
            else
            {
                _database.Logos.Add(item);
                await _database.SaveChangesAsync();
                return CreatedAtAction(nameof(GetLogo), new { id = item.Id }, item);
            }

        }
    }
}
