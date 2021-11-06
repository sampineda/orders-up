using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersUpBackend.Models
{
    public class Machine
    {
        public int Id { get; set; }
        public int BusinessId { get; set; }
        public int Heads { get; set; }
        public Business Business { get; set; }
    }
}
