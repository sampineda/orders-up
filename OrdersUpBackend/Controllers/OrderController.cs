using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    public class OrderController : ControllerBase
    {
        private SignInManager<User> _signInManager;
        private UserManager<User> _userManager;
        private readonly OrdersUpDataContext _database;
        double totalTimeOfOrder;
        List<Detail> filterDetails = new List<Detail>();
        DateTime deliveryDate = DateTime.Today;

        public OrderController(OrdersUpDataContext context, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _database = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _database.Orders.Include(q => q.Client).Include(q => q.Details).ThenInclude(q => q.Inventory).ThenInclude(q => q.Product).OrderBy(p => p.DueDate).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var item = await _database.Orders.Include(q => q.Client).Include(q => q.Details).ThenInclude(q => q.Inventory).ThenInclude(q => q.Product).FirstOrDefaultAsync(q => q.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        private static readonly Expression<Func<Order, OrderDto>> AsBookDto =
            x => new OrderDto
            {
                Id = x.Id,
                ClientId = x.ClientId,
                DueDate = x.DueDate,
                ElaborationMinutes = x.ElaborationMinutes,
                Done = x.Done,
                Client = x.Client,
                BusinessId = x.BusinessId,
                Business = x.Business
            };

        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order item)
        {
            item.Done = false;
            var client = await _database.Clients.FirstOrDefaultAsync(q => q.Id == item.ClientId);

            if (item.ClientId == 0 || client==null)
            {
                return NotFound("Debe de ingresar un cliente valido");
            }
            else
            {
                _database.Orders.Add(item);
                await _database.SaveChangesAsync();
                return CreatedAtAction(nameof(GetOrder), new { id = item.Id }, item);
            }
            
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutOrder(int id, Order item)
        {
            var client = await _database.Clients.FirstOrDefaultAsync(q => q.Id == item.ClientId);

            if(item.ElaborationMinutes == 1)
            {
                List<Detail> details = _database.Details.ToList();

                foreach (var detail in details)
                {
                    if (item.Id == detail.OrderId)
                    {
                        filterDetails.Add(detail);
                    }
                }

                CalculateDelivery(item.BusinessId);
                item.DueDate = deliveryDate;
                item.ElaborationMinutes = totalTimeOfOrder;
            }


            if (id != item.Id)
            {
                return BadRequest();
            }
            if (item.ClientId == 0 || client == null)
            {
                return NotFound("Debe de ingresar un cliente valido");
            }
            if (item.ElaborationMinutes <= 0)
            {
                return NotFound("Debe de ingresar una cantidad de minutos");
            }
            else
            {
                _database.Entry(item).State = EntityState.Modified;
                await _database.SaveChangesAsync();
                return CreatedAtAction(nameof(GetOrder), new { id = item.Id }, item);
            }
        }

        public void CalculateDelivery(int businessId)
        {
            double selectedMachine = 0, timeOfBatchesPerMinute, batches, stitchesPerMinute = 800, extraTime = 10, counter = 0;
            bool dateSelected = false;
            int newMinorLeftover, minorLeftover = 0;

            List<Machine> allMachines = _database.Machines.AsNoTracking().ToList();
            List<double> machines = new List<double>();

            foreach (var item in allMachines)
            {
                if (item.BusinessId == businessId)
                {
                    machines.Add(item.Heads);
                }
            }

            foreach (var item in filterDetails)
            {
                foreach (int heads in machines)
                {
                    if (minorLeftover == 0)
                    {
                        newMinorLeftover = item.Quantity - heads;
                        if (newMinorLeftover < 0)
                        {
                            minorLeftover = newMinorLeftover * -1;
                        }
                        else
                        {
                            minorLeftover = newMinorLeftover;
                        }
                        selectedMachine = heads;
                    }
                    else
                    {
                        newMinorLeftover = item.Quantity - heads;
                        if (newMinorLeftover < 0)
                        {
                            newMinorLeftover = newMinorLeftover * -1;
                        }
                        if (minorLeftover > newMinorLeftover)
                        {
                            minorLeftover = newMinorLeftover;
                            selectedMachine = heads;
                        }
                    }
                }

                batches = item.Quantity / selectedMachine;
                if (batches < 1)
                {
                    batches = 1;
                }
                timeOfBatchesPerMinute = (item.Stitches / stitchesPerMinute) + extraTime;
                totalTimeOfOrder = (batches * timeOfBatchesPerMinute) + totalTimeOfOrder;
            }
            double restofMinutes, minutesAllOrders = 0, minutesin8h = 480;

            string day = deliveryDate.ToString("dddd");

            if (day == "Sunday")
            {
                deliveryDate = deliveryDate.AddDays(1);

            }
            else if (day == "Saturday")
            {
                deliveryDate = deliveryDate.AddDays(2);
            }

            List<Order> orders = _database.Orders.AsNoTracking().ToList();
            foreach (var item in orders)
            {
                if (item.DueDate == deliveryDate && item.Done == false)
                {
                    minutesAllOrders = item.ElaborationMinutes + minutesAllOrders;
                }
            }

            restofMinutes = minutesin8h - minutesAllOrders;
            double availablesMinutes = restofMinutes;

            do
            {
                if (totalTimeOfOrder > availablesMinutes)
                {
                    counter++;
                    deliveryDate = deliveryDate.AddDays(counter);
                    minutesAllOrders = 0;
                    foreach (var item in orders)
                    {
                        if (item.DueDate == deliveryDate && item.Done == false)
                        {
                            minutesAllOrders = item.ElaborationMinutes + minutesAllOrders;
                        }
                    }

                    restofMinutes = minutesin8h - minutesAllOrders;
                    availablesMinutes = restofMinutes;
                    if (totalTimeOfOrder < availablesMinutes)
                    {
                        dateSelected = true;
                    }
                }
                else
                {
                    dateSelected = true;
                }

            } while (dateSelected == false);

            string dayOfWeek = deliveryDate.ToString("dddd");

            if (dayOfWeek == "Sunday")
            {
                deliveryDate= deliveryDate.AddDays(1);

            } else if (dayOfWeek == "Saturday")
            {
                deliveryDate=deliveryDate.AddDays(2);
            }
        }

        //public double GetRestofMinutes(DateTime date)
        //{
        //    double restofMinutes, minutesAllOrders = 0, minutesin8h = 480;

        //    List<Order> orders = _database.Orders.ToList();

        //    foreach (var item in orders)
        //    {
        //        if (item.DueDate == date && item.Done == false)
        //        {
        //            minutesAllOrders = item.ElaborationMinutes + minutesAllOrders;
        //        }
        //    }

        //    restofMinutes = minutesin8h - minutesAllOrders;
        //    return restofMinutes;
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            List<Detail> details = _database.Details.AsNoTracking().ToList();
            List<Event> events = _database.Events.AsNoTracking().ToList();

            foreach (var detail in details)
            {
                if (detail.OrderId== id)
                {
                    _database.Details.Remove(detail);
                    await _database.SaveChangesAsync();
                }
            }

            foreach (var ev in events)
            {
                if (ev.OrderId== id)
                {
                    _database.Events.Remove(ev);
                    await _database.SaveChangesAsync();
                }
            }

            var item = await _database.Orders.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            _database.Orders.Remove(item);
            await _database.SaveChangesAsync();

            return NoContent();
        }

    }
}
