using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPagesMovie.Models
{
    public class Movie
    {
        public int ID { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Book { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Chapter { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        
        [Required]
        public string Scripture { get; set; }

        
    }
}