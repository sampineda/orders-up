﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersUpBackend.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public int BusinessId { get; set; }
        public Business Business { get; set; }
    }
}
