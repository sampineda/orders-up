using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersUpBackend.Models
{
    public class Business
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RTN { get; set; }
        public List<User> Users { get; set; }
        public List<Machine> Machines { get; set; }
        public List<Product> Products { get; set; }
        public List<Order> Orders { get; set; }
        public List<Inventory> Inventories { get; set; }
    }
}
