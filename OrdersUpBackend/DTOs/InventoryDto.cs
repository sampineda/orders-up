using OrdersUpBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersUpBackend.DTOs
{
    public class InventoryDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }
        public int BusinessId { get; set; }
        public Business Business { get; set; }
        public Product Product { get; set; }
        public List<Detail> Details { get; set; }
    }
}
