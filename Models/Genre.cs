using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Genre
    {
        public short Id { get; set; }
        [StringLength(30)]
        public String GenreName { get; set; }
    }
}