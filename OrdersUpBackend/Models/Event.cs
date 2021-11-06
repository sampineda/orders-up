using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersUpBackend.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string StartStr { get; set; }
        public string EndStr { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
