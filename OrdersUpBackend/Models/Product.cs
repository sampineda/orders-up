using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersUpBackend.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int BusinessId { get; set; }
        public Business Business { get; set; }
        public List<Inventory> Inventories { get; set; }
    }
}
