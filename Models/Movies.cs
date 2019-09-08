using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movies
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Genre Genre { get; set; }
        [Display(Name = "Genre")]
        public short GenreId { get; set; }
        public DateTime DateAdded { get; set; }
        [Display(Name="Release Date")]
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Display(Name = "Number in Stock")]
        [Range(1,20)]
        public int NumberInStock { get; set; }
    }
}