using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.DTO
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GenreDTO Genre { get; set; }
        public short GenreId { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int NumberInStock { get; set; }
    }
}