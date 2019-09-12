using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.DTO
{
    public class NewRentalsDTO
    {
        public int customerId { get; set; }
        public List<int> movieIds { get; set; }
    }
}