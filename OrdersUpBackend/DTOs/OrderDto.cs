using OrdersUpBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersUpBackend.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int BusinessId { get; set; }
        public DateTime DueDate { get; set; }
        public double ElaborationMinutes { get; set; }
        public bool Done { get; set; }
        public Client Client { get; set; }
        public Business Business { get; set; }
        public List<Detail> Details { get; set; }
        public List<Event> Events { get; set; }
    }
}
