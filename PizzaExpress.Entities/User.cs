﻿//using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PizzaExpress.Entities
{
    public class User: IdentityUser<int>
    {
        public string? Name { get; set; }

        [NotMapped]
        public string[]? Roles { get; set; }
        
    }
}