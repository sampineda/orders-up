using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersUpBackend.Models
{
    public class Detail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int InventoryId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int LogoId { get; set; }
        public int Stitches { get; set; }
        public Logo Logo { get; set; }
        public Inventory Inventory { get; set; }
        public Order Order { get; set; }
    }
}
